using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.ShoppingCart.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;

namespace AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;

/// <inheritdoc />
public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly IRepository<Domain.ShoppingCart> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ShoppingCartRepository"/>.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    public ShoppingCartRepository(IRepository<Domain.ShoppingCart> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ShoppingCartDto>> GetAllAsync()
    {
        return await _repository.GetAll()
            .Include(s => s.Product)
            .Select(s => new ShoppingCartDto
            {
                Id = s.Id,
                ProductName = s.Product.Name,
                Quantity = s.Quantity,
                Price = s.Price,
                Amount = s.Amount
            }).ToListAsync();
    }

    /// <inheritdoc />
    public async Task UpdateQuantityAsync(Guid id, int quantity)
    {
        var existingCart = await _repository.GetByIdAsync(id);

        if (existingCart == null)
        {
            throw new InvalidOperationException($"Корзины с идентификатором {id} не найдено!");
        }
        
        existingCart.Quantity = quantity;
        await _repository.UpdateAsync(existingCart);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        var existingCart = await _repository.GetByIdAsync(id);

        if (existingCart == null)
        {
            throw new InvalidOperationException($"Корзины с идентификатором {id} не найдено!");
        }
        
        await _repository.DeleteAsync(existingCart);
    }
}