using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using FastFood.SharedKernel.Exceptions;
using FastFood.WebApi.Models;

namespace FastFood.WebApi.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "Ocorreu um erro interno no servidor.";
        
        if (exception is DomainException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = exception.Message;
        }

        statusCode = exception switch
        {
            NotFoundDomainException => HttpStatusCode.NotFound,
            InvalidOperationDomainException => HttpStatusCode.BadRequest,
            _ => statusCode
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        BaseResponse response = new BaseResponse(
            context.Response.StatusCode, message);

        string json = JsonSerializer.Serialize(response);
        
        await context.Response.WriteAsync(json);
    }
}

public static class GlobalExceptionHandlerExtensions
{
    public static void UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
