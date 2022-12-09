using AdvertBoard.AppServices.Category.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellation)
        {
            return await _repository.GetAll().Select(c => new CategoryDto
            {
                Key = c.Id,
                Title = c.Name,
                ParentCategoryId = c.ParentCategoryId
            }).ToListAsync(cancellation);
        }

        public CategoryDto FindById(Guid categoryId)
        {
            var result = _repository.GetById(categoryId);
            return new CategoryDto
            {
                Key = result.Id,
                Title = result.Name
            };
        }

        public async Task<CategoryDto> FindByIdAsync(Guid categoryId, CancellationToken cancellation)
        {
            var result = await _repository.GetByIdAsync(categoryId);
            return new CategoryDto
            {
                Key = result.Id,
                Title = result.Name
            };
        }

        public async Task<CategoryDto> FindByName(string name, CancellationToken cancellation)
        {
            var result = _repository.GetAll().Where(c => c.Name == name).FirstOrDefault();
            return new CategoryDto
            {
                Key = result.Id,
                Title = result.Name
            };
        }

        public async Task AddAsync(Domain.Category category, CancellationToken cancellation)
        {
            await _repository.AddAsync(category);
        }

        public async Task EditAsync(Domain.Category category, CancellationToken cancellation)
        {
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(Domain.Category category, CancellationToken cancellation)
        {
            await _repository.DeleteAsync(category);
        }

    }
}
