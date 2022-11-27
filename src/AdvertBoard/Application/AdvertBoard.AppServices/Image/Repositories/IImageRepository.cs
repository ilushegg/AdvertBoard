using AdvertBoard.Domain;

namespace AdvertBoard.DataAccess.EntityConfigurations.ProductImage
{
    public interface IImageRepository
    {
        Task AddAsync(Image image, CancellationToken cancellationToken);
        Task Delete(Image image, CancellationToken cancellationToken);
        Task EditAsync(Image image, CancellationToken cancellationToken);
        Task<Image> GetById(Guid id, CancellationToken cancellationToken);
    }
}