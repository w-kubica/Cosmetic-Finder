using Cosmetic_Finder.Application.DTO;

namespace Cosmetic_Finder.Application.Services;

public interface ICategoryService
{
    public Task <IEnumerable<CategoryDto>> GetCategories();
}
