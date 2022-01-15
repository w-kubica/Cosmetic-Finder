using CommonServiceLocator;
using Cosmetic_Finder.Importer.Domain.Model;
using Cosmetic_Finder.Importer.Infrastructure.Mappers;
using Cosmetic_Finder.Importer.Infrastructure.Models;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cosmetic_Finder.Importer.Infrastructure.Repositories
{
    public static class CosmeticRepository

    {
        private static readonly ISolrOperations<SolrCosmetic> _solrOperations;

        private static readonly string _brandDtoFields = $"{SolrCosmetic.CosmeticId}, {SolrCosmetic.CosmeticCategory}, {SolrCosmetic.CosmeticBrand}, {SolrCosmetic.CosmeticCaption}, {SolrCosmetic.CosmeticPrice},{SolrCosmetic.NavigateUrl},{SolrCosmetic.CosmeticCompose}";

        public static async Task AddOrUpdateCosmetics(IEnumerable<Cosmetic> cosmetics)
        {
            var solrCosmetic = cosmetics.ToInfrastructure();
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrCosmetic>>();
            var result = await solr.AddRangeAsync(solrCosmetic);
           
        }

        public static async Task<IEnumerable<Cosmetic>> GetCosmetics(string? search, CancellationToken cancellationToken)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<SolrCosmetic>>();
            var options = new QueryOptions
            {
                Fields = new List<string> { _brandDtoFields },
                OrderBy = new[] { new SortOrder($"{SolrCosmetic.CosmeticPrice}", Order.DESC) },
            };

            if (!string.IsNullOrEmpty(search))
            {
                options.FilterQueries = new List<ISolrQuery>
                {
                    new SolrQueryByField(SolrCosmetic.LowerCompose, $"*{search}*"),
                };
            }

            var result = await solr.QueryAsync(SolrQuery.All, options,cancellationToken);

            return result.ToDomain();
        }
    }
}

