using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.Product.Services
{
    public interface ICategoryService
    {
        Task<Guid> AddAsync(Guid parentId, string categoryName, CancellationToken cancellation = default);
        Task<Guid> DeleteAsync(Guid categoryId, CancellationToken cancellation);
        Task<Guid> EditAsync(Guid categoryId, string name, CancellationToken cancellation);
        Task<CategoryDto> Get(Guid categoryId, CancellationToken cancellation);
        Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellation);
    }
}