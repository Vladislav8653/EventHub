using BusinessLayer.DtoModels.UserDto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventHub.Validation.User.Attributes;

public class ValidateRegisterUserRequestAttribute : IAsyncActionFilter
{
    private readonly IValidator<RegisterUserRequest> _validator;

    public ValidateRegisterUserRequestAttribute(IValidator<RegisterUserRequest> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        if (context.ActionArguments.FirstOrDefault().Value is not RegisterUserRequest model)
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