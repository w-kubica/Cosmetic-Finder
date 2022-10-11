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

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, [FromQuery] PaginationFilter paginationFilter, [FromQuery] string search = "ascorbic acid", [FromQuery] bool shouldContainCompose = true, [FromQuery] int mainCategoryId = 8686, [FromQuery] bool sort = true, [FromQuery] bool sortByPriceAsc = false)
    {
        var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

        var cosmetics = await _cosmeticService.GetCosmetics(search, mainCategoryId, shouldContainCompose, sort, sortByPriceAsc, validPaginationFilter.PageNumber, validPaginationFilter.PageSize,
            cancellationToken);

        var totalRecords = await _cosmeticService.GetAllCountAsync(search, mainCategoryId, shouldContainCompose, sort, sortByPriceAsc, cancellationToken);

        var result = PaginationHelper.CreatePagedResponse(cosmetics, validPaginationFilter, totalRecords);

        return Ok(result);
    }
}
