using Cosmetic_Finder.Infrastructure.Gateways.Response;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Gateways;

public interface IProductsAdditionalsApi
{
    [Get("/Products/{productId}/additionals")]
    Task<ResponseProductsAdditionals> Get(int productId);
}
