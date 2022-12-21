using AdvertBoard.Contracts;
using AdvertBoard.Domain;

namespace AdvertBoard.AppServices.Advertisement.Services;

/// <summary>
/// Сервис для работы с объявлениями.
/// </summary>
public interface IAdvertisementService
{
    /// <summary>
    /// Возвращает объявление по идентификатору.
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<FullAdvertisementDto> GetById(Guid advertisementId, CancellationToken cancellation);

    /// <summary>
    /// Возвращает объявления по поиску.
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="query"></param>
    /// <param name="categoryId"></param>
    /// <param name="location"></param>
    /// <param name="fromPrice"></param>
    /// <param name="toPrice"></param>
    /// <param name="sort"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GetPagedResultDto<AdvertisementDto>> GetAllBySearch(int skip, int take, string? query, Guid? categoryId, string? location, decimal? fromPrice, decimal? toPrice, string? sort, CancellationToken cancellationToken);


    /// <summary>
    /// Возвращает все объявления используя пагинацию.
    /// </summary>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    Task<GetPagedResultDto<AdvertisementDto>> GetAll(int take, int skip, CancellationToken cancellation);

    /// <summary>
    /// Возвращает все объявления автора используя пагинацию
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="userId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<GetPagedResultDto<AdvertisementDto>> GetAllByAuthor(int skip, int take, Guid userId, CancellationToken cancellation);

    /// <summary>
    /// Редактирует объявление.
    /// </summary>
    /// <param name="advertisement"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<(Guid adId, Guid locId)> EditAsync(Guid advertisementId, string name, string description, decimal price, Guid categoryId, CancellationToken cancellation);

    /// <summary>
    /// Снять/ выставить на публикацию.
    /// </summary>
    /// <param name="adId"></param>
    /// <param name="publ"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Guid> EditPublicAsync(Guid adId, string status, CancellationToken cancellation);


    /// <summary>
    /// Удаляет объявление.
    /// </summary>
    /// <param name="advertisementId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid advertisementId, CancellationToken cancellation);

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="price"></param>
    /// <param name="categoryId"></param>
    /// <param name="locationId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Guid Add(string name, string description, decimal price, Guid categoryId, Guid locationId, Guid userId);
    
    /// <summary>
    /// Добавляет объявление асинхронно.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="price"></param>
    /// <param name="categoryId"></param>
    /// <param name="locationId"></param>
    /// <param name="user"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Guid> AddAsync(string name, string description, decimal price, Guid categoryId, Guid locationId, Domain.User user, CancellationToken cancellation = default);


}