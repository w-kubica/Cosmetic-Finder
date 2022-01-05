using Cosmetic_Finder.Importer.Response;
using Refit;
using System.Threading.Tasks;

namespace Cosmetic_Finder.Importer.Gateways
{
    public interface IProductsAdditionalsApi
    {
        [Get("/Products/{productId}/additionals")]
        Task<ResponseProductsAdditionals> Get(int productId);
    }
}
