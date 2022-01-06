using Cosmetic_Finder.Importer.Response;
using Refit;
using System.Threading.Tasks;

namespace Cosmetic_Finder.Importer.Gateways
{
    public interface ICategoriesApi
    {
        [Get("/Products?CategoryId={CategoryId}&PageSize=10")]
        Task<ResponseProducts> Get(int categoryId);
    }
}
