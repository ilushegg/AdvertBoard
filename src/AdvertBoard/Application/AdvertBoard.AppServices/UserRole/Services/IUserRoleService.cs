namespace AdvertBoard.AppServices.UserRole.Services
{
    public interface IUserRoleService
    {
        Task AddAsync(Guid userId, string role, CancellationToken cancellationToken);
        Task EditAsync(Guid userId, string role, CancellationToken cancellationToken);
    }
}