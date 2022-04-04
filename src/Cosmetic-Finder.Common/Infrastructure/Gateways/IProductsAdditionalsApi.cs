using Cosmetic_Finder.Common.Infrastructure.Gateways.Response;
using Refit;

namespace Cosmetic_Finder.Common.Infrastructure.Gateways
{
    public interface IProductsAdditionalsApi
    {
        [Get("/Products/{productId}/additionals")]
        Task<ResponseProductsAdditionals> Get(int productId);
    }
}
