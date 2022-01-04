using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cosmetic_Finder.Importer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Category category = new Category();
            foreach (var item in category.ListOfCategory)
            {
                var idCategory = item.Key;
                var nameOfCategory = item.Value;
                Console.WriteLine($"KATEGORIA: {idCategory},{nameOfCategory}");

                var productsInCategories = RestService.For<ICategoriesApi>("https://www.rossmann.pl/products/api");
                var allProducts = await productsInCategories.Get(idCategory);

                var listOfProductId = allProducts.Data.products.Select(w => w.Id);

                //var options = new JsonSerializerOptions { WriteIndented = true };
                //string jsonString = JsonSerializer.Serialize(allProducts, options);
                //Console.WriteLine(jsonString);

                //var path = @"C:\Users\Weronika\Desktop\file.txt";
                //using (StreamWriter sw = File.AppendText(path))
                //{
                //    sw.WriteLine($"KATEGORIA: {idCategory},{nameOfCategory}");
                //    sw.WriteLine(jsonString);
                //}
                foreach (var id in listOfProductId)
                {
                    var infoProduct = RestService.For<IAdditionalInfo>("https://www.rossmann.pl/products/api");
                    var listInfo = await infoProduct.Get(id);

                    var compose = string.Join("", listInfo.Data
                        .Where(h => h.type == "CharacterComponents")
                        .Select(x => x.html));

                   // Console.WriteLine("OBROBIONY JSON:");
                  //  Console.WriteLine(compose);

                    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(compose);
                    //usuwa style
                    htmlDoc.DocumentNode.Descendants()
                        .Where(n => n.Name == "script" || n.Name == "style")
                        .ToList()
                        .ForEach(n => n.Remove());

                    //usuwa twardą spację
                    string nodes = htmlDoc.DocumentNode.InnerText;
                    string noHTML = Regex.Replace(nodes, @"<[^>]+>|&nbsp|&lt|&reg;", "").Trim();

                    Console.WriteLine("OBROBIONY HTML");
                    Console.WriteLine($"Id produktu: {id}");
                    Console.WriteLine(noHTML);
                }
            }
        }
    }

    public interface ICategoriesApi
    {
        [Get("/Products?CategoryId={key}&PageSize=5")]
        Task<Root> Get(int key);
    }
    public class Product
    {
        public int Id { get; set; }
        public string NavigateUrl { get; set; }
        // public string brand { get; set; }
        // public string caption { get; set; }
        // public string Category { get; set; }
    }
    public class Root
    {
        public Data Data { get; set; }
    }
    public class Data
    {
        public List<Product> products { get; set; }
        public int totalCount { get; set; }
    }
    public class Category
    {
        public Dictionary<int, string> ListOfCategory = new Dictionary<int, string>()
        {
            {8686, "Twarz"},
            //{8528, "Makijaż"},
            //{8655, "Włosy"},
            //{8625, "Ciało"},
            //{8576, "Higiena"},
           // {9220, "Ochrona antybakteryjna"},
            //{8512, "Perfumy"},
            //{8471, "Mama i Dziecko"},
            //{9246, "Mężczyzna"},
            //{8445, "Zdrowie"},
        };
    }

    public interface IAdditionalInfo
    {
        [Get("/Products/{productId}/additionals")]
        Task<ListAdditionalInfo> Get(int productId);
    }
    public class AdditionalInfo
    {
        public string type { get; set; }
        public string html { get; set; }
    }

    public class ListAdditionalInfo
    {
        public List<AdditionalInfo> Data { get; set; }
    }
}