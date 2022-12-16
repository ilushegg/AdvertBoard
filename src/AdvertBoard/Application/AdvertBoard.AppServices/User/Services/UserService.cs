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
    private readonly IClaimsAccessor _claimsAccessor;
    private readonly IConfiguration _configuration;
    private readonly IPasswordCryptography _passwordCryptography;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
    public UserService(IUserRepository userRepository, IClaimsAccessor claimsAccessor, IConfiguration configuration, IUserAvatarRepository userAvatarRepository, IPasswordCryptography passwordCryptography)
    {
        _userRepository = userRepository;
        _claimsAccessor = claimsAccessor;
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

    public async Task<Domain.User> GetCurrent(CancellationToken cancellationToken)
    {
        var claims = await _claimsAccessor.GetClaims(cancellationToken);
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId))
        {
            return null;
        }

        var id = Guid.Parse(claimId);
        var user = await _userRepository.FindById(id, cancellationToken);

        if (user == null)
        {
            throw new Exception($"Пользователь с идентификатором '{id}' не найден.");
        }

        return user;
    }

    public async Task<(string token, Guid userId)> Login(LoginUserDto userDto, CancellationToken cancellationToken)
    {
        var user =  _userRepository.FindWhere(user => user.Email == userDto.Email);
        if (user == null)
        {
            throw new InvalidOperationException("Пользователь с таким логином не найден");
               
        }



        if (!_passwordCryptography.AreEqual(userDto.Password, user.Password, "salt"))
        {
            throw new InvalidOperationException("Неверный пароль.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserRole)
        };
        var secretKey = _configuration["Token:SecretKey"];
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256)
            );

        var result = new JwtSecurityTokenHandler().WriteToken(token);
        return (result, user.Id);
    }

    public async Task<Guid> Register(string name, string email, string password, CancellationToken cancellationToken)
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

    public async Task<Guid> EditAsync(Guid id, string name, string mobile, CancellationToken cancellationToken)
    {
        var exUser = await _userRepository.FindWhereAsync(user => user.Id == id, cancellationToken);
        if (exUser != null)
        {
            
            var user = new Domain.User
            {
                Id = exUser.Id,
                Name = name,
                Email = exUser.Email,
                Mobile = mobile,
                Password = exUser.Password,
                CreateDate = exUser.CreateDate

            };

            await _userRepository.EditAsync(user, cancellationToken);
            return user.Id;
        }
        else
        {
            throw new InvalidOperationException($"Пользователь не найден");
        }

        
    }

}