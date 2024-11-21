namespace BusinessLayer.Exceptions;

public class EntityAlreadyExistException : Exception
{
    public EntityAlreadyExistException(string message) : base(message){ }
    
    public EntityAlreadyExistException(string entityName, string invalidProperty ,string invalidValue) 
        : base($"{entityName} with {invalidProperty} {invalidValue} already exists.") { }
}