using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.EventsDto;
using BusinessLayer.DtoModels.EventsDto.QueryParams;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventHub.Validation.Event.Attributes;

public class ValidateEventQueryParamsAttribute : IAsyncActionFilter
{
    private readonly IValidator<EventFiltersDto> _filtersValidator;
    private readonly IValidator<PageParamsDto> _pageParamsValidator;

    public ValidateEventQueryParamsAttribute(IValidator<EventFiltersDto> filtersValidator, IValidator<PageParamsDto> pageParamsValidator)
    {
        _filtersValidator = filtersValidator;
        _pageParamsValidator = pageParamsValidator;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        
        if (context.ActionArguments.FirstOrDefault().Value is not EventQueryParamsDto model)
        {
            context.Result = new BadRequestResult(); 
            return;
        }

        if (model.PageParams != null)
        {
            var result = await _pageParamsValidator.ValidateAsync(model.PageParams);
            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(vf => vf.PropertyName)
                    .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                context.Result = new BadRequestObjectResult(errors);
                return;
            }
        }

        if (model.Filters != null)
        {
            var result = await _filtersValidator.ValidateAsync(model.Filters);
            if (!result.IsValid)
            {
                var errors = result.Errors
                    .GroupBy(vf => vf.PropertyName)
                    .ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                context.Result = new BadRequestObjectResult(errors);
                return;
            }
        }
        
        await next();
    }
}