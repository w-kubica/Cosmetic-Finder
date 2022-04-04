namespace Cosmetic_Finder.Common.Infrastructure.Gateways.Response
{
    public class ResponseProducts
    {
        public Data Data { get; set; }

    }

    // todo: productreponse and productreponseapi
    public class Product
    {
        public int Id { get; set; }
        public string NavigateUrl { get; set; }
        public string Brand { get; set; }
        public string Caption { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int MainCategoryId { get; set; }
    }

    public class Data
    {
        public List<Product> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
