using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using System.Linq.Expressions;

namespace AdvertBoard.AppServices.User.Repositories;

/// <summary>
/// Репозиторий чтения/записи для работы с товарами.
/// </summary>
public interface IUserRepository
{
    Task<Domain.User> FindWhere(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken);

    Task AddAsync(Domain.User user);

    void Add(Domain.User user);

    Task<Domain.User> FindById(Guid id, CancellationToken cancellationToken);

    void Edit(Domain.User user);

    Task EditAsync(Domain.User user, CancellationToken cancellation);
}