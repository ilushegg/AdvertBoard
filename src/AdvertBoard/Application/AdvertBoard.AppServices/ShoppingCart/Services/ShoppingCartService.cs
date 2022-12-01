using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.AppServices.ShoppingCart.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace AdvertBoard.AppServices.ShoppingCart.Services;

/// <inheritdoc />
public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IAdvertisementRepository _productRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IAdvertisementRepository productRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;

    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ShoppingCartDto>> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        /* ISession session = _contextAccessor.HttpContext.Session;
         Guid shoppingCartId = session[CartSe]
         return _shoppingCartRepository.GetAllAsync(cancellationToken);*/
        /*if (_contextAccessor.HttpContext.Session. == null)
        {

        }*/
        var shoppingCart = await _shoppingCartRepository.GetAllAsync(userId, cancellationToken);
        var shoppingCartDto = new List<ShoppingCartDto>();
        foreach(Domain.ShoppingCart s in shoppingCart)
        {
            var product = await _productRepository.GetById(s.ProductId, cancellationToken);
            shoppingCartDto.Add(new ShoppingCartDto
            {
                Id = s.ProductId,
                ProductName = product.Name,
                Quantity = s.Quantity,
                Price = product.Price,
                Amount = s.Amount
            });
        }
        return shoppingCartDto; 


    }

    /// <inheritdoc />
    public Task<Guid> AddAsync(FullAdvertisementDto product, int quantity, Domain.User user, CancellationToken cancellationToken)
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
    public Task UpdateQuantityAsync(Guid shoppingCartId, Guid productId, int quantity,CancellationToken cancellationToken)
    {
        return _shoppingCartRepository.UpdateQuantityAsync(shoppingCartId, productId, quantity, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _shoppingCartRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<Domain.ShoppingCart> GetByProductId(Guid productId, Domain.User user, CancellationToken cancellationToken)
    {
        return await _shoppingCartRepository.GetByProductId(productId, user.Id, cancellationToken);
    }
}