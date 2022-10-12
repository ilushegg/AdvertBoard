using AdvertBoard.AppServices.ShoppingCart.Repositories;
using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.ShoppingCart.Services;

/// <inheritdoc />
public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<ShoppingCartDto>> GetAsync()
    {
        return _shoppingCartRepository.GetAllAsync();
    }

    /// <inheritdoc />
    public Task UpdateQuantityAsync(Guid id, int quantity)
    {
        return _shoppingCartRepository.UpdateQuantityAsync(id, quantity);
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id)
    {
        return _shoppingCartRepository.DeleteAsync(id);
    }
}