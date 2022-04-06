using Cosmetic_Finder.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_Finder.API.Controllers
{
    [Route("[controller]")]
    public class CosmeticsController : Controller
    {
        private readonly ICosmeticService _cosmeticService;

        public CosmeticsController(ICosmeticService cosmeticService)
        {
            _cosmeticService = cosmeticService;
        }

        [HttpGet("{search}")]
        public async Task<IActionResult> Get(string search, CancellationToken cancellationToken)
        {
            var mainCategoryId = 8686;
            var shouldContainCompose = false;
            var sort = false;
            var sortByPriceAsc = false;

            var cosmetics = await _cosmeticService.GetCosmetics(search, mainCategoryId, shouldContainCompose, sort, sortByPriceAsc,
                cancellationToken);

            return Ok(cosmetics);
        }

    }
}
