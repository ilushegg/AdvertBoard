using Microsoft.AspNetCore.Http;

namespace AdvertBoard.AppServices.ProductImage.Services
{
    public interface IAdvertisementImageService
    {
        Task AddAsync(Guid productId, Guid[] files, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task EditAsync(Guid id, IFormFile file, CancellationToken cancellationToken);
    }
}