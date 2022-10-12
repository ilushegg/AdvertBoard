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
}