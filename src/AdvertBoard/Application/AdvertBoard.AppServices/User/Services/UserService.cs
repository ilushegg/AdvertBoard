using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.AppServices.User.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.DataAccess.EntityConfigurations.UserAvatar;
using AdvertBoard.Domain;
using AdvertBoard.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace AdvertBoard.AppServices.User.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAvatarRepository _userAvatarRepository;
    private readonly IConfiguration _configuration;
    private readonly IPasswordCryptography _passwordCryptography;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
    public UserService(IUserRepository userRepository, IConfiguration configuration, IUserAvatarRepository userAvatarRepository, IPasswordCryptography passwordCryptography)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _userAvatarRepository = userAvatarRepository;
        _passwordCryptography = passwordCryptography;   
    }

    public async Task<UserDto> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var user = _userRepository.FindWhere(u => u.Id == id);
            var avatar = await _userAvatarRepository.GetByUserIdAsync(user.Id, cancellationToken);
            var avatarData = "";
            if (avatar?.FilePath != null)
            {
                byte[] byteImage = File.ReadAllBytes(avatar.FilePath);
                avatarData = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }

            user.Avatar = avatarData;
            return user;

        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserDto> GetWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.FindWhereAsync(predicate, cancellationToken);
            return user;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<(string token, Guid userId)> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {
        var user = _userRepository.FindWhereEntity(user => user.Email == loginUserDto.Email);
        var userDto = _userRepository.FindWhere(user => user.Email == loginUserDto.Email);

        if (user == null)
        {
            throw new InvalidOperationException("Пользователь с таким логином не найден");
               
        }



        if (!_passwordCryptography.AreEqual(loginUserDto.Password, user.Password, "salt"))
        {
            throw new InvalidOperationException("Неверный пароль.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, userDto.UserRole)
        };
        var secretKey = _configuration["Token:SecretKey"];
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256)
            );

        var result = new JwtSecurityTokenHandler().WriteToken(token);
        return (result, user.Id);
    }

    public async Task<Guid> Register(string name, string email, string password, string activationCode, CancellationToken cancellationToken)
    {
        var exUser =  _userRepository.FindWhere(user => user.Email == email);
        if(exUser == null)
        {
        var hashPassword = _passwordCryptography.GenerateHash(password, "salt");
            var id = new Guid();
            var user = new Domain.User
            {
                Id = id,
                Name = name,
                Email = email,
                Password = hashPassword,
                CreateDate = DateTime.UtcNow,
                ActivationCode = activationCode,
                UserRole = new Domain.UserRole { Role = "User", UserId = id}
            };

            _userRepository.Add(user);
            return user.Id;
        }
        else
        {
            throw new InvalidOperationException($"Пользователь с электронным адресом '{email}' уже зарегистрирован");
        }

         
    }

    public async Task<string> RecoverPassword(Guid userId, string password, CancellationToken cancellationToken)
    {
        var exUser = _userRepository.FindWhereEntity(u => u.Id == userId);
        if (exUser != null)
        {
            var hashPassword = _passwordCryptography.GenerateHash(password, "salt");
            exUser.Password = hashPassword;
            exUser.RecoveryCode = null;    

            await _userRepository.EditAsync(exUser, cancellationToken);
            return "Ваш новый пароль сохранен. Теперь вы можете зайти на сайт.";
        }
        else
        {
            return "Пользователь не найден или ссылка неверна.";
        }


    }


    public async Task<Guid> EditAsync(Guid id, string? email, string? oldPassword, string? newPassword, string? name, string? mobile, string? activationCode, string? recoveryCode, CancellationToken cancellationToken)
    {
        var exUser = await _userRepository.FindById(id, cancellationToken);
        if (exUser != null)
        {

            if (oldPassword != null)
            {
                if (!_passwordCryptography.AreEqual(oldPassword, exUser.Password, "salt"))
                {
                    throw new InvalidOperationException("Неверный пароль.");
                }
                exUser.Password = _passwordCryptography.GenerateHash(newPassword, "salt");

            }
            exUser.Id = exUser.Id;
            exUser.Name = name != null ? name : exUser.Name;
            exUser.Email = email != null ? email : exUser.Email;
            exUser.Mobile = mobile != null ? mobile : exUser.Mobile;
            exUser.CreateDate = exUser.CreateDate;
            exUser.ActivationCode = activationCode != null ? activationCode : exUser.ActivationCode;
            exUser.RecoveryCode = recoveryCode != null ? recoveryCode : exUser.RecoveryCode;

            await _userRepository.EditAsync(exUser, cancellationToken);
            return exUser.Id;
        }
        else
        {
            throw new InvalidOperationException($"Пользователь не найден");
        }
               
    }

    public async Task<string> DeleteActivationCodeAsync(Guid id, string activationCode, CancellationToken cancellationToken)
    {
        var exUser = await _userRepository.FindById(id, cancellationToken);
        if (exUser != null && exUser.ActivationCode == activationCode)
        {
            exUser.ActivationCode = null;
            await _userRepository.EditAsync(exUser, cancellationToken);
            return "Пользователь успешно активирован.";
        }
        else
        {
            return "Пользователь не найден или ссылка неверна.";
        }


    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var exUser = await _userRepository.FindById(id, cancellationToken);
        if (exUser != null)
        {
            await _userRepository.DeleteAsync(exUser, cancellationToken);
        }
        else
        {
            throw new InvalidOperationException($"Пользователь не найден");
        }


    }

}