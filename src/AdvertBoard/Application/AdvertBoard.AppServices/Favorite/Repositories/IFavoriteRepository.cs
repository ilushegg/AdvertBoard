using AdvertBoard.Contracts;
using AdvertBoard.Domain;

namespace AdvertBoard.AppServices.Favorite
{
    public interface IFavoriteRepository
    {
        Task<Guid> AddAsync(Domain.Favorite favorite, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<FavoriteDto>> GetAllAsync(int skip, int take, Guid userId, CancellationToken cancellationToken);


        Task<Domain.Favorite> GetByAdvertisementId(Guid advertisementId, Guid userId, CancellationToken cancellationToken);

        Task<int> GetAllCount(Guid userId, CancellationToken cancellation);
    }
}