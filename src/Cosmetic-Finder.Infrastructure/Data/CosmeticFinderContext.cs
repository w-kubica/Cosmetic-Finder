using Cosmetic_Finder.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cosmetic_Finder.Infrastructure.Data;
public class CosmeticFinderContext : DbContext
{
    private readonly IConfiguration _configuration;
    public virtual DbSet<TagDb> Tags { get; set; } = null!;

    public CosmeticFinderContext(DbContextOptions<CosmeticFinderContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SQLServer"));
    //    optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TagConfiguration());
    }
}
