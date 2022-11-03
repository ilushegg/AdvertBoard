using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.ShoppingCart.Repositories;

/// <summary>
/// Репозиторий для чтения записи элементов корзины товаров.
/// </summary>
public interface IShoppingCartRepository
{
    /// <summary>
    /// Возвращает все элементы корзины.
    /// </summary>
    /// <returns>Коллекция элементов корзины <see cref="ShoppingCartDto"/>.</returns>
    Task<IReadOnlyCollection<ShoppingCartDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Создает корзину.
    /// </summary>
    /// <returns>Идентификатор корзины.<see cref="ShoppingCartDto"/>.</returns>
    Task<Guid> CreateAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет количество товара в корзине.
    /// </summary>
    /// <param name="id">Идентификатор позиции корзины.</param>
    /// <param name="quantity">Новое количество товара.</param>
    Task UpdateQuantityAsync(Guid id, int quantity, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удаляет товар из корзины.
    /// </summary>
    /// <param name="id">Идентификатор позиции корзины.</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}