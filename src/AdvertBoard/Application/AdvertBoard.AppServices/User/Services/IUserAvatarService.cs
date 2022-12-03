using Microsoft.AspNetCore.Http;

namespace AdvertBoard.AppServices.User.Services
{
    public interface IUserAvatarService
    {
        Task<Guid> AddAsync(Guid userId, Guid imageId, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task EditAsync(Guid id, IFormFile file, CancellationToken cancellationToken);

        Task<Guid> GetAvatarByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}