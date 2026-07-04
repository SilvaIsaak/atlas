using Polly;
using Polly.CircuitBreaker;
using Polly.RateLimit;
using Polly.Retry;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure.Resilience;

public class ExchangeResiliencePolicies
{
    public AsyncRetryPolicy RetryPolicy { get; }
    public AsyncCircuitBreakerPolicy CircuitBreakerPolicy { get; }
    public AsyncTimeoutPolicy TimeoutPolicy { get; }
    public AsyncRateLimitPolicy RateLimitPolicy { get; }

    public ExchangeResiliencePolicies(
        IOptions<PollyOptions> pollyOptions,
        ILogger<ExchangeResiliencePolicies> logger)
    {
        var options = pollyOptions.Value;

        RetryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: options.RetryCount,
                sleepDurationProvider: retryAttempt => 
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + 
                    TimeSpan.FromMilliseconds(new Random().Next(0, 100)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    logger.LogWarning(
                        exception,
                        "Retrying request after {TimeSpan} (Retry {RetryCount}/{MaxRetryCount})",
                        timeSpan, retryCount, options.RetryCount);
                });

        CircuitBreakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: options.CircuitBreakerExceptions,
                durationOfBreak: options.CircuitBreakerDuration,
                onBreak: (exception, duration) =>
                {
                    logger.LogError(
                        exception,
                        "Circuit breaker opened for {Duration}", duration);
                },
                onReset: () =>
                {
                    logger.LogInformation("Circuit breaker reset");
                });

        TimeoutPolicy = Policy.TimeoutAsync(
            timeout: options.TimeoutDuration,
            timeoutStrategy: Polly.Timeout.TimeoutStrategy.Optimistic);

        RateLimitPolicy = Policy
            .RateLimitAsync(
                numberOfExecutions: 100,
                perTimeSpan: TimeSpan.FromMinutes(1));
    }
}
