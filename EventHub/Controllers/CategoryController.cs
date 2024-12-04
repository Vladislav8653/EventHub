using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.Services.Contracts;
using EventHub.Validators.Category.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    private ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidateCategoryDtoAttribute))]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto item)
    {
        var newEvent = await _categoryService.CreateAsync(item);
        return Ok(newEvent);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        _categoryService.Delete(id);
        return Ok();
    }
}