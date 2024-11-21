using BusinessLayer.DtoModels;
using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace EventHub.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    private ICategoryService _categoryService;
    private IValidator<CreateCategoryDto> _createCategotyValidator;
    private IValidator<EntityByIdDto> _deleteEntityValidator;
    public CategoryController(ICategoryService categoryService, IValidator<CreateCategoryDto> createCategotyValidator, IValidator<EntityByIdDto> deleteEntityValidator)
    {
        _categoryService = categoryService;
        _createCategotyValidator = createCategotyValidator;
        _deleteEntityValidator = deleteEntityValidator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto? item)
    {
        if (item == null)
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

    [HttpDelete]
    public IActionResult DeleteCategory([FromBody] EntityByIdDto id)
    {
        var result = _deleteEntityValidator.Validate(id);
        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(vf => vf.PropertyName)
                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
            return BadRequest(errors);
        }
        _categoryService.Delete(id);
        return Ok();
    }
}