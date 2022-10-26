using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.DTO.Rossmann;
using Cosmetic_Finder.Infrastructure.Gateways;

namespace Cosmetic_Finder.Infrastructure.Mappers;

//todo: rename
public static class CosmeticProfile
{
    public static IEnumerable<Cosmetic> ToDomainCosmetic(this IEnumerable<Product> products, List<ComposeDto> composes)
        => products.Select(product => new Cosmetic
        {
            Id = product.Id,
            NavigateUrl = $"{ApiConst.RossmannPortalUrl}{product.NavigateUrl}",
            Brand = product.Brand,
            Caption = product.Caption,
            Category = product.Category,
            Price = product.Price,
            Compose = composes.FirstOrDefault(c => c.Id == product.Id)?.ProductCompose,
            // todo: add remove find compose
            MainCategoryId = product.MainCategoryId,
            Picture = product.Pictures?.FirstOrDefault()?.Large
        });
}
