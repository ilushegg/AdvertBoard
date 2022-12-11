using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;

namespace AdvertBoard.DataAccess.EntityConfigurations.Advertisement;

/// <inheritdoc />
public class AdvertisementRepository : IAdvertisementRepository
{
    private readonly IRepository<Domain.Advertisement> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AdvertisementRepository"/>.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    public AdvertisementRepository(IRepository<Domain.Advertisement> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<AdvertisementDto>> GetAll(int take, int skip, CancellationToken cancellation)
    {
        return await _repository.GetAll()
            .Select(p => new AdvertisementDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.Category.Id,
                Price = p.Price,
                LocationQuery = p.Location.City,
                DateTimeCreated = $"{p.DateTimeCreated.ToString("f")}"
            })
            .Take(take).Skip(skip).ToListAsync(cancellation);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<AdvertisementDto>> GetAllByAuthor(int take, int skip, Guid userId, CancellationToken cancellation)
    {
        return await _repository.GetAll().Where(ad => ad.UserId == userId)
            .Select(p => new AdvertisementDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.Category.Id,
                Price = p.Price,
                LocationQuery = p.Location.City,
                DateTimeCreated = $"{p.DateTimeCreated.ToString("f")}"
            })
            .Skip(skip).Take(take).ToListAsync(cancellation);
    }

    public async Task<int> GetAllByAuthorCount(Guid userId, CancellationToken cancellation)
    {
        return await _repository.GetAll().Where(ad => ad.UserId == userId).CountAsync();
 
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<AdvertisementDto>> GetAllFiltered(ProductFilterRequest request,
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
            
        return await query.Select(p => new AdvertisementDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId
            }).ToListAsync(cancellation);
    }

    public async Task<bool> AddAsync(Domain.Advertisement product, CancellationToken cancellation)
    {
        var result = _repository.AddAsync(product);
        return true;
    }

    public Guid Add(Domain.Advertisement product)
    {
        _repository.Add(product);
        return product.Id;
    }

    public async Task DeleteAsync(Domain.Advertisement product, CancellationToken cancellation)
    {
        await _repository.DeleteAsync(product);

    }

    public async Task<bool> EditAsync(Domain.Advertisement product, CancellationToken cancellation)
    {
        var result = _repository.UpdateAsync(product);
        return true;
    }

    public async Task<Domain.Advertisement> GetById(Guid productId, CancellationToken cancellation)
    {
        var result = await _repository.GetByIdAsync(productId);

        return result;
    }
}