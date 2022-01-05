using System.Collections.Generic;

namespace Cosmetic_Finder.Importer.Response
{
    public class ResponseCategory
    {
        public Data Data { get; set; }

    }
    public class Product
    {
        public int Id { get; set; }
        public string NavigateUrl { get; set; }
        // public string brand { get; set; }
        // public string caption { get; set; }
        // public string Category { get; set; }
    }

    public class Data
    {
        public List<Product> products { get; set; }
        public int totalCount { get; set; }
    }
}
