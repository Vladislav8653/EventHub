using Application.DtoModels.UserDto;
using FluentValidation;

namespace Application.Validation.User.Validators;

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    private const int MaxLoginLength = 50;
    private const int MaxPasswordLength = 60;

    public LoginUserRequestValidator()
    {
        RuleFor(l => l.Login)
            .NotEmpty()
            .WithMessage(l => EmptyParamMessage(nameof(l.Login)))
            .MaximumLength(MaxLoginLength)
            .WithMessage(l => TooLongParamMessage(nameof(l.Login), MaxLoginLength));
        RuleFor(l => l.Password)
            .NotEmpty()
            .WithMessage(l => EmptyParamMessage(nameof(l.Password)))
            .MaximumLength(MaxPasswordLength)
            .WithMessage(l => TooLongParamMessage(nameof(l.Password), MaxPasswordLength));

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