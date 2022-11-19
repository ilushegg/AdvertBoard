using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;

namespace AdvertBoard.DataAccess.EntityConfigurations.Product;

/// <inheritdoc />
public class ProductRepository : IProductRepository
{
    private readonly IRepository<Domain.Product> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ProductRepository"/>.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    public ProductRepository(IRepository<Domain.Product> repository, IRepository<ProductDto> repositoryDto)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ProductDto>> GetAll(int take, int skip, CancellationToken cancellation)
    {
        return await _repository.GetAll()
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.Category.Id,
                Price = p.Price
            })
            .Take(take).Skip(skip).ToListAsync(cancellation);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ProductDto>> GetAllFiltered(ProductFilterRequest request,
        CancellationToken cancellation)
    {
        var query = _repository.GetAll();

        if (request.Id.HasValue)
        {
            query = query.Where(p => p.Id == request.Id);
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(request.Name.ToLower()));
        }
            
        return await query.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId
            }).ToListAsync(cancellation);
    }

    public async Task<bool> AddAsync(Domain.Product product, CancellationToken cancellation)
    {
        var result = _repository.AddAsync(product);
        return true;
    }

    public void Add(Domain.Product product, CancellationToken cancellation)
    {
        _repository.Add(product);
    }

    public async Task<bool> DeleteAsync(Domain.Product product, CancellationToken cancellation)
    {
        var result = _repository.DeleteAsync(product);
        return true;
    }

    public async Task<bool> EditAsync(Domain.Product product, CancellationToken cancellation)
    {
        var result = _repository.UpdateAsync(product);
        return true;
    }

    public async Task<Domain.Product> FindById(Guid productId, CancellationToken cancellation)
    {
        var result = await _repository.GetByIdAsync(productId);
        return result;
    }
}