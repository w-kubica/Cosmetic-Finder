using Cosmetic_Finder.Common.Application.UtilsHtml;
using Cosmetic_Finder.Common.Domain.Model;
using Cosmetic_Finder.Common.Infrastructure;
using Cosmetic_Finder.Common.Infrastructure.Gateways;
using Cosmetic_Finder.Common.Infrastructure.Response;
using HtmlAgilityPack;
using Refit;

namespace Cosmetic_Finder.Common.Application
{
    public static class CosmeticProvider
    {
        public static async Task<IEnumerable<Product>> ImportProducts()
        {
            var getProductsTasks = new List<Task<IEnumerable<Product>>>();

            foreach (var category in Categories.CosmeticCategories)
            {
                var task = GettingProductsByCategoryId(category);
                getProductsTasks.Add(task);
            }

            await Task.WhenAll(getProductsTasks);

            var products = new List<Product>();

            // wyłuskanie danych
            foreach (var productTask in getProductsTasks)
            {
                var product = await productTask;
                products.AddRange(product);
            }

            //wypisanie danych
            foreach (var product in products)
            {
                Console.WriteLine(product.Id);
            }

            return products;
        }

        public static async Task<List<Compose>> ImportCompose(IEnumerable<Product> products)
        {
            var getComposesTasks = new List<Task<Compose>>();

            foreach (var product in products)
            {
                var task = GettingComposeByProductId(product);
                getComposesTasks.Add(task);
            }

            // pobranie składów
            await Task.WhenAll(getComposesTasks);

            // wyłuskanie danych
            var composes = new List<Compose>();

            foreach (var composeTask in getComposesTasks)
            {
                var compose = await composeTask;
                composes.Add(compose);
            }

            foreach (var compose in composes)
            {
                Console.WriteLine($"{compose.Id} {compose.ProductCompose}");
                Console.WriteLine();
            }

            return composes;
        }

        private static async Task<Compose> GettingComposeByProductId(Product product)
        {
            var productsApi = RestService.For<IProductsAdditionalsApi>($"{ApiConst.RossmannPortalUrl}/products/api");
            var productAdditionals = await productsApi.Get(product.Id);

            var productCompose = string.Join("", productAdditionals.Data
                .Where(h => h.Type == "CharacterComponents")
                .Select(x => x.Html));

            var html = new HtmlDocument();
            html.LoadHtml(productCompose);
                HtmlUtils.RemoveStyles(html);
            var noHtml = HtmlUtils.ConvertHtmlToString(html);

            return new Compose(product.Id, noHtml);
        }

        private static async Task<IEnumerable<Product>> GettingProductsByCategoryId(KeyValuePair<int, string> category)
        {
            var categoryId = category.Key;
            var productsApi = RestService.For<ICategoriesApi>($"{ApiConst.RossmannPortalUrl}/products/api");
            var products = (await productsApi.Get(categoryId)).Data.Products;

            foreach (var product in products)
            {
                product.MainCategoryId = categoryId;
            }

            return products;
        }
    }
}
