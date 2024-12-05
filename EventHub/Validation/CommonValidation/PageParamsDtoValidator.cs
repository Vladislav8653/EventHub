using BusinessLayer.DtoModels.CommonDto;
using FluentValidation;
namespace EventHub.Validation.CommonValidation;

public class PageParamsDtoValidator : AbstractValidator<PageParamsDto>
{
    public PageParamsDtoValidator()
    {
        RuleFor(e => e.Page)
            .GreaterThan(0)
            .When(e => e.Page != null)
            .WithMessage("Page must be greater than 0.");
        RuleFor(e => e.PageSize)
            .GreaterThan(0)
            .When(e => e.PageSize != null)
            .WithMessage("PageSize must be greater than 0.");
    }

}