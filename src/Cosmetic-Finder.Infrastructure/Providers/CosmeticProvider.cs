using System.Net;
using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.DTO.Rossmann;
using Cosmetic_Finder.Infrastructure.Gateways;
using Cosmetic_Finder.Infrastructure.UtilsHtml;
using HtmlAgilityPack;
using Refit;

namespace Cosmetic_Finder.Infrastructure.Providers;

public static class CosmeticProvider
{
    public static async Task<IEnumerable<Product>> ImportProducts(int category)
    {
        
        var getProductsTasks = new List<Task<IEnumerable<Product>>>();

        //foreach (var category in Categories.CosmeticCategories)
        //{
            var task = GettingProductsByCategoryId(category);
            getProductsTasks.Add(task);
        //}

        await Task.WhenAll(getProductsTasks);

        var products = new List<Product>();

        // wyłuskanie danych
        foreach (var productTask in getProductsTasks)
        {
            var product = await productTask;
            products.AddRange(product);
        }

        return products;
    }

    public static async Task<List<ComposeDto>> ImportComposes(IEnumerable<Product> products)
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

    private static async Task<ComposeDto> GettingComposeByProductId(Product product)
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

    private static async Task<IEnumerable<Product>> GettingProductsByCategoryId(int categoryId)
    {
        //var categoryId = category.Key;

        var productsApi = RestService.For<IProductsApi>($"{ApiConst.RossmannPortalUrl}/products/api");
        var request = await productsApi.Get(categoryId, 1);
        var totalPage = request.Data.TotalPages;

        var getProductsTasks = new List<Task<ResponseProducts>>();

        for (var k = 1; k <= totalPage; k++)
        {
            var task = productsApi.Get(categoryId, k);
            getProductsTasks.Add(task);
        }

        await Task.WhenAll(getProductsTasks);

        var products = new List<List<Product>>();

        // wyłuskanie danych
        foreach (var productTask in getProductsTasks)
        {
            var product = (await productTask).Data.Products;
            products.Add(product);
        }

        //var te = (await productsApi.Get(categoryId, 10)).Data.Products;

        var productsNew = new List<Product>();

        foreach (var item in products)
        {
            foreach (var product in item)
            {
                product.MainCategoryId = categoryId;
                productsNew.Add(product);
            }
        }
        return productsNew;
    }
}
