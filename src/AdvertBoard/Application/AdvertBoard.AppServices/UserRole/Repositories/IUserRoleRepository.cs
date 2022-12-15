using System.Linq.Expressions;

namespace AdvertBoard.DataAccess.EntityConfigurations.Product
{
    public interface IUserRoleRepository
    {
        Guid Add(Domain.UserRole role);
        Task AddAsync(Domain.UserRole role);
        Guid Edit(Domain.UserRole role);
        Task EditAsync(Domain.UserRole role, CancellationToken cancellation);
        Task<Domain.UserRole> FindByUserIdAsync(Guid id, CancellationToken cancellationToken);
        Domain.UserRole FindWhere(Expression<Func<Domain.UserRole, bool>> predicate);
        Task<Domain.UserRole> FindWhereAsync(Expression<Func<Domain.UserRole, bool>> predicate, CancellationToken cancellationToken);
    }
}