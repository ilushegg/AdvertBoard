using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.ShoppingCart.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using AdvertBoard.Domain;

namespace AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;

/// <inheritdoc />
public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IRepository<Domain.ShoppingCart> _repository;
    private readonly IRepository<Domain.Advertisement> _productRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ShoppingCartRepository"/>.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    public ShoppingCartRepository(IRepository<Domain.ShoppingCart> repository, IRepository<Domain.Advertisement> product)
    {
        _repository = repository;
        _productRepository = product;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Domain.ShoppingCart>> GetAllAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _repository.GetAll().Where(s => s.UserId == userId).ToListAsync();
    }

    /// <inheritdoc />
    public async Task UpdateQuantityAsync(Guid shoppingCartId, Guid productId, int quantity, CancellationToken cancellationToken)
    {
        var existingCart = await _repository.GetByIdAsync(shoppingCartId);

        if (existingCart == null)
        {
            throw new InvalidOperationException($"Корзины с идентификатором {shoppingCartId} не найдено!");
        }
        
        existingCart.Quantity = quantity;
        await _repository.UpdateAsync(existingCart);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingCart = await _repository.GetByIdAsync(id);

        if (existingCart == null)
        {
            throw new InvalidOperationException($"Корзины с идентификатором {id} не найдено!");
        }
        
        await _repository.DeleteAsync(existingCart);
    }

    public async Task<Guid> CreateAsync(Domain.ShoppingCart shoppingCart,  CancellationToken cancellationToken)
    {

        await _repository.AddAsync(shoppingCart);
        return shoppingCart.Id;
    }

    public async Task<Domain.ShoppingCart> GetByProductId(Guid productId, Guid userId, CancellationToken cancellationToken)
    {
        return await _repository.GetAll().Where(s => s.ProductId == productId && s.UserId == userId).FirstOrDefaultAsync();
    }

}