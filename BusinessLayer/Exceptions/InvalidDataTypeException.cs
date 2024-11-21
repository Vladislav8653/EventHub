namespace BusinessLayer.Exceptions;

public class InvalidDataTypeException : Exception
{
    public InvalidDataTypeException(string message) : base(message) { }
}