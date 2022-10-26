namespace Cosmetic_Finder.Infrastructure.DTO.Rossmann;

public class ResponseProducts
{
    public Data Data { get; set; }
}
public class Data
{
    public List<Product> Products { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages { get; set; }
}
public class Product
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Name { get; set; }
    public string Caption { get; set; }
    public int MainCategoryId { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public string NavigateUrl { get; set; }
    public List<Picture> Pictures { get; set; }

}
public class Picture
{
    public int Id { get; set; }
    public string Large { get; set; }
    public int Type { get; set; }

}

