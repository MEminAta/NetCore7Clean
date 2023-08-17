using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.PipelineBehaviors;

public class ExceptionHandlerMiddleware
{
    public static string LocalizationKey => "LocalizationKey";
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
                ValidationException => HttpStatusCode.BadRequest,
                KeyNotFoundException or FileNotFoundException => HttpStatusCode.NotFound,
                BusinessException => HttpStatusCode.OK,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                ArgumentException or InvalidOperationException => HttpStatusCode.BadRequest,
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