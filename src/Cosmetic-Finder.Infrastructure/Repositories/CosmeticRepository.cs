using System.Globalization;
using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Core.Repositories;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.Mappers;
using SolrNet;
using SolrNet.Commands.Parameters;

namespace Cosmetic_Finder.Infrastructure.Repositories;

public class CosmeticRepository : ICosmeticRepository
{
    private readonly ISolrOperations<SolrCosmetic> _solr;

    private static readonly string BrandDtoFields = $"{SolrCosmetic.CosmeticId}, {SolrCosmetic.CosmeticCategory}, {SolrCosmetic.CosmeticBrand}, {SolrCosmetic.CosmeticCaption}, {SolrCosmetic.CosmeticPrice},{SolrCosmetic.NavigateUrl},{SolrCosmetic.CosmeticCompose},{SolrCosmetic.MainCategoryId}";

    public CosmeticRepository(ISolrOperations<SolrCosmetic> solr)
    {
        _solr = solr;
    }

    public async Task<bool> AddOrUpdateCosmetics(IEnumerable<Cosmetic> cosmetics)
    {
        var solrCosmetic = cosmetics.Select(b => b.ToInfrastructure());
        var result = await _solr.AddRangeAsync(solrCosmetic);

        return result.Status == 0;
    }

    public async Task<IEnumerable<Cosmetic>> GetCosmetics(string search, int mainCategoryId, bool shouldContainCompose, int pageNumber, int pageSize, string sortField, bool ascending, CancellationToken cancellationToken)
    {
        var options = new QueryOptions
        {
            Fields = new List<string> { BrandDtoFields },
            OrderBy = new[] { new SortOrder(sortField, ascending ? Order.ASC : Order.DESC) },
            StartOrCursor = new StartOrCursor.Start((pageNumber - 1) * pageSize),
            Rows = pageSize
        };

        var searchArray = search.Split(",");

        options.FilterQueries = new List<ISolrQuery>();

        if (!string.IsNullOrEmpty(search))
        {
            options.FilterQueries.Add(new SolrQueryByField(SolrCosmetic.MainCategoryId, mainCategoryId.ToString(CultureInfo.InvariantCulture)));
            if (!shouldContainCompose)
            {
                foreach (var item in searchArray)
                {
                    NotContainComposeFilter(options, item);
                }
                AdditionalConditions(options);
            }
            if(shouldContainCompose)
            {
                foreach (var item in searchArray)
                {
                    ContainComposeFilter(options, item);
                }
            }
        }

        var result = await _solr.QueryAsync(SolrQuery.All, options, cancellationToken);
        return result.Select(a => a.ToDomain());
    }

    public async Task<int> GetAllCountAsync(string search, int mainCategoryId, bool shouldContainCompose,
       CancellationToken cancellationToken)
    {
        var options = new QueryOptions
        {
            Fields = new List<string> { BrandDtoFields }

        };
        var searchArray = search.Split(",");

        options.FilterQueries = new List<ISolrQuery>();

        if (!string.IsNullOrEmpty(search))
        {
            options.FilterQueries.Add(new SolrQueryByField(SolrCosmetic.MainCategoryId, mainCategoryId.ToString(CultureInfo.InvariantCulture)));
            if (!shouldContainCompose)
            {
                foreach (var item in searchArray)
                {
                    NotContainComposeFilter(options, item);
                }
                AdditionalConditions(options);
            }
            if (shouldContainCompose)
            {
                foreach (var item in searchArray)
                {
                    ContainComposeFilter(options, item);
                }
               
            }
        }
        var result = await _solr.QueryAsync(SolrQuery.All, options, cancellationToken);
        return result.NumFound;
    }

    private static void NotContainComposeFilter(QueryOptions options, string item)
    {
        options.FilterQueries.Add(!new SolrQueryByField(SolrCosmetic.LowerCompose, item));
    }

    private static void ContainComposeFilter(QueryOptions options, string item)
        => options.FilterQueries.Add(new SolrQueryByField(SolrCosmetic.LowerCompose, item));

    private static void AdditionalConditions(QueryOptions options)
    {
        options.FilterQueries.Add(!new SolrQueryByField(SolrCosmetic.CosmeticCompose, "Brak danych"));
        options.FilterQueries.Add(!new SolrQueryByField(SolrCosmetic.CosmeticCompose, "Brak danych;"));
        options.FilterQueries.Add(!new SolrQueryByField(SolrCosmetic.CosmeticCompose, ""));
    }
}
