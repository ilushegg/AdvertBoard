using AdvertBoard.Contracts;

namespace AdvertBoard.DataAccess.EntityConfigurations.UserAvatar
{
    public interface IUserAvatarRepository
    {
        Task AddAsync(Domain.UserAvatar userAvatar, CancellationToken cancellationToken);
        Task Delete(Guid productId, CancellationToken cancellationToken);
        Task EditAsync(Domain.UserAvatar userAvatar, CancellationToken cancellationToken);
        Task<UserAvatarDto> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}