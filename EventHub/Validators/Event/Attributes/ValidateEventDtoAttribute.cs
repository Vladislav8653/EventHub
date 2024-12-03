using BusinessLayer.DtoModels.EventsDto;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventHub.Validators.Event.Attributes;

public class ValidateEventDtoAttribute : IAsyncActionFilter
{
    private readonly IValidator<CreateEventDto> _validator;

    public ValidateEventDtoAttribute(IValidator<CreateEventDto> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        
        if (context.ActionArguments.FirstOrDefault().Value is not CreateEventDto model)
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