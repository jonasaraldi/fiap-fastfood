namespace FastFood.Contracts.Abstractions.Exceptions;

public class NotFoundDomainException : DomainException
{
    public NotFoundDomainException(string message) 
        : base(message)
    {
    }
}