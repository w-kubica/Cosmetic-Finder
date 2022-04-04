namespace Cosmetic_Finder.TUI
{
    public static class Search
    {
        public static short GettiSearchOption()
        {
            Console.WriteLine("Wybierz jak chcesz szukać, podając cyfrę 1 lub 2:");
            Console.WriteLine("1. Zawiera składnik");
            Console.WriteLine("2. Nie zawiera składnika");

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

        public static string? GettingComponent()
        {
            Console.WriteLine("Podaj składnik: ");

            string? component;
            try
            {
                component = Console.ReadLine();
            }
            catch (FormatException )
            {
                Console.WriteLine("Podaj poprawną wartość");
                component = Console.ReadLine();
            }
            return component;
        }

        public static bool SearchOptions(short response)
        {
            var shouldContainComponent = false;
            switch (response)
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
