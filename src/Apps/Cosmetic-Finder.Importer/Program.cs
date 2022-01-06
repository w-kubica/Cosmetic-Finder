using Cosmetic_Finder.Importer.Gateways;
using Cosmetic_Finder.Importer.Model;
using Cosmetic_Finder.Importer.Response;
using HtmlAgilityPack;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cosmetic_Finder.Importer
{
    class Program
    {
        static async Task Main()
        {
            var products = await ImportProducts();
            var composes = await ImportCompose(products);
        }

        private static async Task<List<ResponseProducts>> ImportProducts()
        {
            var getProductsTasks = new List<Task<ResponseProducts>>();

            foreach (var category in Categories.CosmeticCategories)
            {
                var task = GettingProductsByCategoryId(category);
                getProductsTasks.Add(task);
            }

            await Task.WhenAll(getProductsTasks);

            var products = new List<ResponseProducts>();

            // wyłuskanie danych
            foreach (var productTask in getProductsTasks)
            {
                var product = await productTask;
                products.Add(product);
            }

            Console.WriteLine("Zakończono pobieranie produktów");

            //wypisanie danych
            foreach (var product in products)
            {
                foreach (var oneProducts in product.Data.Products)
                {
                    Console.WriteLine(oneProducts.Id);
                }
            }

            return products;
        }

        private static async Task<List<string>> ImportCompose(List<ResponseProducts> products)
        {
            var getComposesTasks = new List<Task<string>>();

            foreach (var product in products)
            {
                foreach (var oneProducts in product.Data.Products)
                {
                    var task = GettingComposeByProductId(oneProducts);
                    getComposesTasks.Add(task);
                }
            }

            // pobranie składów
            await Task.WhenAll(getComposesTasks);

            // wyłuskanie danych
            var composes = new List<string>();

            foreach (var composeTask in getComposesTasks)
            {
                var compose = await composeTask;
                composes.Add(compose);
            }

            foreach (var compose in composes)
            {
                Console.WriteLine($"{composes.IndexOf(compose)} {compose}");
            }

            return composes;
        }

        private static async Task<string> GettingComposeByProductId(Product product)
        {
            var productsApi = RestService.For<IProductsAdditionalsApi>("https://www.rossmann.pl/products/api");
            var productAdditionals = await productsApi.Get(product.Id);

            var productCompose = string.Join("", productAdditionals.Data
                .Where(h => h.Type == "CharacterComponents")
                .Select(x => x.Html));

            var html = new HtmlDocument();
            html.LoadHtml(productCompose);

            //usuwa style
            RemoveStyles(html);

            //usuwa twardą spację
            var noHtml = ConvertHtmlToString(html);

            //Console.WriteLine("OBROBIONY HTML");
            //Console.WriteLine($"Id produktu: {product.Id}");
            //Console.WriteLine($"URL: https://www.rossmann.pl{product.NavigateUrl}");
            //Console.WriteLine(noHtml);

            //var path = @"C:\Users\Weronika\Desktop\file-compose.txt";
            //using (StreamWriter sw = File.AppendText(path))
            //{
            //    sw.WriteLine($"Id produktu: {product.Id}");
            //    sw.WriteLine(noHtml);
            //}

            return noHtml;
        }

        private static async Task<ResponseProducts> GettingProductsByCategoryId(KeyValuePair<int, string> category)
        {
            var categoryId = category.Key;
            var categoryName = category.Value;
            //Console.WriteLine($"KATEGORIA: {categoryId},{categoryName}");

            var productsApi = RestService.For<ICategoriesApi>("https://www.rossmann.pl/products/api");
            var products = await productsApi.Get(categoryId);

            //var options = new JsonSerializerOptions { WriteIndented = true };
            //string jsonString = JsonSerializer.Serialize(products, options);

            //var path = @"C:\Users\Weronika\Desktop\file.txt";
            //await using StreamWriter sw = File.AppendText(path);
            //sw.WriteLine($"KATEGORIA: {categoryId},{categoryName}");
            //sw.WriteLine(jsonString);

            return products;
        }

        private static string ConvertHtmlToString(HtmlDocument html)
        {
            var nodes = html.DocumentNode.InnerText;
            var noHtml = Regex.Replace(nodes, @"<[^>]+>|&nbsp|&lt|&reg;", "").Trim();
            return noHtml;
        }

        private static void RemoveStyles(HtmlDocument html)
        {
            html.DocumentNode.Descendants()
                .Where(n => n.Name is "script" or "style")
                .ToList()
                .ForEach(n => n.Remove());
        }
    }
}