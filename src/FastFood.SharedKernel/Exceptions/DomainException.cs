namespace FastFood.SharedKernel.Exceptions;

public class DomainException : Exception
{
    protected DomainException(string message)
        : base(message)
    {
    }

    public static void Throw(string message)
    {
        throw new DomainException(message);
    }

    public static void ThrowIf(bool condition, string message)
    {
        if(!condition) return;

        Throw(message);
    }
}