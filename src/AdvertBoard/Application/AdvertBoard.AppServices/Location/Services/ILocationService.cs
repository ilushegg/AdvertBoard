using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.Location.Services
{
    public interface ILocationService
    {
        Guid Add(string country, string city, string street, string house, string flat, string lat, string lon, CancellationToken cancellation);
        Task<Guid> AddAsync(string country, string city, string street, string house, string flat, string lat, string lon, CancellationToken cancellation);
        Task DeleteAsync(Guid locationId, CancellationToken cancellation);
        Task<Guid> EditAsync(Guid locationId, string country, string city, string street, string number, CancellationToken cancellation);
        Task<IReadOnlyCollection<LocationDto>> GetAll(int take, int skip, CancellationToken cancellation);
        Task<LocationDto> GetById(Guid locationId, CancellationToken cancellation);
    }
}