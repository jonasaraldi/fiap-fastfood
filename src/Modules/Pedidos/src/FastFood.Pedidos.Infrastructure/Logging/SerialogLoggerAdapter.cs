using Serilog;
using ILogger = FastFood.Pedidos.Application.Abstractions.ILogger;

namespace FastFood.Pedidos.Infrastructure.Logging;

public class SerialogLoggerAdapter : ILogger
{
    public SerialogLoggerAdapter()
    {
    }

    public void Debug(string message) => Log.Debug(message);
    public void Debug<T0>(string message, T0 propertyValue0) => Log.Debug(message, propertyValue0);
    public void Debug<T0,T1>(string message, T0 propertyValue0, T1 propertyValue1) => Log.Debug(message, propertyValue0, propertyValue1);
    public void Debug<T0,T1, T2>(string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => Log.Debug(message, propertyValue0, propertyValue1, propertyValue2);
    public void Debug(string message, params object?[]? propertyValues) => Log.Debug(message, propertyValues);
    
    public void Information(string message) => Log.Information(message);
    public void Information<T0>(string message, T0 propertyValue0) => Log.Information(message, propertyValue0);
    public void Information<T0,T1>(string message, T0 propertyValue0, T1 propertyValue1) => Log.Information(message, propertyValue0, propertyValue1);
    public void Information<T0,T1, T2>(string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => Log.Information(message, propertyValue0, propertyValue1, propertyValue2);
    public void Information(string message, params object?[]? propertyValues) => Log.Information(message, propertyValues);

    public void Error(string message) => Log.Error(message);
    public void Error<T0>(string message, T0 propertyValue0) => Log.Error(message, propertyValue0);
    public void Error<T0,T1>(string message, T0 propertyValue0, T1 propertyValue1) => Log.Error(message, propertyValue0, propertyValue1);
    public void Error<T0,T1, T2>(string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => Log.Error(message, propertyValue0, propertyValue1, propertyValue2);
    public void Error(string message, params object?[]? propertyValues) => Log.Error(message, propertyValues);
    
    public void Error(Exception? exception, string message) => Log.Error(exception, message);
    public void Error<T0>(Exception? exception, string message, T0 propertyValue0) => Log.Error(exception, message, propertyValue0);
    public void Error<T0, T1>(Exception? exception, string message, T0 propertyValue0, T1 propertyValue1) => 
        Log.Error(exception, message, propertyValue0, propertyValue1);
    public void Error<T0, T1, T2>(Exception? exception, string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => 
        Log.Error(exception, message, propertyValue0, propertyValue1, propertyValue2);
}