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
    Task<IReadOnlyCollection<ShoppingCartDto>> GetAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="quantity"></param>
    /// <returns></returns>
    Task UpdateQuantityAsync(Guid id, int quantity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
}