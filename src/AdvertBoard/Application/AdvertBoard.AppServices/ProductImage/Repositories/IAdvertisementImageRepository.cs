using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.AdvertisementImage.Repositories
{
    public interface IAdvertisementImageRepository
    {
        Task AddAsync(Domain.AdvertisementImage productImage, CancellationToken cancellationToken);
        Task Delete(Guid productId, CancellationToken cancellationToken);
        Task EditAsync(Domain.AdvertisementImage productImage, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<ProductImageDto>> GetAllByProduct(Guid productId, CancellationToken cancellationToken);

        Task<Domain.AdvertisementImage> GetById(Guid id, CancellationToken cancellationToken);
    }
}