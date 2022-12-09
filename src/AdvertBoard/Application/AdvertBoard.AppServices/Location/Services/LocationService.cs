using AdvertBoard.AppServices.Category.Repositories;

using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using System.IO;
using AdvertBoard.AppServices.User.Repositories;
using static System.Net.Mime.MediaTypeNames;
using AdvertBoard.AppServices.Location.Repositories;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AdvertBoard.AppServices.Location.Services;

/// <inheritdoc />
public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;


    /// <summary>
    /// Инициализирует экземпляр <see cref="LocationService"/>.
    /// </summary>
    /// <param name="locationRepository"></param>
    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;

    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<LocationDto>> GetAll(int take, int skip, CancellationToken cancellation)
    {
        var locations = await _locationRepository.GetAll(cancellation);


        return locations;

    }


    public Guid Add(string country, string city, string street, string house, string flat, string query, string lat, string lon)
    {
        var location = new Domain.Location
        {
            Id = new Guid(),
            Country = country,
            City = city,
            Street = street,
            House = house,
            Number = flat,
            LocationQueryString = query,
            Lat = lat,
            Lon = lon
        };

        return _locationRepository.Add(location);
    }

    /// <inheritdoc />
    public async Task<Guid> AddAsync(string country, string city, string street, string house, string flat, string query, string lat, string lon, CancellationToken cancellation)
    {
        var location = new Domain.Location
        {
            Country = country,
            City = city,
            Street = street,
            House = house,
            Number = flat,
            LocationQueryString = query,
            Lat = lat,
            Lon = lon
        };

        await _locationRepository.AddAsync(location, cancellation);
        return location.Id;
    }

    public async Task<Guid> EditAsync(Guid locationId, string country, string city, string street, string number, CancellationToken cancellation)
    {
        var location = await _locationRepository.GetById(locationId, cancellation);
        if (location == null)
        {
            throw new Exception($"Локация с идентификатором '{locationId}' не найдена");
        }
        else
        {
            location.Country = country;
            location.City = city;
            location.Street = street;
            location.Number = number;
            await _locationRepository.EditAsync(location, cancellation);
            return location.Id;
        }
    }

    public async Task DeleteAsync(Guid locationId, CancellationToken cancellation)
    {
        var location = await _locationRepository.GetById(locationId, cancellation);
        if (location == null)
        {
            throw new Exception($"Локация с идентификатором '{locationId}' не найдена");
        }
        else
        {
            await _locationRepository.DeleteAsync(location, cancellation);
        }

    }

    public async Task<LocationDto> GetById(Guid locationId, CancellationToken cancellation)
    {
        try
        {
            var location = await _locationRepository.GetById(locationId, cancellation);


            var result = new LocationDto
            {
                Id = location.Id,
                Country = location.Country,
                City = location.City,
                Street = location.Street,
                Number = location.Number

            };
            return result;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}