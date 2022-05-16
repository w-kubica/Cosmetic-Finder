using Cosmetic_Finder.Application.DTO;
using Cosmetic_Finder.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cosmetic_Finder.API.Controllers;

[Route("[controller]")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetCategories()
    {
        var category = _categoryService.GetCategories();

        return Ok(category);
    }
}
