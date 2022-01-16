using Cosmetic_Finder.Common.Infrastructure.Response;
using Refit;

namespace Cosmetic_Finder.Common.Infrastructure.Gateways
{
    public interface ICategoriesApi
    {
        [Get("/Products?CategoryId={CategoryId}&PageSize=3")]
        Task<ResponseProducts> Get(int categoryId);
    }
}
