using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Core.Repositories;

namespace Cosmetic_Finder.Application.Services;

public class CosmeticService : ICosmeticService
{
    private readonly ICosmeticRepository _cosmeticRepository;

    public CosmeticService(ICosmeticRepository cosmeticRepository)
    {
        _cosmeticRepository = cosmeticRepository;
    }

    public async Task<IEnumerable<Cosmetic>> GetCosmetics(string search, int mainCategoryId, bool shouldContainCompose,
        bool sort, bool sortByPriceAsc, CancellationToken cancellationToken)
    {
        var cosmetics = await _cosmeticRepository.GetCosmetics(search, mainCategoryId, shouldContainCompose, sort, sortByPriceAsc,
            cancellationToken);

        return cosmetics;
    }
}
