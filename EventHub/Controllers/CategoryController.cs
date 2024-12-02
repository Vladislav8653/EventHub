using BusinessLayer.DtoModels;
using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace EventHub.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    private ICategoryService _categoryService;
    private IValidator<CategoryDto> _createCategotyValidator;
    public CategoryController(ICategoryService categoryService, IValidator<CategoryDto> createCategotyValidator)
    {
        _categoryService = categoryService;
        _createCategotyValidator = createCategotyValidator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto item)
    {
        if (item == null!)
            return BadRequest("Body is null");
        var result = await _createCategotyValidator.ValidateAsync(item);
        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(vf => vf.PropertyName)
                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
            return BadRequest(errors);
        }

        var newEvent = await _categoryService.CreateAsync(item);
        return Ok(newEvent);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        /*var result = _deleteEntityValidator.Validate(id);
        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(vf => vf.PropertyName)
                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
            return BadRequest(errors);
        }*/
        _categoryService.Delete(id);
        return Ok();
    }
}