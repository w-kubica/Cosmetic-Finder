namespace Cosmetic_Finder.TUI
{
    public static class Sort
    {
        public static short GettingIsSort()
        {
            Console.WriteLine("Czy chcesz posortować wyniki po cenie?");
            Console.WriteLine("1. Tak");
            Console.WriteLine("2. Nie");

            short response;
            try
            {
                response = Convert.ToInt16(Console.ReadLine());
            }
            catch (FormatException )
            {
                Console.WriteLine("Podaj poprawną wartość");
                response = Convert.ToInt16(Console.ReadLine());
            }
            return response;
        }
        public static bool IsSortOptions(short response)
        {
            bool isSort = false;

            switch (response)
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
        public static short GettingSortMethod()
        {
            Console.WriteLine("1. Sortowanie rosnące");
            Console.WriteLine("2. Sortowanie malejące");

            short response;
            try
            {
                response = Convert.ToInt16(Console.ReadLine());
            }
            catch (FormatException )
            {
                Console.WriteLine("Podaj poprawną wartość");
                response = Convert.ToInt16(Console.ReadLine());
            }
            return response;
        }

        public static bool SortByPriceAsc(short response)
        {
            bool sortByPriceAsc = false;

            switch (response)
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
    }
}
