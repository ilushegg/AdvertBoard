using AdvertBoard.AppServices.ShoppingCart.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace AdvertBoard.AppServices.ShoppingCart.Services;

/// <inheritdoc />
public class ShoppingCartService : IShoppingCartService
{
    private const string CartId = "CartId";
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IHttpContextAccessor contextAccessor)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _contextAccessor = contextAccessor;
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<ShoppingCartDto>> GetAsync(CancellationToken cancellationToken)
    {
        /* ISession session = _contextAccessor.HttpContext.Session;
         Guid shoppingCartId = session[CartSe]
         return _shoppingCartRepository.GetAllAsync(cancellationToken);*/
        /*if (_contextAccessor.HttpContext.Session. == null)
        {

        }*/
        return _shoppingCartRepository.GetAllAsync(cancellationToken);


    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(Domain.Product product, int quantity, User user, CancellationToken cancellationToken)
    {
        var shoppingCart = new Domain.ShoppingCart
        {
            ProductId = product.Id,
            Quantity = quantity,
            Price = product.Price,
            Amount = product.Price * quantity,
            User = user
        };

        return _shoppingCartRepository.CreateAsync(shoppingCart, cancellationToken);
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

    public async Task<Domain.ShoppingCart> GetByProductId(Guid productId, User user, CancellationToken cancellationToken)
    {
        return await _shoppingCartRepository.GetByProductId(productId, user.Id, cancellationToken);
    }
}