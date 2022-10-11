using Cosmetic_Finder.API.Filters;
using Cosmetic_Finder.API.Wrappers;

namespace Cosmetic_Finder.API.Helpers;

public class PaginationHelper
{
    public static PagedResponse<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> pagedData,
        PaginationFilter validPaginationFilter, int totalRecords)
    {
        var response = new PagedResponse<IEnumerable<T>>(pagedData, validPaginationFilter.PageNumber,
            validPaginationFilter.PageSize);

        var totalPages = totalRecords / (double)validPaginationFilter.PageSize;
        var roundedTotalPages = (int)Math.Ceiling(totalPages);
        var currentPage = validPaginationFilter.PageNumber;

        response.TotalPages = roundedTotalPages;
        response.TotalRecords = totalRecords;
        response.PreviousPage = currentPage > 1;
        response.NextPage = currentPage < roundedTotalPages;

        return response;

    }
}
