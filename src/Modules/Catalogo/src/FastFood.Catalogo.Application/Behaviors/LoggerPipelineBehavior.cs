using FastFood.Catalogo.Application.Abstractions;
using MediatR;

namespace FastFood.Catalogo.Application.Behaviors;

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
        _logger.Information("[INICIO] {CommandName}: {@Command}", typeof(TCommand).Name, request);

        try
        {
            var response = await next();
            _logger.Information("[FIM] {CommandName}", typeof(TCommand).Name);
            return response;
        }
        catch (Exception exception)
        {
            _logger.Error(exception, "[ERRO] {CommandName}: {ExceptionMessage}", typeof(TCommand).Name, exception.Message);
            throw;
        }
    }
}