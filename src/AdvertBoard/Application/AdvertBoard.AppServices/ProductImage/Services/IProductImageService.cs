using Microsoft.AspNetCore.Http;

namespace AdvertBoard.AppServices.ProductImage.Services
{
    public interface IProductImageService
    {
        Task AddAsync(Guid productId, IFormFile[] files, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task EditAsync(Guid id, IFormFile file, CancellationToken cancellationToken);
    }
}