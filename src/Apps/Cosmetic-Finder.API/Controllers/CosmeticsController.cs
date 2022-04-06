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
    public async Task<IActionResult> Get([FromQuery] string search, [FromQuery] int mainCategoryId, [FromQuery] bool sort, [FromQuery] bool sortByPriceAsc,
        [FromQuery] bool shouldContainCompose, CancellationToken cancellationToken)
    {
        var cosmetics = await _cosmeticService.GetCosmetics(search, mainCategoryId, shouldContainCompose, sort, sortByPriceAsc,
            cancellationToken);

        return Ok(cosmetics);
    }

}
