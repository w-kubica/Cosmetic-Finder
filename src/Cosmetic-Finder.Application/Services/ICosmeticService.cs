using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Application.Services;

public interface ICosmeticService
{
    public Task<IEnumerable<Cosmetic>> GetCosmetics(string search, int mainCategoryId,
        bool shouldContainCompose, int pageNumber, int pageSize, string sortField, bool ascending, CancellationToken cancellationToken);

    public Task<int> GetAllCountAsync(string search, int mainCategoryId,
        bool shouldContainCompose, CancellationToken cancellationToken);

}


