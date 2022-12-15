using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using System.Linq.Expressions;

namespace AdvertBoard.AppServices.User.Repositories;

/// <summary>
/// Репозиторий чтения/записи для работы с товарами.
/// </summary>
public interface IUserRepository
{
    Task<UserDto> FindWhereAsync(Expression<Func<Domain.User, bool>> predicate, CancellationToken cancellationToken);

    UserDto FindWhere(Expression<Func<Domain.User, bool>> predicate);

    Task AddAsync(Domain.User user);

    Guid Add(Domain.User user);

    Task<Domain.User> FindById(Guid id, CancellationToken cancellationToken);

    Guid Edit(Domain.User user);

    Task EditAsync(Domain.User user, CancellationToken cancellation);
}