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

        string[] searchArray = search.Split(",");

        var queryList = new List<ISolrQuery>();
        if (!string.IsNullOrEmpty(search))
        {
            if (shouldContainCompose)
            {

                foreach (var item in searchArray)
                {
                    queryList.Add(new SolrMultipleCriteriaQuery(new List<ISolrQuery>
                    {
                        new SolrQueryByField(SolrCosmetic.LowerCompose, item)

                    }, "AND"));
                }
                queryList.Add(new SolrQueryByField(SolrCosmetic.MainCategoryId,
                    mainCategoryId.ToString(CultureInfo.InvariantCulture)));
            }
            else
            {
                foreach (var item in searchArray)
                {
                    queryList.Add(!new SolrMultipleCriteriaQuery(new List<ISolrQuery>
                    {
                        new SolrQueryByField(SolrCosmetic.LowerCompose, item)

                    }, "AND"));
                }
                queryList.Add(new SolrQueryByField(SolrCosmetic.MainCategoryId,
                    mainCategoryId.ToString(CultureInfo.InvariantCulture)));
            }
        }
        var finalQuery = new SolrMultipleCriteriaQuery(queryList, "AND");

        var result = await _solr.QueryAsync(finalQuery, cancellationToken);

        return result.Select(a => a.ToDomain());
    }

    public async Task<int> GetAllCountAsync(string search, int mainCategoryId, bool shouldContainCompose,
       CancellationToken cancellationToken)
    {
        var options = new QueryOptions
        {
            Fields = new List<string> { BrandDtoFields }

        };
        string[] searchArray = search.Split(",");

        var queryList = new List<ISolrQuery>();

        if (shouldContainCompose)
        {

            foreach (var item in searchArray)
            {
                queryList.Add(new SolrMultipleCriteriaQuery(new List<ISolrQuery>
                {
                    new SolrQueryByField(SolrCosmetic.LowerCompose, item)

                }, "AND"));
            }
            queryList.Add(new SolrQueryByField(SolrCosmetic.MainCategoryId,
                mainCategoryId.ToString(CultureInfo.InvariantCulture)));
        }
        else
        {
            foreach (var item in searchArray)
            {
                queryList.Add(!new SolrMultipleCriteriaQuery(new List<ISolrQuery>
                {
                    new SolrQueryByField(SolrCosmetic.LowerCompose, item)

                }, "AND"));
            }
            queryList.Add(new SolrQueryByField(SolrCosmetic.MainCategoryId,
                mainCategoryId.ToString(CultureInfo.InvariantCulture)));
        }
        var finalQuery = new SolrMultipleCriteriaQuery(queryList, "AND");

        var result = await _solr.QueryAsync(finalQuery, cancellationToken);
        return result.NumFound;
    }
}
