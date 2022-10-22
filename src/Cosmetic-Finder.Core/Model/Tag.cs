namespace Cosmetic_Finder.Core.Model;
public  class Tag
{
    public int Id { get; set; }
    public string TagName { get; set; }
    public string TagValue { get; set; }

    public Tag(int id, string tagName, string tagValue)
    {
        Id = id;
        TagName = tagName;
        TagValue = tagValue;
    }
}
