using Cosmetic_Finder.Common.Domain.Model;

namespace Cosmetic_Finder.TUI
{
    public class Program
    {
        private static async Task Main()
        {
            var cosmetics = await Cosmetics.FilterAndSort();

            CosmeticsWithNumber(cosmetics);

            Console.WriteLine("Które produkty dodać do ulubionych? Wymień po przecinku");
            var favCosmetics = Convert.ToInt16(Console.ReadLine().Split(","));

            Console.WriteLine("Czy wyświetlić listę ulubionych?");
            Console.WriteLine("Tak"); // todo funkcja do 
            Console.WriteLine("Nie");

            Console.WriteLine("Czy chcesz pobrać listę?");
            Console.WriteLine("Tak"); //todo funkcja do zapisywania wyników do pliku 
            Console.WriteLine("Nie");

        }

        private static void CosmeticsWithNumber(IEnumerable<Cosmetic> cosmetics)
        {
            Dictionary<int, Cosmetic> favCosmetics = new Dictionary<int, Cosmetic>();
            var counter = 0;
            foreach (var cosmetic in cosmetics)
            {
                counter++;
                favCosmetics.Add(counter, cosmetic);
            }
        }







    }
}