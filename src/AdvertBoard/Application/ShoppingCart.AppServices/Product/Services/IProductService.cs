using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.Product.Services;

/// <summary>
/// Сервис для работы с товарами
/// </summary>
public interface IProductService
{
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
    Task<bool> AddAsync(string name, string description, decimal price, Guid category, CancellationToken cancellation);
}