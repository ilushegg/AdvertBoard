using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.DataAccess.EntityConfigurations.Category;
using AdvertBoard.Domain;

namespace AdvertBoard.AppServices.Product.Services;

/// <inheritdoc />
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ProductService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
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

    /// <inheritdoc />
    public async Task<Guid> AddAsync(string name, string description, decimal price, string categoryName, User user, CancellationToken cancellation = default) 
    {
        var product = new Domain.Product
        {
            Name = name,
            Description = description,
            Price = price, 
            User = user,
            DateTimeCreated = DateTime.UtcNow,
            DateTimeUpdated = DateTime.UtcNow,
            DateTimePublish = DateTime.UtcNow
        };
            
        var category = await _categoryRepository.FindByName(categoryName, cancellation);

        if (category == null)
        {
            category = new Category
            {
                Name = categoryName
                        
            };
            _categoryRepository.Add(category, cancellation);
        }

        product.Category = category;

        _productRepository.Add(product, cancellation);
        return product.Id;
    }

    public async Task<bool> EditAsync(Guid productId, string name, string description, decimal price, Guid categoryId, CancellationToken cancellation)
    {
        var product = await _productRepository.FindById(productId, cancellation);
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
            return await _productRepository.EditAsync(product, cancellation);
        }
    }

    public async Task<bool> DeleteAsync(Guid productId, CancellationToken cancellation)
    {
        var product = await _productRepository.FindById(productId, cancellation);
        if(product == null)
        {
            throw new Exception($"Товар с идентификатором '{productId}' не найден");
        }
        else
        {
            return await _productRepository.DeleteAsync(product, cancellation);
        }
         
    }

    public Task<Domain.Product> Get(Guid productId, CancellationToken cancellation)
    {
        return _productRepository.FindById(productId, cancellation);
    }
}