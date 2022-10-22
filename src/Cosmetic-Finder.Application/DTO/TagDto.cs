namespace Cosmetic_Finder.Application.DTO;
public class TagDto
{
    public int Id { get; set; }
    public string TagName { get; set; }
    public string TagValue { get; set; }

    public TagDto(int id, string tagName, string tagValue)
    {
        Id = id;
        TagName = tagName;
        TagValue = tagValue;
    }

    public TagDto()
    {
        
    }
}

