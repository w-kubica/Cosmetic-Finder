using Cosmetic_Finder.Application.DTO;
using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Application.Mapper;
public static class ProductMapper
{
    public static Cosmetic ToDomain(this ProductDto product) =>
        new(product.Id, product.Brand, product.Name, product.Caption, product.Compose, product.Price, product.OldPrice,
            product.MainCategoryId , product.Category, product.NavigateUrl, product.Picture);
}
