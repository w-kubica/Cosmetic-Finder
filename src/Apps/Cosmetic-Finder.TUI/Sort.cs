namespace Cosmetic_Finder.TUI
{
    public static class Sort
    {
        public static short GettingIsSort()
        {
            Console.WriteLine("Czy chcesz posortować wyniki po cenie?");
            Console.WriteLine("1. Tak");
            Console.WriteLine("2. Nie");

            var sort = Convert.ToInt16(Console.ReadLine());
            return sort;
        }
        public static short GettingSortMethod()
        {
            Console.WriteLine("1. Sortowanie rosnące");
            Console.WriteLine("1. Sortowanie malejące");
            var sortOption = Convert.ToInt16(Console.ReadLine());
            return sortOption;
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
    }
}
