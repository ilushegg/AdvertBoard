using AdvertBoard.Domain;

namespace AdvertBoard.DataAccess.EntityConfigurations.Image
{
    public interface IImageRepository
    {
        Task AddAsync(Domain.Image image, CancellationToken cancellationToken);
        Task Delete(Domain.Image image, CancellationToken cancellationToken);
        Task EditAsync(Domain.Image image, CancellationToken cancellationToken);
        Task<Domain.Image> GetById(Guid id, CancellationToken cancellationToken);
    }
}