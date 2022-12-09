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

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<AdvertisementDto>> GetAllByAuthor(int take, int skip, Guid userId, CancellationToken cancellation)
    {
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

        return advertisements;

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
    public async Task<Guid> AddAsync(string name, string description, decimal price, Guid categoryId, Guid locationId, Domain.User user, CancellationToken cancellation = default) 
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

    public async Task<Guid> EditAsync(Guid productId, string name, string description, decimal price, Guid categoryId, CancellationToken cancellation)
    {
        var advertisement = await _productRepository.GetById(productId, cancellation);
        if (advertisement == null)
        {
            throw new Exception($"Товар с идентификатором '{productId}' не найден");
        }
        else
        {
            advertisement.Name = name;
            advertisement.Price = price;
            advertisement.Description = description;
            advertisement.DateTimeUpdated = DateTime.UtcNow;
            await _productRepository.EditAsync(advertisement, cancellation);
            return advertisement.Id;
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
            var location = await _locationRepository.GetById(ad.LocationId, cancellation);
            var images = await _productImageRepository.GetAllByProduct(advertisementId, cancellation);
            var user = await _userRepository.FindById(ad.UserId, cancellation);
            var userAvatar = await _userAvatarRepository.GetByUserIdAsync(user.Id, cancellation);
            var imageList = new List<string>();
           
            foreach (var image in images)
            {
                byte[] byteImage = File.ReadAllBytes(image.FilePath);
                imageList.Add("data:image/png;base64," + Convert.ToBase64String(byteImage));
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
                AuthorAvatar = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(userAvatar.FilePath)),
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