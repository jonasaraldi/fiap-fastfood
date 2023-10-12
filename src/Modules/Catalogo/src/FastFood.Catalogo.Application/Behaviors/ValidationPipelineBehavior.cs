using FastFood.Catalogo.Application.Abstractions;
using FastFood.Contracts.Abstractions.Exceptions;
using FluentValidation;
using MediatR;

namespace FastFood.Catalogo.Application.Behaviors;

public class ValidationPipelineBehavior<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public ValidationPipelineBehavior(
        IEnumerable<IValidator<TCommand>> validators)
    {
        _validators = validators;
    }
    
    public Task<TResponse> Handle(
        TCommand request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return next();

        ValidationError[] validationErrors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (validationErrors.Any())
            throw new ValidationErrorException(validationErrors);
        
        return next();
    }
}