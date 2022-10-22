using Cosmetic_Finder.Core.Model;

namespace Cosmetic_Finder.Core.Repositories;
public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync();
    Task<Tag> GetByIdAsync(int id);
    Task AddAsync(Tag tag);
    Task UpdateAsync(Tag tag);
    Task DeleteAsync(Tag tag);
}
