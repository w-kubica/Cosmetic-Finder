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
         int pageNumber, int pageSize, string sortField, bool ascending, CancellationToken cancellationToken)
    {
        var cosmetics = await _cosmeticRepository.GetCosmetics(search, mainCategoryId, shouldContainCompose,
             pageNumber, pageSize, sortField, ascending, cancellationToken);

        return cosmetics;
    }

    public async Task<int> GetAllCountAsync(string search, int mainCategoryId, bool shouldContainCompose,
        CancellationToken cancellationToken)
    {
        return await _cosmeticRepository.GetAllCountAsync(search, mainCategoryId, shouldContainCompose,
            cancellationToken);
    }
}
