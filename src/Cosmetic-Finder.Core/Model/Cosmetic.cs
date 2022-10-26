namespace Cosmetic_Finder.Core.Model;

public record Cosmetic
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Caption { get; set; }
    public string Compose { get; set; }
    public double Price { get; set; }
    public int MainCategoryId { get; set; }
    public string Category { get; set; }
    public string NavigateUrl { get; set; }
    public string Picture { get; set; }

    public Cosmetic(int id, string brand, string caption, string compose, double price, int mainCategoryId, string category, string navigateUrl, string picture)
    {
        Id = id;
        Brand = brand;
        Caption = caption;
        Compose = compose;
        Price = price;
        MainCategoryId = mainCategoryId;
        Category = category;
        NavigateUrl = navigateUrl;
        Picture = picture;
    }

    public Cosmetic()
    {

    }
}
