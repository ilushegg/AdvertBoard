using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.AdvertisementImage.Repositories
{
    public interface IAdvertisementImageRepository
    {
        Task AddAsync(Domain.AdvertisementImage productImage, CancellationToken cancellationToken);

        void Add(Domain.AdvertisementImage productImage);
        Task Delete(Guid productId, CancellationToken cancellationToken);
        Task EditAsync(Domain.AdvertisementImage productImage, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<ProductImageDto>> GetAllByProduct(Guid productId, CancellationToken cancellationToken);

        Task<ICollection<Domain.AdvertisementImage>> GetAllByAdvertisementEntities(Guid productId, CancellationToken cancellationToken);

        Task<Domain.AdvertisementImage> GetById(Guid id, CancellationToken cancellationToken);
    }
}