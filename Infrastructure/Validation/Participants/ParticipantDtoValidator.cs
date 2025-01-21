using Application.DtoModels.ParticipantDto;
using FluentValidation;

namespace Infrastructure.Validation.Participants;

public class ParticipantDtoValidator : AbstractValidator<CreateParticipantDto>
{
    private const int MaxNameLength = 100;
    private const int MaxSurnameLength = 100;
 
    
    public ParticipantDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(nameof(c.Name)))
            .MaximumLength(MaxNameLength)
            .WithMessage(c => TooLongParamMessage(nameof(c.Name), MaxNameLength));
        
        RuleFor(c => c.Surname)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(nameof(c.Name)))
            .MaximumLength(MaxNameLength)
            .WithMessage(c => TooLongParamMessage(nameof(c.Name), MaxSurnameLength));
        
        RuleFor(c => c.DateOfBirth)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(nameof(c.DateOfBirth)))
            .Must(CheckDateOfBirth)
            .WithMessage("Invalid date of birth format. Scheme: dd-mm-yyyy");
        
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(nameof(c.Email)))
            .EmailAddress()
            .WithMessage("Invalid email address.");
        
    }


    

    private bool CheckDateOfBirth(string date)
    {
        return DateOnly.TryParse(date, out _);
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

