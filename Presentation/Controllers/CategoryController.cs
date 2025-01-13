using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Application.DtoModels.CategoryDto;
using Application.Validation.Category.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController : ControllerBase
{
    private readonly ICreateCategoryUseCase _createCategoryUseCase;
    private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;
    private readonly IGetAllCategoriesUseCase _getAllCategoriesUseCase;
    public CategoryController(
        ICreateCategoryUseCase createCategoryUseCase, 
        IGetAllCategoriesUseCase getAllCategoriesUseCase, 
        IDeleteCategoryUseCase deleteCategoryUseCase
        )
    {
        _createCategoryUseCase = createCategoryUseCase;
        _getAllCategoriesUseCase = getAllCategoriesUseCase;
        _deleteCategoryUseCase = deleteCategoryUseCase;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _getAllCategoriesUseCase.Handle();
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ServiceFilter(typeof(ValidateCategoryDtoAttribute))]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryDto item)
    {
        var newEvent = await _createCategoryUseCase.Handle(item);
        return Ok(newEvent);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
        _deleteCategoryUseCase.Handle(id);
        return Ok();
    }
}