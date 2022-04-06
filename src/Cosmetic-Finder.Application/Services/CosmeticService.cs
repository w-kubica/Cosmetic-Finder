using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Application.Services
{
    public class CosmeticService : ICosmeticService
    {
        public async Task<IEnumerable<Cosmetic>> GetCosmetics(string search, int mainCategoryId, bool shouldContainCompose,
            bool sort, bool sortByPriceAsc, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
