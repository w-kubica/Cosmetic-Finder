namespace Cosmetic_Finder.API.Helpers;

public class SortingHelper
{
    public static KeyValuePair<string, string>[] GetSortFields()
    {
        return new[] { SortFields.Brand, SortFields.Price };
    }
}

public class SortFields
{
    public static KeyValuePair<string, string> Brand = new KeyValuePair<string, string>("brand", "brand");
    public static KeyValuePair<string, string> Price = new KeyValuePair<string, string>("price", "price");
}
