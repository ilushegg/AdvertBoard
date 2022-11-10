using AdvertBoard.Contracts;
using AdvertBoard.Domain;

namespace AdvertBoard.AppServices.Product.Services;

/// <summary>
/// Сервис для работы с товарами
/// </summary>
public interface IProductService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Domain.Product> Get(Guid productId, CancellationToken cancellation);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<ProductDto>> GetAll(int take, int skip, CancellationToken cancellation);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<ProductDto>> GetAllFiltered(ProductFilterRequest request, CancellationToken cancellation);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>



    Task<bool> EditAsync(Guid productId, string name, string description, decimal price, Guid categoryId, CancellationToken cancellation);


    Task<bool> DeleteAsync(Guid productId, CancellationToken cancellation);
    Task<Guid> AddAsync(string name, string description, decimal price, string categoryName, User user, CancellationToken cancellation = default);
}