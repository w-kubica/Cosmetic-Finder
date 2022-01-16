using CommonServiceLocator;
using Cosmetic_Finder.Importer.Domain.Model;
using Cosmetic_Finder.Importer.Infrastructure.Mappers;
using Cosmetic_Finder.Importer.Infrastructure.Models;
using SolrNet;
using SolrNet.Commands.Parameters;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmetic_Finder.Importer.Infrastructure.Repositories
{
    public static class CosmeticRepository
    {
        private const string BrandDtoFields = $"{SolrCosmetic.CosmeticId}, {SolrCosmetic.CosmeticCategory}, {SolrCosmetic.CosmeticBrand}, {SolrCosmetic.CosmeticCaption}, {SolrCosmetic.CosmeticPrice},{SolrCosmetic.NavigateUrl},{SolrCosmetic.CosmeticCompose}";

        public static async Task<bool> AddOrUpdateCosmetics(IEnumerable<Cosmetic> cosmetics)
        {
            var solrCosmetic = cosmetics.ToInfrastructure();
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrCosmetic>>();
            var result = await solr.AddRangeAsync(solrCosmetic);

            return result.Status == 0;
        }

        public static async Task<IEnumerable<Cosmetic>> GetCosmetics(string search, bool contains, bool sortByPriceAsc, CancellationToken cancellationToken)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrCosmetic>>();
            var options = new QueryOptions
            {
                Fields = new List<string> { BrandDtoFields },
                OrderBy = new[] { new SortOrder(SolrCosmetic.CosmeticPrice, sortByPriceAsc ? Order.ASC : Order.DESC) }
            };
            
            if (!string.IsNullOrEmpty(search))
            {
                if (contains)
                {
                    options.FilterQueries = new List<ISolrQuery>
                        {
                            new SolrQueryByField(SolrCosmetic.LowerCompose, search),
                        };
                }
                else if (!contains)
                {
                    options.FilterQueries = new List<ISolrQuery>
                        {
                            !new SolrQueryByField(SolrCosmetic.LowerCompose, search),
                        };
                }
            }

            var result = await solr.QueryAsync(SolrQuery.All, options, cancellationToken);

            return result.ToDomain();
        }
    }
}

