using Inkukan.Application.Services;

namespace Inkukan.Api.Middlewares;

public class TraceIdMiddleware(RequestDelegate next)
{
    private const string TraceIdHeader = "X-Trace-Id";

    public async Task InvokeAsync(HttpContext context, ITraceIdAccessor accessor)
    {
        Guid traceId = GetOrCreateCorrelationId(context);

        accessor.TraceId = traceId;

        context.Response.OnStarting(() =>
        {
            context.Response.Headers.TryAdd(TraceIdHeader, accessor.ToString());
            return Task.CompletedTask;
        });

        context.Items[TraceIdHeader] = traceId;

        await next(context);
    }

    private static Guid GetOrCreateCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(TraceIdHeader, out var existingId)
            && !string.IsNullOrWhiteSpace(existingId) && Guid.TryParse(existingId, out Guid traceId))
        {
            return traceId;
        }

        return Guid.NewGuid();
    }
}
