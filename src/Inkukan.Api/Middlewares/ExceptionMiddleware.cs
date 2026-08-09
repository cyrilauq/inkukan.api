using Inkukan.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Inkukan.Api.Middlewares;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment environment) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            string traceId = System.Diagnostics.Activity.Current?.Id ?? context.TraceIdentifier;
            logger.LogError(
                "Error occured while processing the request, TraceId: {traceId}, Message : {message},\nStackTrace: {stackTrace}\nException data: {data}", 
                traceId,
                ex.Message, 
                ex.StackTrace, 
                JsonSerializer.Serialize(ex.Data));
            
            Tuple<int, string, string> httpError = GetHttpErrorFromException(ex);

            context.Response.StatusCode = httpError.Item1;

            ProblemDetails problemDetails = new()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = httpError.Item2,
                Status = httpError.Item1,
                Instance = context.Request.Path,
                Detail = httpError.Item3,
            };
            problemDetails.Extensions["traceId"] = traceId;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    public Tuple<int, string, string> GetHttpErrorFromException(Exception exception)
    {
        Type exceptionType = exception.GetType();
        if (exceptionType == typeof(ConflictException))
        {
            return Tuple.Create(StatusCodes.Status409Conflict, "Conflict", exception.Message);
        }
        if (exceptionType == typeof(WrongCredentialsException))
        {
            return Tuple.Create(StatusCodes.Status401Unauthorized, "Unauthorized", exception.Message);
        }
        if (exceptionType == typeof(EntityNotFoundException))
        {
            return Tuple.Create(StatusCodes.Status404NotFound, "Not Found", exception.Message);
        }
        if (exception is EntityValidationException validationException)
        {
            return Tuple.Create(StatusCodes.Status400BadRequest, "Bad request", JsonSerializer.Serialize(validationException.Errors));
        }
        return Tuple.Create(StatusCodes.Status500InternalServerError, "Internal Server Error", environment.IsDevelopment() ? exception.Message : $"Internal server error occured");
    }
}
