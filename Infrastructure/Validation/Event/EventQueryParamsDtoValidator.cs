using Application.DtoModels.EventsDto;
using FluentValidation;
using Infrastructure.Validation.CommonValidation;

namespace Infrastructure.Validation.Event;

public class EventQueryParamsDtoValidator : AbstractValidator<EventQueryParamsDto>
{
    public EventQueryParamsDtoValidator()
    {
        RuleFor(x => x.Filters)
            .SetValidator(new EventFiltersDtoValidator()!)
            .When(x => x.Filters != null);
        RuleFor(x => x.PageParams)
            .SetValidator(new PageParamsDtoValidator()!)
            .When(x => x.PageParams != null);
    }
}