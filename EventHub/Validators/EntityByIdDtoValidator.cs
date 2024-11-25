using BusinessLayer.DtoModels;
using FluentValidation;

namespace EventHub.Validators;

public class EntityByIdDtoValidator : AbstractValidator<EntityByIdDto>
{
    public EntityByIdDtoValidator()
    {
        RuleFor(d => d.Id)
            .NotEmpty().WithMessage(d => EmptyParamMessage(nameof(d.Id)))
            .Must(CheckGuid).WithMessage("Invalid ID format");
    }
    private string EmptyParamMessage(string paramName)
    {
        return $"{paramName} can't be empty.";
    }
    
    private bool CheckGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
}