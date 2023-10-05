namespace FastFood.WebApi.Models;

public record BaseResponse(
    int StatusCode, string Message);

public record BaseResponse<TData>(
    int StatusCode, string Message, TData? Data = null)
    where TData : class;