using AdvertBoard.Contracts;
using AdvertBoard.Domain;

namespace AdvertBoard.AppServices.Product.Services;

/// <summary>
/// Сервис для работы с товарами
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Регистрация.
    /// </summary>
    /// <param name="Login">Логин.</param>
    /// <param name="Password">Пароль.</param>
    /// <returns>Идентификатор пользователя.</returns>
    Task<Guid> Register(string login, string password, CancellationToken cancellationToken);

    /// <summary>
    /// Логин.
    /// </summary>
    /// <param name="Login">Логин.</param>
    /// <param name="Password">Пароль.</param>
    /// <returns>Токен.</returns>
    Task<string> Login(string login, string password, CancellationToken cancellationToken);

    /// <summary>
    /// Получить текущего пользователя.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User> GetCurrent(CancellationToken cancellationToken);

}