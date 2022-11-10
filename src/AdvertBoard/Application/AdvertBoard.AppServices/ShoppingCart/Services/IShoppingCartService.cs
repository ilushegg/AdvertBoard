using AdvertBoard.Contracts;
using AdvertBoard.Domain;

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
    Task<IReadOnlyCollection<ShoppingCartDto>> GetAsync(Guid userId, CancellationToken cancellationToken);

    Task<Guid> AddAsync(Domain.Product product, int quantity, User user, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task UpdateQuantityAsync(Guid shoppingCartId, Guid productId, int quantity, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Domain.ShoppingCart> GetByProductId(Guid productId, User user, CancellationToken cancellationToken);
}