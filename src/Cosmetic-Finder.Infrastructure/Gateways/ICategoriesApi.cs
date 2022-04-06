using Cosmetic_Finder.Infrastructure.Gateways.Response;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Gateways;

public interface ICategoriesApi
{
    [Get("/Products?CategoryId={CategoryId}&PageSize=6500")]
    Task<ResponseProducts> Get(int categoryId);
}
