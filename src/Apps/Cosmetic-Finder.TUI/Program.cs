using Cosmetic_Finder.Common.Domain.Model;
using Cosmetic_Finder.Common.Infrastructure.Repositories;
using System.Globalization;
using CommonServiceLocator;
using Cosmetic_Finder.Common.Infrastructure.Models;
using SolrNet;

namespace Cosmetic_Finder.TUI
{
    public class Program
    {
        private static async Task Main()
        {
            var category_num = GettingCategory();
            var mainCategoryId = CategoryOptions(category_num);
            Console.WriteLine(mainCategoryId);

            var searchOption = GettiSearchOption();
            var shouldContainCompose = SearchOptions(searchOption);
            Console.WriteLine(shouldContainCompose);

            var search = GettingComponent();
            Console.WriteLine(search);

            //Czy sortować
            var sortInput = GettingIsSort();
            // bool sort true or false
            var sortOpt = IsSortOptions(sortInput);
            Console.WriteLine(sortOpt);
            // jeśli true to zapytaj czy malejąco czy rosnąco
            //var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrCosmetic>>();

            if (!sortOpt)
            {
                await WriteCosmeticsAsync(search, mainCategoryId, shouldContainCompose, sortOpt, false);
            }
            else
            {
                var sort = SortByAsc(sortOpt);
                await WriteCosmeticsAsync(search, mainCategoryId, shouldContainCompose, sortOpt, sort);
            }

            

            //Dictionary<int, int> favCosmetics = new Dictionary<int, int>();
            //Console.WriteLine("Dodaj produkty do ulubionych: ");
            //var cosmetic = Convert.ToInt16(Console.ReadLine());
            //favCosmetics
        }

        public static bool SortByAsc(bool sort)
        {
            bool sortByPriceAsc = false;
            switch (sort)
            {
                case true:
                    sortByPriceAsc = true;
                    var sortMethod = SortMethod();
                    break;
                case false:
                    sortByPriceAsc = false;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }

            return sortByPriceAsc;
        }

        public static bool SortMethod() 
        {
            bool sortByPriceAsc = false;
            var sortOption = GettingSortMethod();
            switch (sortOption)
            {
                case 1:
                    sortByPriceAsc = true;
                    break;
                case 2:
                    sortByPriceAsc = false;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }

            return sortByPriceAsc; 
        }

        public static async Task WriteCosmeticsAsync(string search, int mainCategoryId, bool shouldContainCompose, bool sort, bool sortByPriceAsc)
        {
            var result = await CosmeticRepository.GetCosmetics(search, mainCategoryId, shouldContainCompose, sort, sortByPriceAsc, CancellationToken.None);

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

        public static bool IsSortOptions(short sort)
        {
            bool isSort = false;

            switch (sort)
            {
                case 1:
                    isSort = true;
                    break;
                case 2:
                    isSort = false;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }

            return isSort;
        }

        public static bool SearchOptions(short searchOption)
        {
            bool shouldContainComponent = false;
            switch (searchOption)
            {
                case 1:
                    shouldContainComponent = true;
                    break;
                case 2:
                    shouldContainComponent = false;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }
            return shouldContainComponent;
        }

        public static int CategoryOptions(short category_num)
        {
            int mainCategoryId = 0;

            switch (category_num)
            {
                case 1:
                    mainCategoryId = 8686;
                    break;
                case 2:
                    mainCategoryId = 8528;
                    break;
                case 3:
                    mainCategoryId = 8655;
                    break;
                case 4:
                    mainCategoryId = 8625;
                    break;
                case 5:
                    mainCategoryId = 8576;
                    break;
                case 6:
                    mainCategoryId = 9220;
                    break;
                case 7:
                    mainCategoryId = 8512;
                    break;
                case 8:
                    mainCategoryId = 8471;
                    break;
                case 9:
                    mainCategoryId = 9246;
                    break;
                case 10:
                    mainCategoryId = 8445;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }

            Console.WriteLine(mainCategoryId);
            return mainCategoryId;
        }

        public static string? GettingComponent()
        {
            Console.WriteLine("Podaj składnik: ");
            var component = Console.ReadLine();
            return component;
        }

        public static short GettingSortMethod()
        {
            Console.WriteLine("1. Sortowanie rosnące");
            Console.WriteLine("1. Sortowanie malejące");
            var sortOption = Convert.ToInt16(Console.ReadLine());
            return sortOption;
        }

        public static short GettingIsSort()
        {
            Console.WriteLine("Czy chcesz posortować wyniki po cenie?");
            Console.WriteLine("1. Tak");
            Console.WriteLine("2. Nie");

            var sort = Convert.ToInt16(Console.ReadLine());
            return sort;
        }

        public static short GettiSearchOption()
        {
            Console.WriteLine("Wybierz jak chcesz szukać, podając cyfrę 1 lub 2:");
            Console.WriteLine("1. Zawiera składnik");
            Console.WriteLine("2. Nie zawiera składnika");
            var searchOption = Convert.ToInt16(Console.ReadLine());
            return searchOption;
        }

        public static short GettingCategory()
        {
            Console.WriteLine("Wybierz kategorię, podając cyfrę od 1 do 10: ");
            var counter = 0;
            foreach (var category in Categories.CosmeticCategories)
            {
                counter++;
                Console.WriteLine($"{counter}: {category.Value}");
            }

            var category_num = Convert.ToInt16(Console.ReadLine());
            return category_num;
        }
    }
}