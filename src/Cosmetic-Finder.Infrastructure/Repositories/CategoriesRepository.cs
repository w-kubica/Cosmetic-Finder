using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Core.Repositories;
using Cosmetic_Finder.Infrastructure.Gateways;
using Cosmetic_Finder.Infrastructure.Mappers;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Repositories;
public class CategoriesRepository : ICategoriesRepository
{
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categoriesApi = RestService.For<ICategoriesApi>($"{ApiConst.RossmannPortalUrl}/products/api");
        var request = await categoriesApi.Get();
        return request.Data.ToDomain();
    }
}
