
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using AdvertBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.DataAccess.EntityConfigurations.Comment
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepository<Domain.Comment> _repository;

        public CommentRepository(IRepository<Domain.Comment> repository)
        {
            _repository = repository;
        }



        public async Task AddAsync(Domain.Comment comment, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(comment);
        }

        public async Task EditAsync(Domain.Comment comment, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(comment);
        }

        public async Task Delete(Domain.Comment comment, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(comment);
        }

        public async Task<Domain.Comment> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Domain.Comment> GetWhere(Expression<Func<Domain.Comment, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<CommentDto>> GetAllPaged(int skip, int take, Expression<Func<Domain.Comment, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(predicate).Skip(skip).Take(take).Select(c => new CommentDto
            {
                Id = c.Id,
                AdvertisementId = c.AdvertisementId,
                UserId = c.UserId,
                Text = c.Text,
                DateTimeCreated = $"{c.DateTimeCreated.ToString("g")}",
                Status = c.Status,
                UserName = c.User.Name,
                UserAvatar = c.User.Avatar != null ? "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(c.User.Avatar.Image.FilePath)) : "",
            }).ToListAsync();
        }

        public async Task<int> GetAllCount(Expression<Func<Domain.Comment, bool>> predicate, CancellationToken cancellation)
        {
            return await _repository.GetAll().Where(predicate).CountAsync();

        }

    }
}
