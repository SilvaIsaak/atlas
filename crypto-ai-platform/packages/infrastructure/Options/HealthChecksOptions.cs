namespace CryptoAIPlatform.Infrastructure.Options;

public class HealthChecksOptions
{
    public const string SectionName = "HealthChecks";
    public bool EnablePostgreSQL { get; set; } = true;
    public bool EnableRedis { get; set; } = true;
    public bool EnableRabbitMQ { get; set; } = true;
    public bool EnableStorage { get; set; } = true;
}
