namespace Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message){ }

    public EntityNotFoundException(string entityName, string invalidProperty ,string invalidValue) 
        : base($"{entityName} with {invalidProperty} {invalidValue} not found.") { }
}