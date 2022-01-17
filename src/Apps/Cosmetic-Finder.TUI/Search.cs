namespace Cosmetic_Finder.TUI
{
    public static class Search
    {
        public static short GettiSearchOption()
        {
            Console.WriteLine("Wybierz jak chcesz szukać, podając cyfrę 1 lub 2:");
            Console.WriteLine("1. Zawiera składnik");
            Console.WriteLine("2. Nie zawiera składnika");
            var searchOption = Convert.ToInt16(Console.ReadLine());
            return searchOption;
        }

        public static string? GettingComponent()
        {
            Console.WriteLine("Podaj składnik: ");
            var component = Console.ReadLine();
            return component;
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
    }
}
