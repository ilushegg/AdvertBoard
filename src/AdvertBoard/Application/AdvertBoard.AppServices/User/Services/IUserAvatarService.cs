using Microsoft.AspNetCore.Http;

namespace AdvertBoard.AppServices.ProductImage.Services
{
    public interface IUserAvatarService
    {
        Task AddAsync(Guid userId, IFormFile file, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task EditAsync(Guid id, IFormFile file, CancellationToken cancellationToken);
    }
}