using SolrNet;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cosmetic_Finder.Common.Application;
using Cosmetic_Finder.Common.Infrastructure.Models;
using Cosmetic_Finder.Common.Infrastructure.Repositories;

namespace Cosmetic_Finder.Importer
{
    public class Program
    {
        private static async Task Main()
        {
            var products = (await CosmeticProvider.ImportProducts()).ToList();
            var composes = await CosmeticProvider.ImportCompose(products);
            var cosmetics = products.ToDomainCosmetic(composes);

            Startup.Init<SolrCosmetic>("http://localhost:8983/solr/cosmetics");

            await CosmeticRepository.AddOrUpdateCosmetics(cosmetics);


            var result = await CosmeticRepository.GetCosmetics("Hydrogenated Ethylhexyl Olivate", false, false, CancellationToken.None);

            foreach (var item in result)
            {
                Console.WriteLine($"Cena {Convert.ToString(item.Price, CultureInfo.InvariantCulture)}");
                Console.WriteLine(item.Id);
                Console.WriteLine(item.NavigateUrl);
                Console.WriteLine(item.Brand);
                Console.WriteLine(item.Caption);
                Console.WriteLine($"Kategoria {Convert.ToString(item.Category)}");
                Console.WriteLine($"Compose {item.Compose}");
                Console.WriteLine("**************************");
            }
        }
    }
}