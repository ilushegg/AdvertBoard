using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvertBoard.AppServices.Product.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IClaimsAccessor _claimsAccessor;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
    public UserService(IUserRepository userRepository, IClaimsAccessor claimsAccessor)
    {
        _userRepository = userRepository;
        _claimsAccessor = claimsAccessor;
    }

    public async Task<User> GetCurrent(CancellationToken cancellationToken)
    {
        var claims = await _claimsAccessor.GetClaims(cancellationToken);
        var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }
        var user = await _userRepository.

        return null;
    }

    public async Task<string> Login(string login, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindWhere(user => user.Login == login, cancellationToken);
        if (user == null)
        {
            throw new Exception("Пользователь не найден.");
        }

        if (!user.Password.Equals(password))
        {
            throw new Exception("Нет прав.");
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Login)
        };
        var secretKey = "secretKey";
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256)
            );

        var result = new JwtSecurityTokenHandler().WriteToken(token);
        return result;
    }

    public async Task<Guid> Register(string login, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindWhere(user => user.Login == login, cancellationToken);
        if(user == null)
        {
            user = new User
            {
                Name = login,
                Login = login,
                Password = password,
                CreateDate = DateTime.UtcNow
            };
        }
        else
        {
            throw new Exception($"Пользователь с логином '{login}' уже зарегестрирован");
        }

        await _userRepository.Add(user);
        return user.Id; 
    }

}