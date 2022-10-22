using Cosmetic_Finder.Application.DTO;
using Cosmetic_Finder.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_Finder.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;
    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> Get()
    {
        var tags = await _tagService.GetTagsAsync();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> Get(int id)
    {
        var tag = await _tagService.GetTagByIdAsync(id);
        return Ok(tag);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromQuery] TagDto tag)
    {
        await _tagService.AddTagAsync(tag);
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromQuery] TagDto tag)
    {
        await _tagService.UpdateTagAsync(tag);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _tagService.DeleteTagAsync(id);
        return NoContent();
    }
}
