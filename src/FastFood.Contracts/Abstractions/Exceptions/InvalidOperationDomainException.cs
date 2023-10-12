namespace FastFood.Contracts.Abstractions.Exceptions;

public class InvalidOperationDomainException : DomainException
{
    public InvalidOperationDomainException(string message) 
        : base(message)
    {
    }
}