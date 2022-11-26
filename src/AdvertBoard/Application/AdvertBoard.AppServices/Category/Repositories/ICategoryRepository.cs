using AdvertBoard.Contracts;

namespace AdvertBoard.AppServices.Category.Repositories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellation);

        Task AddAsync(Domain.Category category, CancellationToken cancellation);

        Task EditAsync(Domain.Category category, CancellationToken cancellation);

        Task<CategoryDto> FindById(Guid categoryId, CancellationToken cancellation);

        Task<CategoryDto> FindByName(string name, CancellationToken cancellation);

        Task DeleteAsync(Domain.Category category, CancellationToken cancellation);
    }
}