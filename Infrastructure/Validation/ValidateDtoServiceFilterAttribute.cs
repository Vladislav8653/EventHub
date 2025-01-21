using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Validation
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ValidateDtoServiceFilterAttribute<T> : Attribute, IAsyncActionFilter where T : class
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.RequestServices.GetService(typeof(IValidator<T>)) is not IValidator<T> validator)
            {
                context.Result = new BadRequestResult();
                return;
            }

            if (context.ActionArguments.Values.FirstOrDefault() is not T model)
            {
                context.Result = new BadRequestResult();
                return;
            }

            var result = await validator.ValidateAsync(model);

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
}