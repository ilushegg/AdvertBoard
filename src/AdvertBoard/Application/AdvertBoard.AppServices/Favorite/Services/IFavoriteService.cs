using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.Favorite.Services
{
    public interface IFavoriteService
    {
        Task<Guid> AddAsync(Guid advertisementId, Guid userId, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<GetPagedResultDto<AdvertisementDto>> GetAllAsync(int take, int skip, Guid userId, CancellationToken cancellationToken);

        Task<bool> IsAdvertisementFavorite(Guid advertisementId, Guid userId, CancellationToken cancellationToken);

        Task<Guid> GetByAdvertisementId(Guid advertisementId, Guid userId, CancellationToken cancellationToken);
    }
}