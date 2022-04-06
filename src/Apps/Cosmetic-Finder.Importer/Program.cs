using System.Linq;
using System.Threading.Tasks;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.Mappers;
using Cosmetic_Finder.Infrastructure.Providers;
using SolrNet;

namespace Cosmetic_Finder.Importer;

public class Program
{
    private static async Task Main()
    {
        var products = (await CosmeticProvider.ImportProducts()).ToList();
        var composes = await CosmeticProvider.ImportComposes(products);
        var cosmetics = products.ToDomainCosmetic(composes);

        Startup.Init<SolrCosmetic>("http://localhost:8983/solr/cosmetics");

        //await CosmeticRepository.AddOrUpdateCosmetics(cosmetics);
    }
}
