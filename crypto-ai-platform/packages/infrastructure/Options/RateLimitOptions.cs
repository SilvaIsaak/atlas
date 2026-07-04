namespace CryptoAIPlatform.Infrastructure.Options;

public class RateLimitOptions
{
    public const string SectionName = "RateLimit";
    public int RequestLimitPerSecond { get; set; } = 10;
    public int RequestLimitPerMinute { get; set; } = 1200;
    public int OrderLimitPerSecond { get; set; } = 10;
    public int OrderLimitPerDay { get; set; } = 200000;
}
