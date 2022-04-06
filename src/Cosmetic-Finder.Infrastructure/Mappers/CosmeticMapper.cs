using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Infrastructure.DTO;

namespace Cosmetic_Finder.Infrastructure.Mappers;

public static class CosmeticMapper
{
    private static SolrCosmetic ToInfrastructure(this Cosmetic cosmetic)
    {
        return new SolrCosmetic
        {
            Id = cosmetic.Id,
            Name = cosmetic.Name,
            Brand = cosmetic.Brand,
            Caption = cosmetic.Caption,
            Category = cosmetic.Category,
            Compose = cosmetic.Compose,
            OldPrice = cosmetic.OldPrice,
            Price = cosmetic.Price,
            MainCategory_Id = cosmetic.MainCategoryId,
            Picture = cosmetic.Picture,
            Url = cosmetic.NavigateUrl
        };
    }

    public static IEnumerable<SolrCosmetic> ToInfrastructure(this IEnumerable<Cosmetic> cosmetics)
    {
        return cosmetics.Select(b => b.ToInfrastructure());
    }

    public static Cosmetic ToDomain(this SolrCosmetic solrCosmetic)
    {
        return new Cosmetic
        {
            Id = solrCosmetic.Id,
            Name = solrCosmetic.Name,
            Brand = solrCosmetic.Brand,
            Caption = solrCosmetic.Caption,
            Category = solrCosmetic.Category,
            Compose = solrCosmetic.Compose,
            OldPrice = solrCosmetic.OldPrice,
            Price = solrCosmetic.Price,
            MainCategoryId = solrCosmetic.MainCategory_Id,
            Picture = solrCosmetic.Picture,
            NavigateUrl = solrCosmetic.Url
        };
    }

    public static IEnumerable<Cosmetic> ToDomain(this IEnumerable<SolrCosmetic> cosmetics)
    {
        return cosmetics.Select(b => b.ToDomain());
    }
}
