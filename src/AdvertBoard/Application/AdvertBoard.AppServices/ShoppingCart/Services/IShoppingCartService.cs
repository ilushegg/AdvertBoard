using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.ShoppingCart.Services;

/// <summary>
/// 
/// </summary>
public interface IShoppingCartService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IReadOnlyCollection<ShoppingCartDto>> GetAsync(CancellationToken cancellationToken);

    Task<Guid> CreateAsync(Domain.Product product, int quantity, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task UpdateQuantityAsync(Guid id, int quantity, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}