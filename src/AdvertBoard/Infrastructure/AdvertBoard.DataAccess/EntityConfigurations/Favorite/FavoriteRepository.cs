using Microsoft.EntityFrameworkCore;

using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using AdvertBoard.Domain;
using AdvertBoard.AppServices.Favorite;

namespace AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart;

/// <inheritdoc />
public class FavoriteRepository : IFavoriteRepository
{
    private readonly IRepository<Domain.Favorite> _repository;
    private readonly IRepository<Domain.Advertisement> _advertisementRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="ShoppingCartRepository"/>.
    /// </summary>
    /// <param name="repository">Базовый репозиторий.</param>
    public FavoriteRepository(IRepository<Domain.Favorite> repository, IRepository<Domain.Advertisement> advertisementRepository)
    {
        _repository = repository;
        _advertisementRepository = advertisementRepository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<FavoriteDto>> GetAllAsync(int skip, int take, Guid userId, CancellationToken cancellationToken)
    {
        return await _repository.GetAll().Where(s => s.UserId == userId).Select(f => new FavoriteDto
        {
            Id = f.Id,
            AdvertisementId = f.AdvertisementId,
            UserId = f.UserId
        }).Skip(skip).Take(take).ToListAsync();
    }


    /// <inheritdoc />
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existingFavorite = await _repository.GetByIdAsync(id);

        if (existingFavorite == null)
        {
            throw new InvalidOperationException($"Избранного с идентификатором {id} не найдено!");
        }

        await _repository.DeleteAsync(existingFavorite);
    }

    public async Task<Guid> AddAsync(Domain.Favorite favorite, CancellationToken cancellationToken)
    {

        await _repository.AddAsync(favorite);
        return favorite.Id;
    }

    public async Task<Domain.Favorite> GetByAdvertisementId(Guid advertisementId, Guid userId, CancellationToken cancellationToken)
    {
        return await _repository.GetAll().Where(s => s.AdvertisementId == advertisementId && s.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task<int> GetAllCount(Guid userId, CancellationToken cancellation)
    {
        return await _repository.GetAll().Where(ad => ad.UserId == userId).CountAsync();

    }

}