using Cosmetic_Finder.Common.Application;
using Cosmetic_Finder.Common.Infrastructure.Models;
using Cosmetic_Finder.Common.Infrastructure.Repositories;
using SolrNet;
using System.Linq;
using System.Threading.Tasks;

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



        }
    }
}