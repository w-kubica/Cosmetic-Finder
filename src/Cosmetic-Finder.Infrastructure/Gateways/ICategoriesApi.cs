using Cosmetic_Finder.Infrastructure.Gateways.Response;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Gateways;

public interface ICategoriesApi
{
    //https://www.rossmann.pl/products/api/Products?CategoryId=8686&PageSize=6500
    [Get("/Products?CategoryId={CategoryId}&PageSize={PageSize}&PageNumber={PageNumber}")]
    Task<ResponseProducts> Get(int categoryId, int pageSize, int pageNumber);
}
