namespace CryptoAIPlatform.Infrastructure.Options;

public class OpenTelemetryOptions
{
    public const string SectionName = "OpenTelemetry";
    public string ServiceName { get; set; } = "CryptoAIPlatform.Api";
    public string ServiceVersion { get; set; } = "1.0.0";
    public string? OtlpEndpoint { get; set; }
    public bool EnableTracing { get; set; } = true;
    public bool EnableMetrics { get; set; } = true;
}
