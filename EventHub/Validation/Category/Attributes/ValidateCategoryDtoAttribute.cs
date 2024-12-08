using BusinessLayer.DtoModels.CategoryDto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventHub.Validation.Category.Attributes;

public class ValidateCategoryDtoAttribute : IAsyncActionFilter
{
    private readonly IValidator<CategoryDto> _validator;

    public ValidateCategoryDtoAttribute(IValidator<CategoryDto> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        
        if (context.ActionArguments.FirstOrDefault().Value is not CategoryDto model)
        {
            context.Result = new BadRequestResult(); 
            return;
        }
        
        var result = await _validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(vf => vf.PropertyName)
                .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
            context.Result = new BadRequestObjectResult(errors);
        }
        else
        {
            await next();
        }
    }
}