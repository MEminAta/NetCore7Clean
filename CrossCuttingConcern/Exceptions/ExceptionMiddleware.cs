using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using CrossCuttingConcern.Exceptions.ExceptionTypes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CrossCuttingConcern.Exceptions;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var status = exception switch
            {
                ValidationException or RuleException => HttpStatusCode.BadRequest,
                AuthenticationException => HttpStatusCode.Unauthorized,
                UnauthorizedAccessException => HttpStatusCode.Forbidden,
                _ => HttpStatusCode.InternalServerError
            };

            response.StatusCode = (int)status;
            await response.WriteAsync(JsonSerializer.Serialize(new
            {
                Type = exception.GetType().ToString(),
                exception.Message
            }));
        }
    }
}