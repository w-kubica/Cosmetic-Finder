using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.Gateways;
using Cosmetic_Finder.Infrastructure.Gateways.Response;

namespace Cosmetic_Finder.Infrastructure.Mappers;

//todo: rename
public static class CosmeticProfile
{
    public static IEnumerable<Cosmetic> ToDomainCosmetic(this IEnumerable<ProductResponse> products, List<ComposeDto> composes)
    {
        var cosmetics = new List<Cosmetic>();

        foreach (var product in products)
        {
            cosmetics.Add(new Cosmetic
            {
                Id = product.Id,
                NavigateUrl = $"{ApiConst.RossmannPortalUrl}{product.NavigateUrl}",
                Brand = product.Brand,
                Caption = product.Caption,
                Category = product.Category,
                Price = product.Price,
                Compose = composes.FirstOrDefault(c => c.Id == product.Id)?.ProductCompose,
                // todo: add remove find compose
                MainCategoryId = product.MainCategoryId
            });
        }
        return cosmetics;
    }


}
