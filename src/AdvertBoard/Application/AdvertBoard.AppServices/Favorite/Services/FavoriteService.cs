using AdvertBoard.AppServices.AdvertisementImage.Repositories;
using AdvertBoard.AppServices.Location.Repositories;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Web;

namespace AdvertBoard.AppServices.Favorite.Services;

/// <inheritdoc />
public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IAdvertisementImageRepository _advertisementImageRepository;
    private readonly ILocationRepository _locationRepository;


    public FavoriteService(IFavoriteRepository favoriteRepository, IAdvertisementRepository productRepository, IAdvertisementImageRepository advertisementImageRepository, ILocationRepository locationRepository)
    {
        _favoriteRepository = favoriteRepository;
        _advertisementRepository = productRepository;
        _advertisementImageRepository = advertisementImageRepository;
        _locationRepository = locationRepository;
    }

    /// <inheritdoc />
    public async Task<GetPagedResultDto<AdvertisementDto>> GetAllAsync(int take, int skip, Guid userId, CancellationToken cancellationToken)
    {
        var favorites = await _favoriteRepository.GetAllAsync(skip, take, userId, cancellationToken);
        List<AdvertisementDto> advertisementsDto = new List<AdvertisementDto>();
        foreach(var fav in favorites)
        {
            var advertisement = await _advertisementRepository.GetById(fav.AdvertisementId, cancellationToken);
            var images = await _advertisementImageRepository.GetAllByProduct(advertisement.Id, cancellationToken);
            var location = await _locationRepository.GetByIdAsync(advertisement.LocationId, cancellationToken);
            var imageList = new List<string>();
            foreach (var image in images)
            {
                byte[] byteImage = File.ReadAllBytes(image.FilePath);
                imageList.Add("data:image/png;base64," + Convert.ToBase64String(byteImage));
            }
            advertisementsDto.Add(new AdvertisementDto
            {
                Id = advertisement.Id,
                Name = advertisement.Name,
                Description = advertisement.Description,
                Price = advertisement.Price,
                CategoryId = advertisement.CategoryId,
                DateTimeCreated = $"{advertisement.DateTimeCreated.ToString("f")}",
                Images = imageList,
                isFavorite = true,
                LocationQuery = location.City
            });
        }
        return new GetPagedResultDto<AdvertisementDto>
        {
            Offset = skip,
            Limit = take,
            Total = await _favoriteRepository.GetAllCount(userId, cancellationToken),
            Items = advertisementsDto
        };


    }
    
    public async Task<bool> IsAdvertisementFavorite(Guid advertisementId, Guid userId, CancellationToken cancellationToken)
    {
        var exFavorite = await _favoriteRepository.GetByAdvertisementId(advertisementId, userId, cancellationToken);
        if (exFavorite == null)
        {
            return false;
        }
        return true;


    }

    /// <inheritdoc />
    public async Task<Guid> AddAsync(Guid advertisementId, Guid userId, CancellationToken cancellationToken)
    {
        var exFavorite = await _favoriteRepository.GetByAdvertisementId(advertisementId, userId, cancellationToken);
        if (exFavorite == null)
        {
            var favorite = new Domain.Favorite
            {
                Id = new Guid(),
                AdvertisementId = advertisementId,
                UserId = userId
            };
            return await _favoriteRepository.AddAsync(favorite, cancellationToken);
        }
        else
        {
            return Guid.Empty;
        }

    }

    public async Task<Guid> GetByAdvertisementId(Guid advertisementId, Guid userId, CancellationToken cancellationToken)
    {
        var favorite = await _favoriteRepository.GetByAdvertisementId(advertisementId, userId, cancellationToken);
        return (favorite.Id);
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return _favoriteRepository.DeleteAsync(id, cancellationToken);
    }

    
}