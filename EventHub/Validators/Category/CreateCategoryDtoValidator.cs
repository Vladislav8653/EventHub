using BusinessLayer.DtoModels.CategoryDto;
using FluentValidation;

namespace EventHub.Validators.Category;

public class CreateCategoryDtoValidator : AbstractValidator<CategoryDto>
{
    private const int MaxNameLength = 30;
    public CreateCategoryDtoValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(c => EmptyParamMessage(c.Name))
            .MaximumLength(MaxNameLength)
            .WithMessage(c => TooLongParamMessage(nameof(c.Name), MaxNameLength));
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