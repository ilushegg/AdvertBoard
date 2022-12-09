using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.Location.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;

namespace AdvertBoard.DataAccess.EntityConfigurations.Location;

/// <inheritdoc />
public class LocationRepository : ILocationRepository
{
    private readonly IRepository<Domain.Location> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="LocationRepository"/>.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    public LocationRepository(IRepository<Domain.Location> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<LocationDto>> GetAll(CancellationToken cancellation)
    {
        return await _repository.GetAll()
            .Select(p => new LocationDto
            {
                Id = p.Id,
                Country = p.Country,
                City = p.City,
                Street = p.Street,
                Number = p.Number,
            }).ToListAsync(cancellation);
    }



    public async Task AddAsync(Domain.Location location, CancellationToken cancellation)
    {
        await _repository.AddAsync(location);
    }

    public Guid Add(Domain.Location location)
    {
        _repository.Add(location);
        return location.Id;
    }

    public async Task DeleteAsync(Domain.Location location, CancellationToken cancellation)
    {
        await _repository.DeleteAsync(location);
    }

    public async Task EditAsync(Domain.Location location, CancellationToken cancellation)
    {
        await _repository.UpdateAsync(location);
    }

    public async Task<Domain.Location> GetById(Guid locationId, CancellationToken cancellation)
    {
        return await _repository.GetByIdAsync(locationId);
    }
}