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

    /// <summary>
    /// Добавляет товар.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> AddAsync(Domain.Product product, CancellationToken cancellation);

    /// <summary>
    /// Удаляет товар.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(Domain.Product product, CancellationToken cancellation);

    /// <summary>
    /// Редактирует товар.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> EditAsync(Domain.Product product, CancellationToken cancellation);

    /// <summary>
    /// Ищет товар по идентификатору.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Domain.Product> FindById(Guid productId, CancellationToken cancellation);

}