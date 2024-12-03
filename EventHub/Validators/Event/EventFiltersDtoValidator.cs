using BusinessLayer.DtoModels.EventsDto;
using FluentValidation;

namespace EventHub.Validators.Event;

public class EventFiltersDtoValidator : AbstractValidator<EventFiltersDto>
{
    public EventFiltersDtoValidator()
    {
        RuleFor(e => e.Date)
            .Must(CheckDateTime!)
            .When(e => e.Date != null)
            .WithMessage("Invalid date format.");
        RuleFor(e => e.StartDate)
            .Must(CheckDateTime!)
            .When(e => e.StartDate != null)
            .WithMessage("Invalid date format.");
        RuleFor(e => e.FinishDate)
            .Must(CheckDateTime!)
            .When(e => e.FinishDate != null)
            .WithMessage("Invalid date format.");
        
    }
    private bool CheckDateTime(string date)
    {
        return DateTime.TryParse(date, out _);
    }
}