using Microsoft.EntityFrameworkCore;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using System;
using System.Linq.Expressions;
using System.Globalization;
using Microsoft.IdentityModel.Tokens;

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
        var advertisements = _repository.GetAll();
        return await advertisements.OrderByDescending(ad => ad.DateTimeCreated)
            .Select(p => new AdvertisementDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.Category.Id != null ? p.Category.Id : p.CategoryId,
                Price = p.Price,
                LocationQuery = p.Location.City != null ? p.Location.City : "" ,
                DateTimeCreated = $"{p.DateTimeCreated.ToString("f")}",
                Status = p.Status
            })
            .Take(take).Skip(skip).ToListAsync(cancellation);
    }

    public async Task<IReadOnlyCollection<Domain.Advertisement>> GetAllAsync(int take, int skip, CancellationToken cancellation)
    {
        var advertisements = _repository.GetAll();
       return await advertisements.ToListAsync(cancellation);
    }
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<AdvertisementDto>> GetAllByAuthor(int take, int skip, Guid userId, CancellationToken cancellation)
    {
        return await _repository.GetAll().OrderByDescending(ad => ad.DateTimeCreated).Where(ad => ad.UserId == userId)
            .Select(p => new AdvertisementDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.Category.Id,
                Price = p.Price,
                LocationQuery = p.Location.City,
                DateTimeCreated = $"{p.DateTimeCreated.ToString("f")}",
                Status = p.Status
            })
            .Skip(skip).Take(take).ToListAsync(cancellation);
    }

    public async Task<int> GetAllCount(Expression<Func<Domain.Advertisement, bool>> predicate, CancellationToken cancellation)
    {
        return await _repository.GetAll().Where(predicate).CountAsync();
 
    }
  


    public async Task<IReadOnlyCollection<AdvertisementDto>> GetWhere(int skip, int take, string[]? query, Guid? categoryId, string? location, decimal? fromPrice, decimal? toPrice, string? sort,  CancellationToken cancellation)
    {
        var advertisements = _repository.GetAll();

        if(sort == "asc")
        {
            advertisements = advertisements.OrderBy(ad => ad.Price);

        }
        else if (sort == "desc")
        {
            advertisements = advertisements.OrderByDescending(ad => ad.Price);

        }

            if (!query.IsNullOrEmpty())
            {
        foreach (var q in query)
        {
                advertisements = advertisements.Where(p => p.Name.ToLower().Contains(q.ToLower()));
        }
            }

        if (!string.IsNullOrWhiteSpace(location))
        {
            advertisements = advertisements.Where(p => p.Location.LocationQueryString.ToLower().Contains(location.ToLower()));
        }

        if (categoryId != null)
        {
            advertisements = advertisements.Where(p => p.CategoryId == categoryId || p.Category.ParentCategoryId == categoryId);
        }

        if (fromPrice != null)
        {
            advertisements = advertisements.Where(p => p.Price >= fromPrice);
        }

        if (toPrice != null)
        {
            advertisements = advertisements.Where(p => p.Price <= toPrice);
        }

        advertisements = advertisements.Where(p => p.Status == "public");

        return await advertisements.Select(p => new AdvertisementDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            CategoryId = p.Category.Id,
            Price = p.Price,
            LocationQuery = p.Location.City,
            DateTimeCreated = $"{p.DateTimeCreated.ToString("f")}",
            Status = p.Status
        })/*.Skip(skip).Take(take)*/.ToListAsync(cancellation);
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

    public async Task<Guid> EditAsync(Domain.Advertisement product, CancellationToken cancellation)
    {
        var result = _repository.UpdateAsync(product);
        return product.Id;
    }

    public async Task<Domain.Advertisement> GetById(Guid productId, CancellationToken cancellation)
    {
        var result = await _repository.GetByIdAsync(productId);

        return result;
    }
}