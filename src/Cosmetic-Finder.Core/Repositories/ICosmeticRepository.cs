using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Core.Repositories;

public interface ICosmeticRepository
{
    public Task<bool> AddOrUpdateCosmetics(IEnumerable<Cosmetic> cosmetics, CancellationToken cancellationToken);
    public Task<IEnumerable<Cosmetic>> GetCosmetics(string search, int mainCategoryId,
        bool shouldContainCompose, bool sort, bool sortByPriceAsc, CancellationToken cancellationToken);
}
