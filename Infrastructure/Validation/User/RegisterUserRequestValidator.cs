using Application.DtoModels.UserDto;
using FluentValidation;

namespace Application.Validation.User.Validators;

public class RegisterUserRequestValidator: AbstractValidator<RegisterUserRequest>
{
    private const int MaxLoginLength = 50;
    private const int MaxPasswordLength = 60;
    private const int MaxUsernameLength = 50;
    public RegisterUserRequestValidator()
    {
        RuleFor(r => r.Login)
            .NotEmpty()
            .WithMessage(r => EmptyParamMessage(nameof(r.Login)))
            .MaximumLength(MaxLoginLength)
            .WithMessage(r => TooLongParamMessage(nameof(r.Login), MaxLoginLength));
        RuleFor(r => r.UserName)
            .NotEmpty()
            .WithMessage(r => EmptyParamMessage(nameof(r.UserName)))
            .MaximumLength(MaxLoginLength)
            .WithMessage(r => TooLongParamMessage(nameof(r.UserName), MaxUsernameLength));
        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage(r => EmptyParamMessage(nameof(r.Password)))
            .MaximumLength(MaxPasswordLength)
            .WithMessage(r => TooLongParamMessage(nameof(r.Password), MaxPasswordLength));

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