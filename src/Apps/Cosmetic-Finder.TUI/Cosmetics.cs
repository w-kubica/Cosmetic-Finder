using Cosmetic_Finder.Common.Domain.Model;
using Cosmetic_Finder.Common.Infrastructure.Repositories;
using System.Globalization;

namespace Cosmetic_Finder.TUI
{
    public static class Cosmetics
    {
        public static int MainCategoryId;
        public static string? Search;
        public static bool ShouldContainCompose;

        public static async Task<IEnumerable<Cosmetic>> FilterAndSort()
        {
            MainCategoryId = GettingCategoryName();
            Console.WriteLine(MainCategoryId);

            var searchOption = TUI.Search.GettiSearchOption();
            ShouldContainCompose = TUI.Search.SearchOptions(searchOption);
            Console.WriteLine(ShouldContainCompose);

            Search = TUI.Search.GettingComponent();
            Console.WriteLine(Search);

            var sortInput = Sort.GettingIsSort();
            var isSort = Sort.IsSortOptions(sortInput);
            Console.WriteLine(isSort);

            IEnumerable<Cosmetic> result;
            if (isSort)
            {
                var sortOption = Sort.GettingSortMethod();
                var sortByPriceAsc = Sort.SortByPriceAsc(sortOption);
                result = await WriteCosmeticsAsync(Search, MainCategoryId, ShouldContainCompose, isSort, sortByPriceAsc);
            }
            else
            {
                result = await WriteCosmeticsAsync(Search, MainCategoryId, ShouldContainCompose, isSort, false);
            }
            return result;
        }

        private static int GettingCategoryName()
        {
            var categoryNum = Categories.GettingCategory();
            var mainCategoryId = Categories.CategoryOptions(categoryNum);
            return mainCategoryId;
        }

        public static async Task<IEnumerable<Cosmetic>> WriteCosmeticsAsync(string? search, int mainCategoryId, bool shouldContainCompose, bool sort, bool sortByPriceAsc)
        {

            var result = await CosmeticRepository.GetCosmetics(search, mainCategoryId, shouldContainCompose, sort, sortByPriceAsc, CancellationToken.None);

            var counter = 0;
            var writeCosmeticsAsync = result.ToList();
            foreach (var item in writeCosmeticsAsync)
            {
                counter++;
                Console.WriteLine($"{counter}. {item.Brand}, {item.Caption}, {item.Id}");
                Console.WriteLine($"Cena {Convert.ToString(item.Price, CultureInfo.InvariantCulture)}");
                Console.WriteLine();
            }
            return writeCosmeticsAsync;
        }
    }
}
