using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using System.Linq.Expressions;

namespace AdvertBoard.AppServices.User.Services;

/// <summary>
/// Сервис для работы с товарами
/// </summary>
public interface IUserService { 

    /// <summary>
    /// Получение пользователя по ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<UserDto> GetById(Guid id, CancellationToken cancellationToken);

    Task<UserDto> GetWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken);

    /// <summary>
    /// Регистрация.
    /// </summary>
    /// <param name="Login">Логин.</param>
    /// <param name="Password">Пароль.</param>
    /// <returns>Идентификатор пользователя.</returns>
    Task<Guid> Register(string name, string email, string password, string activationCode, CancellationToken cancellationToken);

    /// <summary>
    /// Логин.
    /// </summary>
    /// <param name="Login">Логин.</param>
    /// <param name="Password">Пароль.</param>
    /// <returns>Токен.</returns>
    Task<(string token, Guid userId)> Login(LoginUserDto userDto, CancellationToken cancellationToken);


    /// <summary>
    /// Редактирует пользователя.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="mobile"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Guid> EditAsync(Guid id, string? email, string? oldPassword, string? newPassword, string? name, string? mobile, string? activationCode, string? recoveryCode, CancellationToken cancellationToken);

    Task<string> RecoverPassword(Guid userId, string password, CancellationToken cancellationToken);

    Task<string> DeleteActivationCodeAsync(Guid id, string activationCode, CancellationToken cancellationToken);


    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

}