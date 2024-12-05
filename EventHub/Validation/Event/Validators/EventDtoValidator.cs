using BusinessLayer.DtoModels.EventsDto;
using FluentValidation;

namespace EventHub.Validation.Event.Validators;

public class EventDtoValidator : AbstractValidator<CreateEventDto>
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 1000;
    private const int MaxPlaceLength = 100;
    private const int MaxImageLength = 100;
    
    public EventDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(nameof(c.Name)))
            .MaximumLength(MaxNameLength)
            .WithMessage(c => TooLongParamMessage(nameof(c.Name), MaxNameLength));
        
        RuleFor(c => c.Description)
            .MaximumLength(MaxDescriptionLength)
            .WithMessage(c => TooLongParamMessage(nameof(c.Description), MaxDescriptionLength));
        
        RuleFor(c => c.DateTime)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(nameof(c.DateTime)))
            .Must(CheckDateTime)
            .WithMessage("Invalid date format.");
        
        RuleFor(c => c.Place)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(nameof(c.Place)))
            .MaximumLength(MaxPlaceLength)
            .WithMessage(c => TooLongParamMessage(nameof(c.Place), MaxPlaceLength));
        
        RuleFor(c => c.Category)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(nameof(c.Category)));

        RuleFor(c => c.Image)
            .Must(HaveValidImageExtension!)
            .When(c => c.Image != null)
            .WithMessage("Invalid image format.");
    }


    private bool HaveValidImageExtension(IFormFile file)
    {
        if (file.Length == 0)
            return false; 
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }

    private bool CheckDateTime(string date)
    {
        return DateTime.TryParse(date, out _);
    }

    private string EmptyParamMessage(string paramName)
    {
        return $"{paramName} can't be empty.";
    }

    private string TooLongParamMessage(string paramName, int length)
    {
        return $"{paramName} can't be longer than {length} symbols.";
    }
}

