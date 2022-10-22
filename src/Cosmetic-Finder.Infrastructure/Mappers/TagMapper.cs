using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.DTO;

namespace Cosmetic_Finder.Infrastructure.Mappers;
public static class TagMapper
{
    public static TagDb ToInfrastructure(this Tag tag) =>
        new(tag.Id,tag.TagName,tag.TagValue);

    public static Tag ToDomain(this TagDb tag) =>
        new(tag.Id, tag.TagName, tag.TagValue);
}
