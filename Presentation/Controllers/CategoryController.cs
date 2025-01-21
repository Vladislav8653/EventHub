using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Application.DtoModels.CategoryDto;
using Infrastructure.Validation;
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
        IDeleteCategoryUseCase deleteCategoryUseCase)
    {
        _createCategoryUseCase = createCategoryUseCase;
        _getAllCategoriesUseCase = getAllCategoriesUseCase;
        _deleteCategoryUseCase = deleteCategoryUseCase;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var result = await _getAllCategoriesUseCase.Handle(cancellationToken);
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ValidateDtoServiceFilter<CategoryDto>]
    public async Task<IActionResult> CreateCategory([FromBody]CategoryDto item, CancellationToken cancellationToken)
    {
        var newEvent = await _createCategoryUseCase.Handle(item, cancellationToken);
        return Ok(newEvent);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        _deleteCategoryUseCase.Handle(id, cancellationToken);
        return Ok();
    }
}