using Cosmetic_Finder.Infrastructure.Gateways.Rossmann;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Gateways;

public interface ICategoriesApi
{
    [Get("/Categories")]
    Task<Categories> Get();
}
