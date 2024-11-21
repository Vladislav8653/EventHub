namespace BusinessLayer.Infrastructure.Validators;

public class AbstractValidator
{
    protected string EmptyParamMessage(string paramName)
    {
        return $"{paramName} can't be empty.";
    }

    protected string TooLongParamMessage(string paramName, int length)
    {
        return $"{paramName} can't be longer than {length} symbols.";
    }
}