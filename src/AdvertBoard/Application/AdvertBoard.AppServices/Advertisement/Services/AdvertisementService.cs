using AdvertBoard.AppServices.Category.Repositories;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.AppServices.AdvertisementImage.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using System.IO;
using AdvertBoard.AppServices.User.Repositories;
using static System.Net.Mime.MediaTypeNames;
using AdvertBoard.DataAccess.EntityConfigurations.UserAvatar;
using AdvertBoard.AppServices.Location.Repositories;
using System.Globalization;
using System.Linq;

namespace AdvertBoard.AppServices.Advertisement.Services;

/// <inheritdoc />
public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAdvertisementImageRepository _productImageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserAvatarRepository _userAvatarRepository;
    private readonly ILocationRepository _locationRepository;

    public AdvertisementService(IAdvertisementRepository productRepository, ICategoryRepository categoryRepository, IAdvertisementImageRepository productImageRepository, IUserRepository userRepository, IUserAvatarRepository userAvatarRepository, ILocationRepository locationRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _productImageRepository = productImageRepository;
        _userRepository = userRepository;
        _userAvatarRepository = userAvatarRepository;
        _locationRepository = locationRepository;
    }

    /// <inheritdoc />
    public async Task<GetPagedResultDto<AdvertisementDto>> GetAll(int take, int skip, CancellationToken cancellation)
    {
        var advertisements = await _productRepository.GetAll(take, skip, cancellation);
        var total = await _productRepository.GetAllCount(ad => ad != null, cancellation);

        foreach (var ad in advertisements)
        {
            var images = await _productImageRepository.GetAllByProduct(ad.Id, cancellation);
            var imageList = new List<string>();
            foreach (var image in images)
            {
                byte[] byteImage = File.ReadAllBytes(image.FilePath);
                imageList.Add("data:image/png;base64," + Convert.ToBase64String(byteImage));
            }
            ad.Images = imageList;

        }

        return new GetPagedResultDto<AdvertisementDto>
        {
            Offset = skip,
            Limit = take,
            Total = total,
            Items = advertisements
        };

    }

    /// <inheritdoc />
    public async Task<GetPagedResultDto<AdvertisementDto>> GetAllBySearch(int skip, int take, string? query, Guid? categoryId, string? location, decimal? fromPrice, decimal? toPrice, string? sort, CancellationToken cancellationToken)
    {
        //var total = await _productRepository.GetAllCount(ad => ((query == null) ? ad.Name!=null : ad.Name.ToLower().Contains(query.ToLower())) 
        //                    && ((categoryId == null) ? ad.CategoryId!=null : ad.CategoryId == categoryId || ad.Category.ParentCategoryId == categoryId) 
        //                    && ((location == null) ? ad.Location.LocationQueryString != null : ad.Location.LocationQueryString.ToLower().Contains(location.ToLower()))
        //                    && ((fromPrice == null) ? ad.Price != null : ad.Price >= fromPrice)
        //                    && ((toPrice == null) ? ad.Price != null : ad.Price <= toPrice)
        //                    && (ad.Status == "public"), cancellationToken) ;
        var advertisements = await _productRepository.GetWhere(skip, take, query == null ? null : query.Split(" "), categoryId, location, fromPrice, toPrice, sort, cancellationToken);
        var total = advertisements.Count();
        advertisements = advertisements.Skip(skip).Take(take).ToList();
        foreach (var ad in advertisements)
        {
            var images = await _productImageRepository.GetAllByProduct(ad.Id, cancellationToken);
            var imageList = new List<string>();
            foreach (var image in images)
            {
                byte[] byteImage = File.ReadAllBytes(image.FilePath);
                imageList.Add("data:image/png;base64," + Convert.ToBase64String(byteImage));
            }
            ad.Images = imageList;

        }

        return new GetPagedResultDto<AdvertisementDto>
        {
            Offset = skip,
            Limit = take,
            Total = total,
            Items = advertisements
        };

    }

    /// <inheritdoc />
    public async Task<GetPagedResultDto<AdvertisementDto>> GetAllByAuthor(int skip, int take, Guid userId, CancellationToken cancellation)
    {
        var total = await _productRepository.GetAllCount(ad => ad.UserId == userId, cancellation);
        var advertisements = await _productRepository.GetAllByAuthor(take, skip, userId, cancellation);
        foreach (var ad in advertisements)
        {
            var images = await _productImageRepository.GetAllByProduct(ad.Id, cancellation);
            var imageList = new List<string>();
            foreach (var image in images)
            {
                byte[] byteImage = File.ReadAllBytes(image.FilePath);
                imageList.Add("data:image/png;base64," + Convert.ToBase64String(byteImage));
            }
            ad.Images = imageList;
        }

        return new GetPagedResultDto<AdvertisementDto>
        {
            Offset = skip,
            Limit = take,
            Total = total,
            Items = advertisements
        };

    }


    /// <inheritdoc />
    public Guid Add(string name, string description, decimal price, Guid categoryId, Guid locationId, Guid userId)
    {
        var advertisement = new Domain.Advertisement
        {
            Name = name,
            Description = description,
            Price = price,
            UserId = userId,
            LocationId = locationId,
            DateTimeCreated = DateTime.UtcNow,
            DateTimeUpdated = DateTime.UtcNow,
            DateTimePublish = DateTime.UtcNow,
            Status = "public"
        };

        var category = _categoryRepository.FindById(categoryId);

        advertisement.CategoryId = category.Id;


        _productRepository.Add(advertisement);
        return advertisement.Id;
    }

    /// <inheritdoc />
    public async Task<Guid> AddAsync(string name, string description, decimal price, Guid categoryId, Guid locationId, Domain.User user, CancellationToken cancellation) 
    {
        var advertisement = new Domain.Advertisement
        {
            Name = name,
            Description = description,
            Price = price, 
            User = user,
            LocationId = locationId,
            DateTimeCreated = DateTime.UtcNow,
            DateTimeUpdated = DateTime.UtcNow,
            DateTimePublish = DateTime.UtcNow,
            Status = "public"
        };
            
        var category = await _categoryRepository.FindByIdAsync(categoryId, cancellation);

        advertisement.CategoryId = category.Key;

        await _productRepository.AddAsync(advertisement, cancellation);
        return advertisement.Id;
    }


    /// <inheritdoc />
    public async Task<(Guid adId, Guid locId)> EditAsync(Guid productId, string name, string description, decimal price, Guid categoryId, CancellationToken cancellation)
    {
        var advertisement = await _productRepository.GetById(productId, cancellation);
        if (advertisement == null)
        {
            throw new Exception($"Объявление с идентификатором '{productId}' не найдено");
        }
        else
        {
            advertisement.Name = name;
            advertisement.Price = price;
            advertisement.Description = description;
            advertisement.CategoryId = categoryId;
            advertisement.DateTimeUpdated = DateTime.UtcNow;
            await _productRepository.EditAsync(advertisement, cancellation);

            return ( advertisement.Id, advertisement.LocationId);
        }
    }

    public async Task<Guid> EditPublicAsync(Guid adId, string status, CancellationToken cancellation)
    {
        var advertisement = await _productRepository.GetById(adId, cancellation);
        if (advertisement == null)
        {
            throw new Exception($"Объявление с идентификатором '{adId}' не найдено");
        }
        else
        {
            advertisement.Status = status;
            await _productRepository.EditAsync(advertisement, cancellation);

            return advertisement.Id;
        }
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid productId, CancellationToken cancellation)
    {
        var advertisement = await _productRepository.GetById(productId, cancellation);
        if(advertisement == null)
        {
            throw new Exception($"Товар с идентификатором '{productId}' не найден");
        }
        else
        {
            await _productRepository.DeleteAsync(advertisement, cancellation);
        }
         
    }

    /// <inheritdoc />
    public async Task<FullAdvertisementDto> GetById(Guid advertisementId, CancellationToken cancellation)
    {
        try
        {
            var ad = await _productRepository.GetById(advertisementId, cancellation);
            var location = await _locationRepository.GetByIdAsync(ad.LocationId, cancellation);
            var images = await _productImageRepository.GetAllByProduct(advertisementId, cancellation);
            var user = await _userRepository.FindById(ad.UserId, cancellation);
            var userAvatar = await _userAvatarRepository.GetByUserIdAsync(user.Id, cancellation);
            var imageList = new List<Tuple<Guid, string>>();
            foreach (var image in images)
            {
                byte[] byteImage = File.ReadAllBytes(image.FilePath);
                imageList.Add(Tuple.Create(image.ImageId, "data:image/png;base64," + Convert.ToBase64String(byteImage)));
            }
            var result = new FullAdvertisementDto
            {
                Id = ad.Id,
                Name = ad.Name,
                Description = ad.Description,
                Price = ad.Price,
                CategoryId = ad.CategoryId,
                Images = imageList,
                DateTimeCreated = $"{ad.DateTimeCreated.ToString("f")}",
                DateTimeUpdated = $"{ad.DateTimeUpdated.ToString("f")}",
                AuthorId = ad.UserId,
                AuthorName = user.Name,
                AuthorAvatar = userAvatar != null ? "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(userAvatar.FilePath)) : "",
                AuthorNumber = user.Mobile,
                AuthorRegisterDate = $"{user.CreateDate.ToString("d")}",
                LocationQueryString = location.LocationQueryString,
                LocationLat = location.Lat,
                LocationLon = location.Lon,
                Status = ad.Status
            };
            return result;

        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}