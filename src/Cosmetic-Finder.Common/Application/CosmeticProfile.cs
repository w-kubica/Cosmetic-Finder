using Cosmetic_Finder.Common.Domain.Model;
using Cosmetic_Finder.Common.Infrastructure;
using Cosmetic_Finder.Common.Infrastructure.Response;

namespace Cosmetic_Finder.Common.Application
{
    public static class CosmeticProfile
    {
        public static IEnumerable<Cosmetic> ToDomainCosmetic(this IEnumerable<Product> products, List<Compose> composes)
        {
            var cosmetics = new List<Cosmetic>();

            foreach (var product in products)
            {
                Cosmetic cosmetic = new()
                {
                    Id = product.Id,
                    NavigateUrl = $"{ApiConst.RossmannPortalUrl}{product.NavigateUrl}",
                    Brand = product.Brand,
                    Caption = product.Caption,
                    Category = product.Category,
                    Price = product.Price,
                    Compose = composes.FirstOrDefault(c => c.Id == product.Id)?.ProductCompose
                    // todo: add remove find compose
                };
                cosmetics.Add(cosmetic);
            }
            return cosmetics;
        }
    }
}
