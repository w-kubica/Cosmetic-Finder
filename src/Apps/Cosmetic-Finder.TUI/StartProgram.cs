namespace Cosmetic_Finder.TUI
{
    public static class StartProgram
    {
        public static bool SearchAgain()
        {
            Console.WriteLine("Czy chcesz ponownie wyszukać?");
            Console.WriteLine("1. TAK");
            Console.WriteLine("2. NIE");

            short response;
            try
            {
                response = Convert.ToInt16(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Podaj poprawną wartość");
                response = Convert.ToInt16(Console.ReadLine());
            }
           
            bool search = false;
            switch (response)
            {
                case 1:
                    search = true;
                    break;
                case 2:
                    search = false;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }
            return search;
        }

        public static async Task StartSearch()
        {
            var cosmetics = await Cosmetics.FilterAndSort();
            var cosmeticsNum = FavouriteCosmetics.CosmeticsWithNumber(cosmetics);
            FavouriteCosmeticsOutput.CreateFavCosmetics(cosmeticsNum);
        }
    }
}
