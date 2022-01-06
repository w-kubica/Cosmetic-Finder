using Refit;
using System.Threading.Tasks;
using Cosmetic_Finder.Importer.Infrastructure.Response;

namespace Cosmetic_Finder.Importer.Infrastructure.Gateways
{
    public interface IProductsAdditionalsApi
    {
        [Get("/Products/{productId}/additionals")]
        Task<ResponseProductsAdditionals> Get(int productId);
    }
}
