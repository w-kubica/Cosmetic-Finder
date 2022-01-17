using Cosmetic_Finder.Common.Domain.Model;
using Cosmetic_Finder.Common.Infrastructure.Models;
using Cosmetic_Finder.Common.Infrastructure.Repositories;
using SolrNet;
using System.Globalization;

namespace Cosmetic_Finder.TUI
{
    public static class Cosmetics
    {
        public static async Task<IEnumerable<Cosmetic>> FilterAndSort()
        {
            var categoryNum = Categories.GettingCategory();
            var mainCategoryId = Categories.CategoryOptions(categoryNum);
            Console.WriteLine(mainCategoryId);

            var searchOption = Search.GettiSearchOption();
            var shouldContainCompose = Search.SearchOptions(searchOption);
            Console.WriteLine(shouldContainCompose);

            var search = Search.GettingComponent();
            Console.WriteLine(search);

            //Czy sortować
            var sortInput = Sort.GettingIsSort();
            // bool sort true or false
            var sortOpt = Sort.IsSortOptions(sortInput);
            Console.WriteLine(sortOpt);
            // jeśli true to zapytaj czy malejąco czy rosnąco
            Startup.Init<SolrCosmetic>("http://localhost:8983/solr/cosmetics");

            IEnumerable<Cosmetic> result;
            if (!sortOpt)
            {
                result = await WriteCosmeticsAsync(search, mainCategoryId, shouldContainCompose, sortOpt, false);
            }
            else
            {
                var sort = Sort.SortByAsc(sortOpt);
                result = await WriteCosmeticsAsync(search, mainCategoryId, shouldContainCompose, sortOpt, sort);
            }

            return result;
        }

        public static async Task<IEnumerable<Cosmetic>> WriteCosmeticsAsync(string search, int mainCategoryId, bool shouldContainCompose, bool sort, bool sortByPriceAsc)
        {

            var result = await CosmeticRepository.GetCosmetics(search, mainCategoryId, shouldContainCompose, sort, sortByPriceAsc, CancellationToken.None);

            var counter = 0;
            foreach (var item in result)
            {
                counter++;
                Console.WriteLine(counter);
                Console.WriteLine($"Cena {Convert.ToString(item.Price, CultureInfo.InvariantCulture)}");
                Console.WriteLine(item.Id);
                Console.WriteLine(item.NavigateUrl);
                Console.WriteLine(item.Brand);
                Console.WriteLine(item.Caption);
                Console.WriteLine($"Kategoria {Convert.ToString(item.Category)}");
                Console.WriteLine($"Compose {item.Compose}");
                Console.WriteLine("**************************");
            }

            return result;
        }

    }
}
