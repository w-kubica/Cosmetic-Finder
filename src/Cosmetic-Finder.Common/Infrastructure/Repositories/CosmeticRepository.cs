using CommonServiceLocator;
using Cosmetic_Finder.Common.Domain.Model;
using Cosmetic_Finder.Common.Infrastructure.Mappers;
using Cosmetic_Finder.Common.Infrastructure.Models;
using SolrNet;
using SolrNet.Commands.Parameters;

namespace Cosmetic_Finder.Common.Infrastructure.Repositories
{
    public static class CosmeticRepository
    {
        private static readonly string BrandDtoFields = $"{SolrCosmetic.CosmeticId}, {SolrCosmetic.CosmeticCategory}, {SolrCosmetic.CosmeticBrand}, {SolrCosmetic.CosmeticCaption}, {SolrCosmetic.CosmeticPrice},{SolrCosmetic.NavigateUrl},{SolrCosmetic.CosmeticCompose},{SolrCosmetic.MainCategoryId}";

        public static async Task<bool> AddOrUpdateCosmetics(IEnumerable<Cosmetic> cosmetics)
        {
            var solrCosmetic = cosmetics.ToInfrastructure();
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrCosmetic>>();
            var result = await solr.AddRangeAsync(solrCosmetic);

            return result.Status == 0;
        }

        public static async Task<IEnumerable<Cosmetic>> GetCosmetics(string search, int mainCategoryId, bool shouldContainCompose, bool sort, bool sortByPriceAsc, CancellationToken cancellationToken)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrCosmetic>>();
            var options = new QueryOptions
            {
                Fields = new List<string> { BrandDtoFields },
            };
            if (sort)
            {
                options.OrderBy = new[] { new SortOrder(SolrCosmetic.CosmeticPrice, sortByPriceAsc ? Order.ASC : Order.DESC) };
            }

            if (!string.IsNullOrEmpty(search))
            {
                if (shouldContainCompose)
                {
                    options.FilterQueries = new List<ISolrQuery>
                        {
                            new SolrQueryByField(SolrCosmetic.MainCategoryId, mainCategoryId.ToString()),
                            new SolrQueryByField(SolrCosmetic.LowerCompose, search),
                        };
                }
                else
                {
                    options.FilterQueries = new List<ISolrQuery>
                        {
                            new SolrQueryByField(SolrCosmetic.MainCategoryId, mainCategoryId.ToString()),
                            !new SolrQueryByField(SolrCosmetic.LowerCompose, search),
                        };
                }
            }

            var result = await solr.QueryAsync(SolrQuery.All, options, cancellationToken);

            return result.ToDomain();
        }
    }
}

