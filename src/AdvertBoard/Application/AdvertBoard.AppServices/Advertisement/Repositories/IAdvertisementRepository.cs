using AdvertBoard.Contracts;
using System.Linq.Expressions;

namespace AdvertBoard.AppServices.Product.Repositories;

/// <summary>
/// Репозиторий чтения/записи для работы с товарами.
/// </summary>
public interface IAdvertisementRepository
{
    /// <summary>
    /// Возвращает записи товаров используя постраничную загрузку.
    /// </summary>
    /// <param name="take">Количество записей в ответе.</param>
    /// <param name="skip">Количество пропущеных записей.</param>
    /// <param name="cancellation">Отмена операции.</param>
    /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
    Task<IReadOnlyCollection<AdvertisementDto>> GetAll(int take, int skip, CancellationToken cancellation);

    /// <summary>
    /// Возвращает объявления автора используя постраничную загрузку.
    /// </summary>
    /// <param name="take">Количество записей в ответе.</param>
    /// <param name="skip">Количество пропущеных записей.</param>
    /// <param name="cancellation">Отмена операции.</param>
    /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
    Task<IReadOnlyCollection<AdvertisementDto>> GetAllByAuthor(int take, int skip, Guid userId, CancellationToken cancellation);

    Task<int> GetAllCount(Expression<Func<Domain.Advertisement, bool>> predicate, CancellationToken cancellation);

    /// <summary>
    /// Возвращает записи товаров по фильтру.
    /// </summary>
    /// <param name="request">Модель фильтра товаров.</param>
    /// <param name="cancellation">Отмена операции.</param>
    /// <returns>Коллекция элементов <see cref="AdvertisementDto"/>.</returns>
    Task<IReadOnlyCollection<AdvertisementDto>> GetAllFiltered(ProductFilterRequest request, CancellationToken cancellation);

    /// <summary>
    /// Добавляет товар.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> AddAsync(Domain.Advertisement product, CancellationToken cancellation);


    Guid Add(Domain.Advertisement product);

    /// <summary>
    /// Удаляет товар.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task DeleteAsync(Domain.Advertisement product, CancellationToken cancellation);

    /// <summary>
    /// Редактирует товар.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> EditAsync(Domain.Advertisement product, CancellationToken cancellation);

    /// <summary>
    /// Ищет товар по идентификатору.
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Domain.Advertisement> GetById(Guid productId, CancellationToken cancellation);


    /// <summary>
    /// Поиск объявления по строке поиска.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<AdvertisementDto>> GetWhere(int skip, int take, string? query, Guid? categoryId, string? city, decimal? fromPrice, decimal? toPrice, string? sort, CancellationToken cancellation);

}