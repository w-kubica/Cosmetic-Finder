using Cosmetic_Finder.Application.DTO;
using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Application.Services;

public class CategoryService : ICategoryService
{
    public IEnumerable<Category> GetCategories()
    {
        return Categories.CosmeticCategories.Select(category => new Category(category.Key, category.Value));
    }
}
