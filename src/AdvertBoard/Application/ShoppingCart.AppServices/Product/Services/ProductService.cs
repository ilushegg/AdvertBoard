using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.Product.Services;

/// <inheritdoc />
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ProductService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
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
    public Task<bool> AddAsync(string name, string description, decimal price, Guid categoryId, CancellationToken cancellation)
    {
        var product = new Domain.Product()
        {
            Name = name,
            Description = description,
            Price = price, 
            CategoryId = categoryId
        };
        return _productRepository.AddAsync(product, cancellation);
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
            product.CategoryId = categoryId;

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
}