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
    public AdvertisementRepository(IRepository<Domain.Advertisement> repository, IRepository<AdvertisementDto> repositoryDto)
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
                DateTimeCreated = $"{p.DateTimeCreated.ToString("f")}"
            })
            .Take(take).Skip(skip).ToListAsync(cancellation);
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

    public void Add(Domain.Advertisement product, CancellationToken cancellation)
    {
        _repository.Add(product);
    }

    public async Task<bool> DeleteAsync(Domain.Advertisement product, CancellationToken cancellation)
    {
        var result = _repository.DeleteAsync(product);
        return true;
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