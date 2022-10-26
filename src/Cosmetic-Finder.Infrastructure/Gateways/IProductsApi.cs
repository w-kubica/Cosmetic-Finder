using Cosmetic_Finder.Infrastructure.DTO.Rossmann;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Gateways;

public interface IProductsApi
{
    //https://www.rossmann.pl/products/api/Products?CategoryId=8686&PageSize=6500
    [Get("/Products?CategoryId={CategoryId}&Page={Page}")]
    Task<ResponseProducts> Get(int categoryId, int page);
}
