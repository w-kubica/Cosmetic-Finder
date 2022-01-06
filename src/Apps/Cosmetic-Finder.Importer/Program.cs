using System.Linq;
using Cosmetic_Finder.Importer.Application;
using System.Threading.Tasks;

namespace Cosmetic_Finder.Importer
{
    class Program
    {
        static async Task Main()
        {
            var products = (await CosmeticProvider.ImportProducts()).ToList();
            var composes = await CosmeticProvider.ImportCompose(products);
            var cosmetics = products.MapToCosmetic(composes);
        }
    }
}