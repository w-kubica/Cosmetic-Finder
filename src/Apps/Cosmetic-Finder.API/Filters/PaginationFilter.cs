namespace Cosmetic_Finder.API.Filters;

public class PaginationFilter
{
    public const int MaxPageSize = 100;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }


    public PaginationFilter()
    {
        PageNumber = 1;
        PageSize = MaxPageSize;
    }

    public PaginationFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
    }
}
