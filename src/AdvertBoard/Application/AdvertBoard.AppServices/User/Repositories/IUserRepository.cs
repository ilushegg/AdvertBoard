using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using System.Linq.Expressions;

namespace AdvertBoard.AppServices.Product.Repositories;

/// <summary>
/// Репозиторий чтения/записи для работы с товарами.
/// </summary>
public interface IUserRepository
{
    Task<User> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);

    Task Add(User user);

    Task<User> FindById(Guid id, CancellationToken cancellationToken);
}