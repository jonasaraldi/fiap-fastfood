namespace FastFood.Contracts.Abstractions.Exceptions;

public sealed class ValidationErrorException : Exception
{
    private readonly IEnumerable<ValidationError> _validationErrors;

    public ValidationErrorException(IEnumerable<ValidationError> validationErrors) 
        : base("Ocorreram erros de validação")
    {
        _validationErrors = validationErrors;
    }

    public override string ToString()
    {
        string errors = string.Join(
            Environment.NewLine, 
            _validationErrors.Select(validationError => validationError.Error));

        return $"{Message}:{Environment.NewLine}{errors}";
    }
}

public record ValidationError(string Property, string Error);