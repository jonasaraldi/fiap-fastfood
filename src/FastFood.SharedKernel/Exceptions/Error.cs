namespace FastFood.SharedKernel.Exceptions;

public static class Error
{
    public static void Throw<TDomainException>(string message)
        where TDomainException : DomainException
    {
        TDomainException? instance = (TDomainException?)Activator
            .CreateInstance(typeof(TDomainException), message);
        
        if(instance is null) return;
        
        throw instance;
    }
}