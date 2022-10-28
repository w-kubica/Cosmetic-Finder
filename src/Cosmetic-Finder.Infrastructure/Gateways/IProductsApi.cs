using Cosmetic_Finder.Infrastructure.Gateways.Rossmann.Products;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Gateways;

public interface IProductsApi
{
    [Get("/Products?CategoryId={CategoryId}&Page={Page}")]
    Task<Products> Get(int categoryId, int page);
}
