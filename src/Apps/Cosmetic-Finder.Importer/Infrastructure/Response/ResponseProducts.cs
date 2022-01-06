using System.Collections.Generic;

namespace Cosmetic_Finder.Importer.Infrastructure.Response
{
    public class ResponseProducts
    {
        public Data Data { get; set; }

    }
    public class Product
    {
        public int Id { get; set; }
        public string NavigateUrl { get; set; }
        public string Brand { get; set; }
        public string Caption { get; set; }
        public string Category { get; set; }
    }

    public class Data
    {
        public List<Product> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
