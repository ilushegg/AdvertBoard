using AdvertBoard.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertBoard.DataAccess.EntityConfigurations.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Domain.Category> _repository;

        public CategoryRepository(IRepository<Domain.Category> repository)
        {
            _repository = repository;
        }

        public async Task<Domain.Category> FindById(Guid categoryId, CancellationToken cancellation)
        {
            var result = await _repository.GetByIdAsync(categoryId);
            return result;
        }

        public async Task<Domain.Category> FindByName(string name, CancellationToken cancellation)
        {
            var result = _repository.GetAll().Where(c => c.Name == name).FirstOrDefault();
            return result;
        }

        public async Task<bool> AddAsync(Domain.Category category, CancellationToken cancellation)
        {
            var result = _repository.AddAsync(category);
            return true;
        }

        public void Add(Domain.Category category, CancellationToken cancellation)
        {
            _repository.Add(category);
        }

    }
}
