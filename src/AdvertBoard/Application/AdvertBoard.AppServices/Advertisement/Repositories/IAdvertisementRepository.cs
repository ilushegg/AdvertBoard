using AdvertBoard.Contracts;
using System.Linq.Expressions;

namespace AdvertBoard.AppServices.Product.Repositories;

/// <summary>
/// Репозиторий чтения/записи для работы с объявлениями.
/// </summary>
public interface IAdvertisementRepository
{
    /// <summary>
    /// Возвращает записи объявлений используя постраничную загрузку.
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

    /// <summary>
    /// Возвращает кол-во объявлений.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<int> GetAllCount(Expression<Func<Domain.Advertisement, bool>> predicate, CancellationToken cancellation);

  

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<bool> AddAsync(Domain.Advertisement product, CancellationToken cancellation);

    /// <summary>
    /// Добавляет объявление.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Guid Add(Domain.Advertisement product);

    /// <summary>
    /// Удаляет объявление.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task DeleteAsync(Domain.Advertisement product, CancellationToken cancellation);

    /// <summary>
    /// Редактирует объявление.
    /// </summary>
    /// <param name="product"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task<Guid> EditAsync(Domain.Advertisement product, CancellationToken cancellation);

    /// <summary>
    /// Ищет объявление по идентификатору.
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
    Task<IReadOnlyCollection<AdvertisementDto>> GetWhere(int skip, int take, string[]? query, Guid? categoryId, string? location, decimal? fromPrice, decimal? toPrice, string? sort, CancellationToken cancellation);

}