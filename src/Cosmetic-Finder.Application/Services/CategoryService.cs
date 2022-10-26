using Cosmetic_Finder.Application.DTO;
using Cosmetic_Finder.Application.Mapper;
using Cosmetic_Finder.Core.Repositories;

namespace Cosmetic_Finder.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoriesRepository _categoriesRepository;
    public CategoryService(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }
    public async Task<IEnumerable<CategoryDto>> GetCategories()
    {
        var categories = await _categoriesRepository.GetAllAsync();
        return categories.Select(a => a.ToApplication());
    }
}
