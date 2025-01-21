using Application.DtoModels.CommonDto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Validation.CommonValidation;

public class ValidatePageParamsAttribute : IAsyncActionFilter
{
    private readonly IValidator<PageParamsDto> _validator;

    public ValidatePageParamsAttribute(IValidator<PageParamsDto> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        
        if (context.ActionArguments.FirstOrDefault().Value is not PageParamsDto model)
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