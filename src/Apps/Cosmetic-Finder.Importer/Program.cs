using System.IO;
using System.Threading.Tasks;
using Cosmetic_Finder.Application.Services;
using Cosmetic_Finder.Core.Repositories;
using Cosmetic_Finder.Infrastructure.DTO;
using Cosmetic_Finder.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SolrNet;

namespace Cosmetic_Finder.Importer;

public class Program
{
    private static async Task Main()
    {

        var builder = new ConfigurationBuilder();
        BuildConfig(builder);
        var config = builder.Build();

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<IImportService, ImportService>();
                services.AddTransient<ICosmeticRepository, CosmeticRepository>();
                services.AddSolrNet<SolrCosmetic>(config.GetConnectionString("Solr"));
            })
            .Build();

        var importService = ActivatorUtilities.CreateInstance<ImportService>(host.Services);
        await importService.ImportProducts();
    }

    private static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}
