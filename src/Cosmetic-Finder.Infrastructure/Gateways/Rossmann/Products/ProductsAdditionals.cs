namespace Cosmetic_Finder.Infrastructure.Gateways.Rossmann.Products;

public class ProductsAdditionals
{
    public List<Datum> Data { get; set; }
}

public class Datum
{
    public string Type { get; set; }
    public string Html { get; set; }
}
