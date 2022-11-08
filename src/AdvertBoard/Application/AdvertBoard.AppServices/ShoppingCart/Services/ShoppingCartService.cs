using AdvertBoard.AppServices.ShoppingCart.Repositories;
using AdvertBoard.Contracts;
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
    public Task<Guid> CreateAsync(Domain.Product product, int quantity, CancellationToken cancellationToken)
    {
        return _shoppingCartRepository.CreateAsync(product, quantity, cancellationToken);
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