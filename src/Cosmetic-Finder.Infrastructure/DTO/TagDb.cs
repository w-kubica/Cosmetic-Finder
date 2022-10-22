namespace Cosmetic_Finder.Infrastructure.DTO;

public class TagDb
{
    public int Id { get; set; }
    public string TagName { get; set; }
    public string TagValue { get; set; }

    public TagDb(int id, string tagName, string tagValue)
    {
        Id = id;
        TagName = tagName;
        TagValue = tagValue;
    }

    public TagDb()
    {
        
    }
}
