namespace Cosmetic_Finder.Infrastructure.Gateways.Response;

public class ResponseProducts
{
    public Data Data { get; set; }
}

public class ProductResponse
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public string Caption { get; set; }
    public int MainCategoryId { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public double OldPrice { get; set; }
    public string NavigateUrl { get; set; }
    public List<Picture> Pictures { get; set; }

}
public class Picture
{
    public int Id { get; set; }
    public string Large { get; set; }
    public string Type { get; set; }

}

public class Data
{
    public List<ProductResponse> Products { get; set; }

    public int TotalCount { get; set; }
}
