namespace CryptoAIPlatform.Infrastructure.Options;

public class PollyOptions
{
    public const string SectionName = "Polly";
    public int RetryCount { get; set; } = 3;
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(1);
    public int CircuitBreakerExceptions { get; set; } = 5;
    public TimeSpan CircuitBreakerDuration { get; set; } = TimeSpan.FromMinutes(1);
    public TimeSpan TimeoutDuration { get; set; } = TimeSpan.FromSeconds(30);
}
