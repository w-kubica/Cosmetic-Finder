using Cosmetic_Finder.Common.Infrastructure.Models;
using SolrNet;

namespace Cosmetic_Finder.TUI
{
    public class Program
    {
        public static async Task Main()
        {
            Startup.Init<SolrCosmetic>("http://localhost:8983/solr/cosmetics");

            await StartProgram.StartSearch();

            bool searchAgain = StartProgram.SearchAgain();
            while (searchAgain)
            {
                Console.Clear();
                await StartProgram.StartSearch();
            }
        }
    }
}