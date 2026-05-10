
using InkShelf.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InkShelf.Api.Middlewares
{
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
                var traceId = Guid.NewGuid();
                logger.LogError($"{DateTime.Now} | Error occure while processing the request, TraceId : ${traceId}, Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");

                var httpError = GetHttpErrorFromException(ex, traceId);

                context.Response.StatusCode = httpError.Item1;

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = httpError.Item2,
                    Status = httpError.Item1,
                    Instance = context.Request.Path,
                    Detail = httpError.Item3,
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        public Tuple<int, string, string> GetHttpErrorFromException(Exception exception, Guid traceId)
        {
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(ConflictException))
            {
                return Tuple.Create(StatusCodes.Status409Conflict, "Conflict", exception.Message);
            }
            if (exceptionType == typeof(WrongCredentialsException))
            {
                return Tuple.Create(StatusCodes.Status401Unauthorized, "Unauthorized", exception.Message);
            }
            if (exceptionType == typeof(ConflictException))
            {
                return Tuple.Create(StatusCodes.Status404NotFound, "Not Found", exception.Message);
            }
            if (exception is EntityValidationException validationException)
            {
                return Tuple.Create(StatusCodes.Status400BadRequest, "Bad request", JsonSerializer.Serialize(validationException.Errors));
            }
            return Tuple.Create(StatusCodes.Status500InternalServerError, "Internal Server Error", environment.IsDevelopment() ? exception.Message : $"Internal server error occured, traceId : {traceId}");
        }
    }
}
