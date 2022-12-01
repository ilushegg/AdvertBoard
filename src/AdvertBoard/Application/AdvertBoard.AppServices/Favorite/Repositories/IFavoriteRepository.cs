using AdvertBoard.Domain;

namespace AdvertBoard.DataAccess.EntityConfigurations.ShoppingCart
{
    public interface IFavoriteRepository
    {
        Task<Guid> CreateAsync(Favorite favorite, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Favorite>> GetAllAsync(Guid userId, CancellationToken cancellationToken);
        Task<Favorite> GetByProductId(Guid advertisementId, Guid userId, CancellationToken cancellationToken);
    }
}