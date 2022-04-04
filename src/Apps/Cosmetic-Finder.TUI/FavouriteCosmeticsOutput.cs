using System.Globalization;
using Cosmetic_Finder.Common.Domain.Model;
// ReSharper disable LoopVariableIsNeverChangedInsideLoop

namespace Cosmetic_Finder.TUI
{
    public static class FavouriteCosmeticsOutput
    {

        public static Dictionary<int, Cosmetic> CreateFavCosmetics(List<int> favcosmetics, Dictionary<int, Cosmetic> cosmetics)
        {
            var favCosmetics = new Dictionary<int, Cosmetic>();
            foreach (var favcosmetic in favcosmetics)
            {
                if (cosmetics.ContainsKey(favcosmetic))
                {
                    favCosmetics.Add(favcosmetic, cosmetics[favcosmetic]);
                }
            }
            return favCosmetics;
        }

        public static async Task CreateFavCosmeticsAsync(Dictionary<int, Cosmetic> cosmeticsNum)
        {
            var isAddResponse = FavouriteCosmetics.IsAddToFavCosmetics();
            var isAdd = FavouriteCosmetics.AddToFavOptions(isAddResponse);

            if (isAdd)
            {
                var favCosmetics = AddingFavCosmetics(cosmeticsNum, out var isDisplay);

                if (isDisplay)
                {
                    var isDownload = DisplayFavCosmetics(favCosmetics);

                    if (isDownload)
                    {
                        SaveFavCosmeticsToTxt(favCosmetics);
                    }
                    else
                    {
                        var searchAgain = StartProgram.SearchAgain();
                        while (searchAgain)
                        {
                            await StartProgram.StartSearch();
                        }
                    }
                }
                else
                {
                    var isDownload = DisplayFavCosmetics(favCosmetics);

                    if (isDownload)
                    {
                        SaveFavCosmeticsToTxt(favCosmetics);
                    }
                    else
                    {
                        var searchAgain = StartProgram.SearchAgain();
                        while (searchAgain)
                        {
                            await StartProgram.StartSearch();
                        }
                    }
                }
            }
            else
            {
                var searchAgain = StartProgram.SearchAgain();
                while (searchAgain)
                {
                    await StartProgram.StartSearch();
                }
                Environment.Exit(0);
            }
        }

        public static Dictionary<int, Cosmetic> AddingFavCosmetics(Dictionary<int, Cosmetic> cosmeticsNum, out bool isDisplay)
        {
            var favcosmetics = FavouriteCosmetics.AddingToFav();
            var favCosmetics = CreateFavCosmetics(favcosmetics, cosmeticsNum);

            var display = FavouriteCosmetics.DisplayFavCosmetics();
            isDisplay = FavouriteCosmetics.DisplayFavCosmeticsOptions(display);
            return favCosmetics;
        }

        public static bool DisplayFavCosmetics(Dictionary<int, Cosmetic> favCosmetics)
        {
            foreach (var favCosmetic in favCosmetics)
            {
                Console.WriteLine(
                    $"{favCosmetic.Key}. {favCosmetic.Value.Brand}, {favCosmetic.Value.Caption}, {favCosmetic.Value.Id}");
                Console.WriteLine($"Cena {Convert.ToString(favCosmetic.Value.Price, CultureInfo.InvariantCulture)}");
                Console.WriteLine();
            }

            var download = FavouriteCosmetics.DownloadFavCosmetics();
            var isDownload = FavouriteCosmetics.DisplayFavCosmeticsOptions(download);
            return isDownload;
        }

        public static void SaveFavCosmeticsToTxt(Dictionary<int, Cosmetic> favCosmetics)
        {
            var categoryName = Common.Domain.Model.Categories.CosmeticCategories[Cosmetics.MainCategoryId];
            var search = Cosmetics.Search;
            string contain;
            if (Cosmetics.ShouldContainCompose)
            {
                contain = "zawiera";
            }
            else
            {
                contain = "nie-zawiera";
            }

            var fileName = $"{categoryName}-{contain}-{search}.txt";

            using (var sw = File.CreateText(fileName))
            {
                foreach (var favCosmetic in favCosmetics)
                {
                    sw.WriteLine(
                        $"{favCosmetic.Key}. {favCosmetic.Value.Brand}, {favCosmetic.Value.Caption}, {favCosmetic.Value.Id}");
                    sw.WriteLine($"Cena {Convert.ToString(favCosmetic.Value.Price, CultureInfo.InvariantCulture)}");
                    sw.WriteLine($"Link: {favCosmetic.Value.NavigateUrl}");
                }
            }
        }
    }
}
