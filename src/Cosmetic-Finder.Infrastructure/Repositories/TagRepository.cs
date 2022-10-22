using Cosmetic_Finder.Core.Model;
using Cosmetic_Finder.Core.Repositories;
using Cosmetic_Finder.Infrastructure.Data;
using Cosmetic_Finder.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Cosmetic_Finder.Infrastructure.Repositories;
public class TagRepository : ITagRepository
{
    private readonly CosmeticFinderContext _context;

    public TagRepository(CosmeticFinderContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        var tags = await _context.Tags.ToListAsync();
        return tags.Select(a => a.ToDomain());
    }

    public async Task<Tag> GetByIdAsync(int id)
    {
        var tag = await _context.Tags.SingleOrDefaultAsync(t => t.Id == id);
        return tag.ToDomain();
    }

    public async Task AddAsync(Tag tag)
    {
        await _context.AddAsync(tag.ToInfrastructure());
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tag tag)
    {
        _context.Tags.Update(tag.ToInfrastructure());
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tag tag)
    {
        _context.Remove(tag.ToInfrastructure());
        await _context.SaveChangesAsync();
    }
}
