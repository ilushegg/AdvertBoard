using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using Microsoft.Extensions.Configuration;
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
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
    public UserService(IUserRepository userRepository, IClaimsAccessor claimsAccessor, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _claimsAccessor = claimsAccessor;
        _configuration = configuration;
    }

    public async Task<User> GetCurrent(CancellationToken cancellationToken)
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
        var secretKey = _configuration["Token:SecretKey"];
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