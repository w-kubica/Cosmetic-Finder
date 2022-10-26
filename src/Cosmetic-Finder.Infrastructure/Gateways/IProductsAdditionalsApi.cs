using Cosmetic_Finder.Infrastructure.Gateways.Rossmann.Products;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Gateways;

public interface IProductsAdditionalsApi
{
    [Get("/Products/{productId}/additionals")]
    Task<ProductsAdditionals> Get(int productId);
}
