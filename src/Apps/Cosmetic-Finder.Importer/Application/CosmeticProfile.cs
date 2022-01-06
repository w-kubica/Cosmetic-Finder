using Cosmetic_Finder.Importer.Domain.Model;
using Cosmetic_Finder.Importer.Infrastructure.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cosmetic_Finder.Importer.Application
{
    public static class CosmeticProfile
    {
        public static IEnumerable<Cosmetic> MapToCosmetic(this IEnumerable<Product> products, List<Compose> composes)
        {
            var cosmetics = new List<Cosmetic>();

            foreach (var product in products)
            {
                Cosmetic cosmetic = new()
                {
                    Id = product.Id,
                    NavigateUrl = product.NavigateUrl,
                    Brand = product.Brand,
                    Caption = product.Caption,
                    Category = product.Category
                };

                cosmetic.Compose = composes.FirstOrDefault(c => c.Id == product.Id)?.ProductCompose;
                // todo: add remove find compose

                cosmetics.Add(cosmetic);
                Console.WriteLine(cosmetic);
            }

            return cosmetics;
        }
    }
}
