using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.Location.Repositories
{
    public interface ILocationRepository
    {
        void Add(Domain.Location location);
        Task AddAsync(Domain.Location location, CancellationToken cancellation);
        Task DeleteAsync(Domain.Location location, CancellationToken cancellation);
        Task EditAsync(Domain.Location location, CancellationToken cancellation);
        Task<IReadOnlyCollection<LocationDto>> GetAll(CancellationToken cancellation);
        Task<Domain.Location> GetById(Guid locationId, CancellationToken cancellation);
    }
}