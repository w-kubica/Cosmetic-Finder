using Cosmetic_Finder.Common.Domain.Model;
using Cosmetic_Finder.Common.Infrastructure.Models;

namespace Cosmetic_Finder.Common.Infrastructure.Mappers
{
    public static class CosmeticMapper
    {
        private static SolrCosmetic ToInfrastructure(this Cosmetic cosmetic)
        {
            return new SolrCosmetic()
            {
                Id = cosmetic.Id,
                Url = cosmetic.NavigateUrl,
                Brand = cosmetic.Brand,
                Caption = cosmetic.Caption,
                Category = cosmetic.Category,
                Compose = cosmetic.Compose,
                Price = cosmetic.Price
            };
        }

        public static IEnumerable<SolrCosmetic> ToInfrastructure(this IEnumerable<Cosmetic> cosmetics)
        {
            return cosmetics.Select(b => b.ToInfrastructure());
        }

        public static Cosmetic ToDomain(this SolrCosmetic solrCosmetic)
        {
            return new Cosmetic()
            {
                Id = solrCosmetic.Id,
                NavigateUrl = solrCosmetic.Url,
                Brand = solrCosmetic.Brand,
                Caption = solrCosmetic.Caption,
                Category = solrCosmetic.Category,
                Compose = solrCosmetic.Compose,
                Price = solrCosmetic.Price
            };
        }

        public static IEnumerable<Cosmetic> ToDomain(this IEnumerable<SolrCosmetic> cosmetics)
        {
            return cosmetics.Select(b => b.ToDomain());
        }

    }
}
