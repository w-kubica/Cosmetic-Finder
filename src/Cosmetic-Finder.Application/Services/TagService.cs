using Cosmetic_Finder.Application.DTO;
using Cosmetic_Finder.Application.Mapper;
using Cosmetic_Finder.Core.Repositories;

namespace Cosmetic_Finder.Application.Services;
public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<IEnumerable<TagDto>> GetTagsAsync()
    {
        var tags = await _tagRepository.GetAllAsync();
        return tags.Select(a => a.ToApplication());

    }
    public async Task<TagDto> GetTagByIdAsync(int id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);
        return tag.ToApplication();
    }

    public async Task AddTagAsync(TagDto tag)
    {
        await _tagRepository.UpdateAsync(tag.ToDomain());
    }

    public async Task UpdateTagAsync(TagDto tag)
    {
        await _tagRepository.UpdateAsync(tag.ToDomain());
    }
   

    public async Task DeleteTagAsync(int id) => throw new NotImplementedException();
}
