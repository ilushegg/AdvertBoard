namespace AdvertBoard.DataAccess.EntityConfigurations.UserAvatar
{
    public interface IUserAvatarRepository
    {
        Task AddAsync(Domain.UserAvatar userAvatar, CancellationToken cancellationToken);
        Task Delete(Guid productId, CancellationToken cancellationToken);
        Task EditAsync(Domain.UserAvatar userAvatar, CancellationToken cancellationToken);
        Task<Domain.UserAvatar> GetById(Guid id, CancellationToken cancellationToken);
    }
}