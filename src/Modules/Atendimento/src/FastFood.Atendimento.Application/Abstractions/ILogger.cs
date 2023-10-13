namespace FastFood.Atendimento.Application.Abstractions;

public interface ILogger
{
    void Debug(string message);
    void Debug<T0>(string message, T0 propertyValue0);
    void Debug<T0, T1>(string message, T0 propertyValue0, T1 propertyValue1);
    void Debug<T0, T1, T2>(string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Debug(string message, params object?[]? propertyValues);
    
    void Information(string message);
    void Information<T0>(string message, T0 propertyValue0);
    void Information<T0, T1>(string message, T0 propertyValue0, T1 propertyValue1);
    void Information<T0, T1, T2>(string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Information(string message, params object?[]? propertyValues);
    
    void Error(string message);
    void Error<T0>(string message, T0 propertyValue0);
    void Error<T0, T1>(string message, T0 propertyValue0, T1 propertyValue1);
    void Error<T0, T1, T2>(string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
    void Error(string message, params object?[]? propertyValues);
    void Error(Exception? exception, string message);
    void Error<T0>(Exception? exception, string message, T0 propertyValue0);
    void Error<T0, T1>(Exception? exception, string message, T0 propertyValue0, T1 propertyValue1);
    void Error<T0, T1, T2>(Exception? exception, string message, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);
}