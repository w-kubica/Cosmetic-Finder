using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.Gateways.Rossmann;

namespace Cosmetic_Finder.Infrastructure.Mappers;
public static class CategoriesMapper
{
    public static IEnumerable<Category> ToDomain(this IEnumerable<Datum> categories)
        => categories.Select(category => new Category()
        {
            Id = category.Id,
            Name = category.Name
        });
}
