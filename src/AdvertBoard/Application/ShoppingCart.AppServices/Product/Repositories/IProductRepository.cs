using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.Product.Repositories;

/// <summary>
/// Репозиторий чтения/записи для работы с товарами.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Возвращает записи товаров используя постраничную загрузку.
    /// </summary>
    /// <param name="take">Количество записей в ответе.</param>
    /// <param name="skip">Количество пропущеных записей.</param>
    /// <param name="cancellation">Отмена операции.</param>
    /// <returns>Коллекция элементов <see cref="ProductDto"/>.</returns>
    Task<IReadOnlyCollection<ProductDto>> GetAll(int take, int skip, CancellationToken cancellation);

    /// <summary>
    /// Возвращает записи товаров по фильтру.
    /// </summary>
    /// <param name="request">Модель фильтра товаров.</param>
    /// <param name="cancellation">Отмена операции.</param>
    /// <returns>Коллекция элементов <see cref="ProductDto"/>.</returns>
    Task<IReadOnlyCollection<ProductDto>> GetAllFiltered(ProductFilterRequest request, CancellationToken cancellation);
}