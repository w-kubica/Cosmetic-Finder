using System.Globalization;
using Cosmetic_Finder.Common.Domain.Model;

namespace Cosmetic_Finder.TUI
{
    public static class FavouriteCosmetics
    {
        public static Dictionary<int, Cosmetic> CosmeticsWithNumber(IEnumerable<Cosmetic> cosmetics)
        {
            var favCosmetics = new Dictionary<int, Cosmetic>();
            var counter = 0;
            foreach (var cosmetic in cosmetics)
            {
                counter++;
                favCosmetics.Add(counter, cosmetic);
            }
            return favCosmetics;
        }

        public static short IsAddToFavCosmetics()
        {
            Console.WriteLine("Czy chcesz dodać kosmetyki do ulubionych?");
            Console.WriteLine("1. TAK");
            Console.WriteLine("2. NIE");

            short addToFavCosmetics;
            try
            {
                addToFavCosmetics = Convert.ToInt16(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine("Podaj poprawną wartość");
                addToFavCosmetics = Convert.ToInt16(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            return addToFavCosmetics;
        }

        public static bool AddToFavOptions(short response)
        {
            var isAdd = false;

            switch (response)
            {
                case 1:
                    isAdd = true;
                    break;
                case 2:
                    isAdd = false;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }
            return isAdd;
        }

        public static List<int> AddingToFav()
        {
            Console.WriteLine("Które produkty dodać do ulubionych? Wymień po przecinku");

            string[]? favCosmetics;
            try
            {
                favCosmetics = Console.ReadLine()?.Split(",");
            }
            catch (FormatException)
            {
                Console.WriteLine("Podaj poprawne wartości");
                favCosmetics = Console.ReadLine()?.Split(",");
            }

            return (favCosmetics ?? Array.Empty<string>()).Select(favCosmetic => Convert.ToInt16(favCosmetic, CultureInfo.InvariantCulture)).Select(cosmetic => (int)cosmetic).ToList();

        }

        public static short DisplayFavCosmetics()
        {
            Console.WriteLine("Czy wyświetlić listę ulubionych?");
            Console.WriteLine("1. TAK");
            Console.WriteLine("2. NIE");

            short response;
            try
            {
                response = Convert.ToInt16(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine("Podaj poprawną wartość");
                response = Convert.ToInt16(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            return response;
        }

        public static bool DisplayFavCosmeticsOptions(short response)
        {
            var isDisplay = false;

            switch (response)
            {
                case 1:
                    isDisplay = true;
                    break;
                case 2:
                    isDisplay = false;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }
            return isDisplay;
        }

        public static short DownloadFavCosmetics()
        {
            Console.WriteLine("Czy chcesz pobrać listę?");
            Console.WriteLine("1. TAK");
            Console.WriteLine("2. NIE");

            short response;
            try
            {
                response = Convert.ToInt16(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                Console.WriteLine("Podaj poprawną wartość");
                response = Convert.ToInt16(Console.ReadLine(), CultureInfo.InvariantCulture);
            }
            return response;
        }

        public static bool DownloadFavCosmeticsOptions(short response)
        {
            var isDownload = false;

            switch (response)
            {
                case 1:
                    isDownload = true;
                    break;
                case 2:
                    isDownload = false;
                    break;
                default:
                    Console.WriteLine("Podano złą wartość.");
                    break;
            }
            return isDownload;
        }
    }
}
