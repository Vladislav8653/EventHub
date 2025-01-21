using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Validation
{
    public class ValidateDtoAttribute<T> : IAsyncActionFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidateDtoAttribute(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.Values.FirstOrDefault() is not T model)
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
}