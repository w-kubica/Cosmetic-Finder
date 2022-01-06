using Refit;
using System.Threading.Tasks;
using Cosmetic_Finder.Importer.Infrastructure.Response;

namespace Cosmetic_Finder.Importer.Infrastructure.Gateways
{
    public interface ICategoriesApi
    {
        [Get("/Products?CategoryId={CategoryId}&PageSize=3")]
        Task<ResponseProducts> Get(int categoryId);
    }
}
