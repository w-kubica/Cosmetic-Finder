using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Core.Repositories;

public interface ICategoriesRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
}
