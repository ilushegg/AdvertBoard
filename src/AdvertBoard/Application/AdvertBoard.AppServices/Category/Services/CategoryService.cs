using AdvertBoard.AppServices.Category.Repositories;
using AdvertBoard.AppServices.Product.Repositories;
using AdvertBoard.Contracts;
using AdvertBoard.Domain;
using System.Xml.Linq;

namespace AdvertBoard.AppServices.Product.Services;

/// <inheritdoc />
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AdvertisementService"/>.
    /// </summary>
    /// <param name="productRepository"></param>
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellation)
    {
        var categories = await _categoryRepository.GetAll(cancellation);
        var result = new List<CategoryDto>();
        foreach(var category in categories)
        {
            if (category.ParentCategoryId == null)
            {
                var categoryDto = new CategoryDto
                {
                    Key = category.Key,
                    Title = category.Title,
                    Children = categories.Where(c => c.ParentCategoryId == category.Key).ToList().Select(cc => new CategoryDto
                    {
                        Key = cc.Key,
                        Title = cc.Title,
                        Children = categories.Where(c => c.ParentCategoryId == cc.Key).ToList()
                    }).ToList()

                };
                result.Add(categoryDto);
            }
        }
        return result;
    }


    /// <inheritdoc />
    public async Task<Guid> AddAsync(Guid parentId, string categoryName, CancellationToken cancellation = default)
    {
        var category = new Domain.Category();
        if (parentId.Equals(Guid.Empty))
        {

            category.Name = categoryName;
            category.ParentCategoryId = null;
        }
        else
        {
            category.Name = categoryName;
            category.ParentCategoryId = parentId;
        }

        await _categoryRepository.AddAsync(category, cancellation);
        return category.Id;
    }

    public async Task<Guid> EditAsync(Guid categoryId, string name, CancellationToken cancellation)
    {
        var category = _categoryRepository.FindById(categoryId);
        if (category == null)
        {
            throw new Exception($"Категория с идентификатором '{categoryId}' не найден");
        }
        else
        {
            category.Name = name;
            await _categoryRepository.EditAsync(category, cancellation);
            return category.Id;
        }
    }

    public async Task<Guid> DeleteAsync(Guid categoryId, CancellationToken cancellation)
    {
        var category = _categoryRepository.FindById(categoryId);
        if (category == null)
        {
            throw new Exception($"Категория с идентификатором '{categoryId}' не найден");
        }
        else
        {
            
            await _categoryRepository.DeleteAsync(category, cancellation);
            return category.Id;
        }

    }


    public Task<CategoryDto> GetAsync(Guid categoryId, CancellationToken cancellation)
    {
        return _categoryRepository.FindByIdAsync(categoryId, cancellation);
    }
}
