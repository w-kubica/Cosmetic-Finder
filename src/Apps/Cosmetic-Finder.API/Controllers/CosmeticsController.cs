using Cosmetic_Finder.API.Filters;
using Cosmetic_Finder.API.Helpers;
using Cosmetic_Finder.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_Finder.API.Controllers;

[Route("[controller]")]
public class CosmeticsController : Controller
{
    private readonly ICosmeticService _cosmeticService;

    public CosmeticsController(ICosmeticService cosmeticService)
    {
        _cosmeticService = cosmeticService;
    }

    [HttpGet("[action]")]
    public IActionResult GetSortField()
    {
        return Ok(SortingHelper.GetSortFields().Select(s => s.Key));
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, [FromQuery] PaginationFilter paginationFilter, [FromQuery] SortingFilter sortingFilter, [FromQuery] string search = "ascorbic acid", [FromQuery] bool shouldContainCompose = true, [FromQuery] int mainCategoryId = 8686)
    {
        var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
        var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

        var cosmetics = await _cosmeticService.GetCosmetics(search, mainCategoryId, shouldContainCompose, validPaginationFilter.PageNumber, validPaginationFilter.PageSize, validSortingFilter.SortField, validSortingFilter.Ascending,
            cancellationToken);

        var totalRecords =
            await _cosmeticService.GetAllCountAsync(search, mainCategoryId, shouldContainCompose, cancellationToken);

        var result = PaginationHelper.CreatePagedResponse(cosmetics, validPaginationFilter, totalRecords);

        return Ok(result);
    }
}
