using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.ProductImage.Repositories
{
    public interface IProductImageRepository
    {
        Task AddAsync(Domain.ProductImage productImage, CancellationToken cancellationToken);
        Task Delete(Guid productId, CancellationToken cancellationToken);
        Task EditAsync(Domain.ProductImage productImage, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<ProductImageDto>> GetAllByProduct(Guid productId, CancellationToken cancellationToken);

        Task<Domain.ProductImage> GetById(Guid id, CancellationToken cancellationToken);
    }
}