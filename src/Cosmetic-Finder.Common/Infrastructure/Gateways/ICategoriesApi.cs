using Cosmetic_Finder.Common.Infrastructure.Response;
using Refit;

namespace Cosmetic_Finder.Common.Infrastructure.Gateways
{
    public interface ICategoriesApi
    {
        [Get("/Products?CategoryId={CategoryId}&PageSize=6500")]
        Task<ResponseProducts> Get(int categoryId);
    }
}
