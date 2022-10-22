using Cosmetic_Finder.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cosmetic_Finder.Infrastructure.Data;
public class TagConfiguration : IEntityTypeConfiguration<TagDb>
{
    public void Configure(EntityTypeBuilder<TagDb> builder)
    {
        builder.ToTable("Tags");
        builder.HasKey(x => x.Id);
    }
}
