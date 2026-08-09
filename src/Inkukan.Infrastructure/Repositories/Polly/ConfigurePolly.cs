using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Inkukan.Infrastructure.Repositories.Polly;

public static class ConfigurePolly
{
    public static IHttpClientBuilder AddPolly(this IHttpClientBuilder builder)
    {
        builder
            .AddPolicyHandler((services, request) =>
            {
                ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
                ILogger logger = loggerFactory.CreateLogger("PollyHttpClient");

                return Policy.WrapAsync(GetRetryPolicy(logger), 
                    GetCircuitBreakerPolicy(logger),
                    GetTimeoutPolicy(logger));
            });

        return builder;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(ILogger logger)
    {
        return Policy.TimeoutAsync<HttpResponseMessage>(
            TimeSpan.FromSeconds(30),
            (context, timeout, task) =>
            {
                logger.LogWarning("Request timed out after {Timeout}s", timeout.TotalSeconds);
                return Task.CompletedTask;
            });
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(ILogger logger)
    {

        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                
                sleepDurationProvider: (attempt, outcome, context) =>
                {
                    // Exponential backoff: 1s, 2s, 4s, 8s, 16s
                    TimeSpan baseDelay = TimeSpan.FromSeconds(Math.Pow(2, attempt - 1));

                    // Add jitter: +/- 20% of the base delay
                    double jitterMs = Random.Shared.Next(0, (int)(baseDelay.TotalMilliseconds * 0.4))
                                  - (baseDelay.TotalMilliseconds * 0.2);

                    return baseDelay + TimeSpan.FromMilliseconds(jitterMs);
                },
                onRetryAsync: async (outcome, timespan, attempt, context) =>
                {
                    // Log the retry attempt
                    logger.LogWarning("Retry {attempt}: waiting {duration}s", attempt, $"{timespan.TotalSeconds:F2}");
                    await Task.CompletedTask;
                });
    }

    static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ILogger logger)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 3,  // Open after 5 failures
                durationOfBreak: TimeSpan.FromSeconds(30),  // Stay open for 30s
                onBreak: (outcome, duration) =>
                {
                    logger.LogWarning("Circuit breaker opened for {duration}s", duration.TotalSeconds);
                },
                onReset: () =>
                {
                    logger.LogWarning("Circuit breaker reset");
                },
                onHalfOpen: () =>
                {
                    logger.LogWarning("Circuit breaker half-open, testing...");
                });
    }
}
