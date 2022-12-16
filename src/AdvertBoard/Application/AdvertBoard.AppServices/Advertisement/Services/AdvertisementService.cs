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

    /// <summary>
    /// Инициализирует экземпляр <see cref="AdvertisementService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
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
    public async Task<IReadOnlyCollection<AdvertisementDto>> GetAll(int take, int skip, CancellationToken cancellation)
    {
        var advertisements = await _productRepository.GetAll(take, skip, cancellation);
        foreach(var ad in advertisements)
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

        return advertisements;

    }

    public async Task<GetPagedResultDto<AdvertisementDto>> GetAllBySearch(int skip, int take, string? query, Guid? categoryId, string? city, decimal? fromPrice, decimal? toPrice, string? sort, CancellationToken cancellationToken)
    {
        var total = await _productRepository.GetAllCount(ad => ((query == null) ? ad.Name!=null : ad.Name.ToLower().Contains(query.ToLower())) 
                            && ((categoryId == null) ? ad.CategoryId!=null : ad.CategoryId == categoryId || ad.Category.ParentCategoryId != null) 
                            && ((city == null) ? ad.Location.City != null : ad.Location.City == city)
                            && ((fromPrice == null) ? ad.Price != null : ad.Price >= fromPrice)
                            && ((toPrice == null) ? ad.Price != null : ad.Price <= toPrice), cancellationToken) ;
        var advertisements = await _productRepository.GetWhere(skip, take, query, categoryId, city, fromPrice, toPrice, sort, cancellationToken);
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
    public Task<IReadOnlyCollection<AdvertisementDto>> GetAllFiltered(ProductFilterRequest request, CancellationToken cancellation)
    {
        return _productRepository.GetAllFiltered(request, cancellation);
    }

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
            DateTimePublish = DateTime.UtcNow
        };

        var category = _categoryRepository.FindById(categoryId);

        advertisement.CategoryId = category.Key;


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
            DateTimePublish = DateTime.UtcNow
        };
            
        var category = await _categoryRepository.FindByIdAsync(categoryId, cancellation);

        advertisement.CategoryId = category.Key;

        await _productRepository.AddAsync(advertisement, cancellation);
        return advertisement.Id;
    }

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
                AuthorRegisterDate = $"{user.CreateDate.ToString("D")}",
                LocationQueryString = location.LocationQueryString,
                LocationLat = location.Lat,
                LocationLon = location.Lon
            };
            return result;

        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}