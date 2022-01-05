using Cosmetic_Finder.Importer.Gateways;
using Cosmetic_Finder.Importer.Model;
using Cosmetic_Finder.Importer.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Cosmetic_Finder.Importer
{
    class Program
    {
        static async Task Main()
        {
            await ImportData();
        }

        private static async Task ImportData()
        {
            Categories categories = new Categories();
            foreach (var category in categories.CosmeticCategories)
            {
                var products = await GettingProductsByCategoryId(category);

                
                foreach (var product in products.Data.Products)
                {
                    await GettingComposeByProductId(product);
                }
            }
        }

        private static async Task GettingComposeByProductId(Product product)
        {
            var productsApi =
                RestService.For<IProductsAdditionalsApi>("https://www.rossmann.pl/products/api");
            var productAdditionals = await productsApi.Get(product.Id);

            var productCompose = string.Join("", productAdditionals.Data
                .Where(h => h.Type == "CharacterComponents")
                .Select(x => x.Html));

            var html = new HtmlDocument();
            html.LoadHtml(productCompose);

            //usuwa style
            html.DocumentNode.Descendants()
                .Where(n => n.Name is "script" or "style")
                .ToList()
                .ForEach(n => n.Remove());

            //usuwa twardą spację
            var nodes = html.DocumentNode.InnerText;
            var noHtml = Regex.Replace(nodes, @"<[^>]+>|&nbsp|&lt|&reg;", "").Trim();

            Console.WriteLine("OBROBIONY HTML");
            Console.WriteLine($"Id produktu: {product.Id}");
            Console.WriteLine($"URL: https://www.rossmann.pl{product.NavigateUrl}");
            Console.WriteLine(noHtml);
        }

        private static async Task<ResponseCategory> GettingProductsByCategoryId(KeyValuePair<int, string> category)
        {
            var categoryId = category.Key;
            var categoryName = category.Value;
            Console.WriteLine($"KATEGORIA: {categoryId},{categoryName}");

            var productsApi = RestService.For<ICategoriesApi>("https://www.rossmann.pl/products/api");
            var products = await productsApi.Get(categoryId);
            return products;
        }
    }
}