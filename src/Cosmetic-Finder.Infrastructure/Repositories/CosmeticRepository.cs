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
            Start = (pageNumber - 1) * pageSize,
            Rows = pageSize
        };
        //if (sort)
        //{
        //    options.OrderBy = new[] { new SortOrder(sortField, ascending ? Order.ASC : Order.DESC) };
        //}

        if (!string.IsNullOrEmpty(search))
        {
            if (shouldContainCompose)
            {
                options.FilterQueries = new List<ISolrQuery>
                {
                    new SolrQueryByField(SolrCosmetic.MainCategoryId, mainCategoryId.ToString(CultureInfo.InvariantCulture)),
                    new SolrQueryByField(SolrCosmetic.LowerCompose, search),
                };
            }
            else
            {
                options.FilterQueries = new List<ISolrQuery>
                {
                    new SolrQueryByField(SolrCosmetic.MainCategoryId, mainCategoryId.ToString(CultureInfo.InvariantCulture)),
                    !new SolrQueryByField(SolrCosmetic.LowerCompose, search),
                };
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

        if (!string.IsNullOrEmpty(search))
        {
            if (shouldContainCompose)
            {
                options.FilterQueries = new List<ISolrQuery>
                {
                    new SolrQueryByField(SolrCosmetic.MainCategoryId, mainCategoryId.ToString(CultureInfo.InvariantCulture)),
                    new SolrQueryByField(SolrCosmetic.LowerCompose, search),
                };
            }
            else
            {
                options.FilterQueries = new List<ISolrQuery>
                {
                    new SolrQueryByField(SolrCosmetic.MainCategoryId, mainCategoryId.ToString(CultureInfo.InvariantCulture)),
                    !new SolrQueryByField(SolrCosmetic.LowerCompose, search),
                };
            }
        }


        var result = await _solr.QueryAsync(SolrQuery.All, options, cancellationToken);
        return result.NumFound;
    }
}
