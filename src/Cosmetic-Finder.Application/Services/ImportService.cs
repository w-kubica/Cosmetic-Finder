using Cosmetic_Finder.Core.Repositories;
using Cosmetic_Finder.Infrastructure.Mappers;
using Cosmetic_Finder.Infrastructure.Providers;

namespace Cosmetic_Finder.Application.Services;
public class ImportService : IImportService
{
    private readonly ICosmeticRepository _cosmeticRepository;
    public ImportService(ICosmeticRepository cosmeticRepository)
    {
        _cosmeticRepository = cosmeticRepository;
    }

    public async Task ImportProducts()
    {
        var products = await CosmeticProvider.ImportProducts();

        var composes = await CosmeticProvider.ImportComposes(products);

        var cosmetics = products.ToDomainCosmetic(composes);

        await _cosmeticRepository.AddOrUpdateCosmetics(cosmetics);
    }
}
