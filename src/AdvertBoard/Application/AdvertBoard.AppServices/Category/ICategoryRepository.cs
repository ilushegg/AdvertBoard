namespace AdvertBoard.DataAccess.EntityConfigurations.Category
{
    public interface ICategoryRepository
    {
        Task<bool> AddAsync(Domain.Category category, CancellationToken cancellation);
        Task<Domain.Category> FindById(Guid categoryId, CancellationToken cancellation);

        Task<Domain.Category> FindByName(string name, CancellationToken cancellation);

        void Add(Domain.Category category, CancellationToken cancellation);
    }
}