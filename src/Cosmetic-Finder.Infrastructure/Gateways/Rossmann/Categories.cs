namespace Cosmetic_Finder.Infrastructure.Gateways.Rossmann;

public class Categories
{
    public List<Datum> Data { get; set; }
}
public class Datum
{
    public List<Child> Children { get; set; }
    public int ProductCount { get; set; }
    public string PhotoUrlWide { get; set; }
    public string PhotoUrlSmall { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string NavigateUrl { get; set; }
    public int Priority { get; set; }
}
public class Child
{
    public List<Child> Children { get; set; }
    public int ProductCount { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string NavigateUrl { get; set; }
    public int Priority { get; set; }
}





