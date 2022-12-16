using AdvertBoard.Contracts;
using System.Linq.Expressions;

namespace AdvertBoard.DataAccess.EntityConfigurations.Comment
{
    public interface ICommentRepository
    {
        Task AddAsync(Domain.Comment comment, CancellationToken cancellationToken);
        Task Delete(Domain.Comment comment, CancellationToken cancellationToken);
        Task EditAsync(Domain.Comment comment, CancellationToken cancellationToken);
        Task<Domain.Comment> GetById(Guid id, CancellationToken cancellationToken);

        Task<Domain.Comment> GetWhere(Expression<Func<Domain.Comment, bool>> predicate, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<CommentDto>> GetAllPaged(int skip, int take, Expression<Func<Domain.Comment, bool>> predicate, CancellationToken cancellationToken);

        Task<int> GetAllCount(Expression<Func<Domain.Comment, bool>> predicate, CancellationToken cancellation);
    }
}