using Cosmetic_Finder.Application.DTO;

namespace Cosmetic_Finder.Application.Services;
public interface ITagService
{

    Task<IEnumerable<TagDto>> GetTagsAsync();
    Task<TagDto> GetTagByIdAsync(int id);
    Task AddTagAsync(TagDto tag);
    Task UpdateTagAsync(TagDto tag);
    Task DeleteTagAsync(int id);
}
