using Cosmetic_Finder.Application.DTO;
using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Application.Mapper;
public static class TagMapper
{
    public static TagDto ToApplication(this Tag tag)
        => new(tag.Id, tag.TagName, tag.TagValue);

    public static Tag ToDomain(this TagDto tag)
        => new(tag.Id, tag.TagName, tag.TagValue);
}
