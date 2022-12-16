using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Enum;

namespace AdvertBoard.AppServices.Comment.Services
{
    public interface ICommentService
    {
        Task<GetPagedResultDto<CommentDto>> GetAllByAdvertisement(int skip, int take, Guid advertisementId, CancellationToken cancellationToken);
        Task<GetPagedResultDto<CommentDto>> GetAllByUser(int skip, int take, Guid userId, CancellationToken cancellationToken);
        Task<Guid> AddAsync(Guid userId, Guid advertisementId, string text, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> EditAsync(Guid id, string text, CommentStatus commentStatus, CancellationToken cancellationToken);


    }
}