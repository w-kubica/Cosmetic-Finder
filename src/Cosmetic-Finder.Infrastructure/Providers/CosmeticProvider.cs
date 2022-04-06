using System.Net;
using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.Gateways;
using Cosmetic_Finder.Infrastructure.Gateways.Response;
using Cosmetic_Finder.Infrastructure.UtilsHtml;
using HtmlAgilityPack;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Providers
{
    public static class CosmeticProvider
    {
        public static async Task<IEnumerable<ProductResponse>> ImportProducts()
        {
            var getProductsTasks = new List<Task<IEnumerable<ProductResponse>>>();

            foreach (var category in Categories.CosmeticCategories)
            {
                var task = GettingProductsByCategoryId(category);
                getProductsTasks.Add(task);
            }

            await Task.WhenAll(getProductsTasks);

            var products = new List<ProductResponse>();

            // wyłuskanie danych
            foreach (var productTask in getProductsTasks)
            {
                var product = await productTask;
                products.AddRange(product);
            }

            return products;
        }

        public static async Task<List<ComposeDto>> ImportComposes(IEnumerable<ProductResponse> products)
        {
            var getComposesTasks = new List<Task<ComposeDto>>();

            foreach (var product in products)
            {
                var task = GettingComposeByProductId(product);
                getComposesTasks.Add(task);
            }

            // pobranie składów
            await Task.WhenAll(getComposesTasks);

            // wyłuskanie danych
            var composes = new List<ComposeDto>();

            foreach (var composeTask in getComposesTasks)
            {
                var compose = await composeTask;
                composes.Add(compose);
            }
            return composes;
        }

        private static async Task<ComposeDto> GettingComposeByProductId(ProductResponse product)
        {
            var productsApi = RestService.For<IProductsAdditionalsApi>($"{ApiConst.RossmannPortalUrl}/products/api");

            ResponseProductsAdditionals productAdditionals = null;
            try
            {
                productAdditionals = await productsApi.Get(product.Id);
            }
            catch (ApiException ex)
            {
                var statusCode = ex.StatusCode;
                if (statusCode == HttpStatusCode.NoContent)
                {
                    return new ComposeDto(product.Id, string.Empty);
                }
            }

            var productCompose = string.Join("", productAdditionals?.Data
                .Where(h => h.Type == "CharacterComponents")
                .Select(x => x.Html) ?? Array.Empty<string>());

            var html = new HtmlDocument();
            html.LoadHtml(productCompose);
            HtmlUtils.RemoveStyles(html);
            var noHtml = HtmlUtils.ConvertHtmlToString(html);

            return new ComposeDto(product.Id, noHtml);
        }

        private static async Task<IEnumerable<ProductResponse>> GettingProductsByCategoryId(KeyValuePair<int, string> category)
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
