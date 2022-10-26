using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.DTO;

namespace Cosmetic_Finder.Infrastructure.Mappers;

public static class CosmeticMapper
{
    public static SolrCosmetic ToInfrastructure(this Cosmetic cosmetic)
    {
        return new SolrCosmetic
        {
            Id = cosmetic.Id,
            Brand = cosmetic.Brand,
            Caption = cosmetic.Caption,
            Category = cosmetic.Category,
            Compose = cosmetic.Compose,
            Price = cosmetic.Price,
            MainCategory_Id = cosmetic.MainCategoryId,
            Picture = cosmetic.Picture,
            Url = cosmetic.NavigateUrl
        };
    }

    public static Cosmetic ToDomain(this SolrCosmetic solrCosmetic)
    {
        return new Cosmetic
        {
            Id = solrCosmetic.Id,
            Brand = solrCosmetic.Brand,
            Caption = solrCosmetic.Caption,
            Category = solrCosmetic.Category,
            Compose = solrCosmetic.Compose,
            Price = solrCosmetic.Price,
            MainCategoryId = solrCosmetic.MainCategory_Id,
            Picture = solrCosmetic.Picture,
            NavigateUrl = solrCosmetic.Url
        };
    }
}
