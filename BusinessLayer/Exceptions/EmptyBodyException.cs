namespace BusinessLayer.Exceptions;

public class EmptyBodyException : Exception
{
    public EmptyBodyException(string message) : base(message){ }

   // public EmptyBodyException(string entityName, string falseValue) 
     //   : base($"{entityName} with name {falseValue} already exists.") { }
}