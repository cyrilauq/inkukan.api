using Inkukan.Application.Services;

namespace Inkukan.Api.Middlewares;

public class TraceIdMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeader = "X-Trace-Id";

    public async Task InvokeAsync(HttpContext context, ITraceIdAccessor accessor)
    {
        // Try to get correlation ID from incoming request
        var correlationId = GetOrCreateCorrelationId(context);

        // Store it for the duration of the request
        accessor.TraceId = correlationId;

        // Add to response headers so clients can see it
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.TryAdd(CorrelationIdHeader, System.Diagnostics.Activity.Current?.Id ?? context.TraceIdentifier);
            return Task.CompletedTask;
        });

        // Add to the HttpContext.Items for easy access
        context.Items[CorrelationIdHeader] = correlationId;

        await next(context);
    }

    private static Guid GetOrCreateCorrelationId(HttpContext context)
    {
        // Check if the request already has a correlation ID
        if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var existingId)
            && !string.IsNullOrWhiteSpace(existingId) && Guid.TryParse(existingId, out Guid traceId))
        {
            return traceId;
        }

        // Generate a new one if not present
        return Guid.NewGuid();
    }
}
