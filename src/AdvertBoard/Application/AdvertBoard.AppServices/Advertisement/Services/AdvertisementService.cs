using AdvertBoard.AppServices.Category.Repositories;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.AppServices.AdvertisementImage.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using System.IO;
using AdvertBoard.AppServices.User.Repositories;

namespace AdvertBoard.AppServices.Advertisement.Services;

/// <inheritdoc />
public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAdvertisementImageRepository _productImageRepository;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AdvertisementService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
    public AdvertisementService(IAdvertisementRepository productRepository, ICategoryRepository categoryRepository, IAdvertisementImageRepository productImageRepository, IUserRepository userRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _productImageRepository = productImageRepository;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<ProductDto>> GetAll(int take, int skip, CancellationToken cancellation)
    {
        return _productRepository.GetAll(take, skip, cancellation);
    }

    /// <inheritdoc />
    public Task<IReadOnlyCollection<ProductDto>> GetAllFiltered(ProductFilterRequest request, CancellationToken cancellation)
    {
        return _productRepository.GetAllFiltered(request, cancellation);
    }

    public Guid Add(string name, string description, decimal price, Guid categoryId, Domain.User user, CancellationToken cancellation = default)
    {
        var product = new Domain.Advertisement
        {
            Name = name,
            Description = description,
            Price = price,
            User = user,
            DateTimeCreated = DateTime.UtcNow,
            DateTimeUpdated = DateTime.UtcNow,
            DateTimePublish = DateTime.UtcNow
        };

        var category = _categoryRepository.FindById(categoryId, cancellation);

        product.CategoryId = category.Result.Key;

        _productRepository.AddAsync(product, cancellation);
        return product.Id;
    }

    /// <inheritdoc />
    public async Task<Guid> AddAsync(string name, string description, decimal price, Guid categoryId, Domain.User user, CancellationToken cancellation = default) 
    {
        var product = new Domain.Advertisement
        {
            Name = name,
            Description = description,
            Price = price, 
            User = user,
            DateTimeCreated = DateTime.UtcNow,
            DateTimeUpdated = DateTime.UtcNow,
            DateTimePublish = DateTime.UtcNow
        };
            
        var category = await _categoryRepository.FindById(categoryId, cancellation);

        product.CategoryId = category.Key;

        await _productRepository.AddAsync(product, cancellation);
        return product.Id;
    }

    public async Task<Guid> EditAsync(Guid productId, string name, string description, decimal price, Guid categoryId, CancellationToken cancellation)
    {
        var product = await _productRepository.GetById(productId, cancellation);
        if (product == null)
        {
            throw new Exception($"Товар с идентификатором '{productId}' не найден");
        }
        else
        {
            product.Name = name;
            product.Price = price;
            product.Description = description;
            product.DateTimeUpdated = DateTime.UtcNow;
            await _productRepository.EditAsync(product, cancellation);
            return product.Id;
        }
    }

    public async Task<bool> DeleteAsync(Guid productId, CancellationToken cancellation)
    {
        var product = await _productRepository.GetById(productId, cancellation);
        if(product == null)
        {
            throw new Exception($"Товар с идентификатором '{productId}' не найден");
        }
        else
        {
            return await _productRepository.DeleteAsync(product, cancellation);
        }
         
    }

    public async Task<FullAdvertisementDto> GetById(Guid productId, CancellationToken cancellation)
    {
        var ad = await _productRepository.GetById(productId, cancellation);
        var images = await _productImageRepository.GetAllByProduct(productId, cancellation);
        var user = await _userRepository.FindById(ad.UserId, cancellation);
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
            Images = imageList,
            DateTimeCreated = $"{ad.DateTimeCreated.ToLocalTime().ToString("D")}",
            DateTimeUpdated = $"{ad.DateTimeUpdated.ToString("D")}",
            AuthorId = ad.UserId,
            AuthorName = user.Name,
            AuthorAvatar = ""
        };
        return result;
    }
}