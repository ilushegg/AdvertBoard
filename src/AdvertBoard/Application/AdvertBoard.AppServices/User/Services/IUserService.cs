using AdvertBoard.Contracts;
using AdvertBoard.Domain;

namespace AdvertBoard.AppServices.Product.Services;

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

    /// <summary>
    /// Регистрация.
    /// </summary>
    /// <param name="Login">Логин.</param>
    /// <param name="Password">Пароль.</param>
    /// <returns>Идентификатор пользователя.</returns>
    Task<Guid> Register(RegisterUserDto userDto, CancellationToken cancellationToken);

    /// <summary>
    /// Логин.
    /// </summary>
    /// <param name="Login">Логин.</param>
    /// <param name="Password">Пароль.</param>
    /// <returns>Токен.</returns>
    Task<(string token, Guid userId)> Login(LoginUserDto userDto, CancellationToken cancellationToken);

    /// <summary>
    /// Получить текущего пользователя.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<User> GetCurrent(CancellationToken cancellationToken);

}