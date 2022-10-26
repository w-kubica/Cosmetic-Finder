using Cosmetic_Finder.Application.DTO;
using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Application.Mapper;
public static class CategoryMapper
{
    public static CategoryDto ToApplication(this Category category)
        => new(category.Id,category.Name);

}
