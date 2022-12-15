using AdvertBoard.Contracts;
using AdvertBoard.Domain;

namespace AdvertBoard.AppServices.Advertisement.Services;

/// <summary>
/// Сервис для работы с товарами
/// </summary>
public interface IAdvertisementService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<FullAdvertisementDto> GetById(Guid productId, CancellationToken cancellation);

    Task<GetPagedResultDto> GetAllBySearch(int skip, int take, string? query, Guid? categoryId, string? city, decimal? fromPrice, decimal? toPrice, CancellationToken cancellationToken);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<AdvertisementDto>> GetAll(int take, int skip, CancellationToken cancellation);


    Task<GetPagedResultDto> GetAllByAuthor(int take, int skip, Guid userId, CancellationToken cancellation);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<AdvertisementDto>> GetAllFiltered(ProductFilterRequest request, CancellationToken cancellation);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>



    Task<(Guid adId, Guid locId)> EditAsync(Guid productId, string name, string description, decimal price, Guid categoryId, CancellationToken cancellation);


    Task DeleteAsync(Guid productId, CancellationToken cancellation);

    Guid Add(string name, string description, decimal price, Guid categoryId, Guid locationId, Guid userId);
    Task<Guid> AddAsync(string name, string description, decimal price, Guid categoryId, Guid locationId, Domain.User user, CancellationToken cancellation = default);


}