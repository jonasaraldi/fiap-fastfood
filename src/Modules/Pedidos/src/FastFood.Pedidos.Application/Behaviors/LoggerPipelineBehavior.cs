using FastFood.Contracts.Abstractions.Exceptions;
using FastFood.Pedidos.Application.Abstractions;
using MediatR;

namespace FastFood.Pedidos.Application.Behaviors;

public class LoggerPipelineBehavior <TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{
    private readonly ILogger _logger;

    public LoggerPipelineBehavior(ILogger logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(
        TCommand request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        _logger.Information("{CommandName} Iniciado {@Command}", typeof(TCommand).Name, request);

        try
        {
            var response = await next();
            _logger.Information("{CommandName} Finalizado", typeof(TCommand).Name);
            return response;
        }
        catch (ValidationErrorException exception)
        {
            _logger.Information("{CommandName} {ExceptionMessage}", typeof(TCommand).Name, exception.Message);
            throw;
        }
        catch (Exception exception)
        {
            _logger.Error(exception, "{CommandName} com erro ({ExceptionMessage})", typeof(TCommand).Name, exception.Message);
            throw;
        }
    }
}