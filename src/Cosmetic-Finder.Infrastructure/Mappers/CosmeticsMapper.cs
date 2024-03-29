using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.Gateways;
using Cosmetic_Finder.Infrastructure.Gateways.Rossmann.Products;

namespace Cosmetic_Finder.Infrastructure.Mappers;

public static class CosmeticsMapper
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
            MainCategoryId = product.MainCategoryId,
            Picture = product.Pictures?.FirstOrDefault()?.Large
        });
}
