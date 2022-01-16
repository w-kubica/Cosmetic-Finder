using Cosmetic_Finder.Importer.Domain.Model;
using Cosmetic_Finder.Importer.Infrastructure.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using Cosmetic_Finder.Importer.Infrastructure;

namespace Cosmetic_Finder.Importer.Application
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
