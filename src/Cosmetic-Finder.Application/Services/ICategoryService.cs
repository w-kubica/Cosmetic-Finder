using Cosmetic_Finder.Application.DTO;

namespace Cosmetic_Finder.Application.Services;

public interface ICategoryService
{
    IEnumerable<Category> GetCategories();
}
