using Microsoft.AspNetCore.Http;

namespace AdvertBoard.AppServices.User.Services
{
    public interface IUserAvatarService
    {
        Task<Guid> AddAsync(Guid userId, Guid imageId, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Guid> EditAsync(Guid id, Guid imageId, CancellationToken cancellationToken);

        Task<Guid> GetAvatarByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Guid GetAvatarByUserId(Guid userId);
    }
}