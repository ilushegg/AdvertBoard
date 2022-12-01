using Microsoft.AspNetCore.Http;

namespace AdvertBoard.AppServices.Image.Services
{
    public interface IImageService
    {
        Task<Guid> AddAsync(IFormFile file, CancellationToken cancellationToken);
        Task<Guid> EditAsync(Guid id, IFormFile file, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}