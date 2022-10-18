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
    public Task<IReadOnlyCollection<ShoppingCartDto>> GetAsync(CancellationToken cancellationToken)
    {
        return _shoppingCartRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(CancellationToken cancellationToken)
    {
        return _shoppingCartRepository.CreateAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task UpdateQuantityAsync(Guid id, int quantity,CancellationToken cancellationToken)
    {
        return _shoppingCartRepository.UpdateQuantityAsync(id, quantity, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _shoppingCartRepository.DeleteAsync(id, cancellationToken);
    }
}