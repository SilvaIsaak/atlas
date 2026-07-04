namespace CryptoAIPlatform.Infrastructure.Options;

public class ReconnectOptions
{
    public const string SectionName = "Reconnect";
    public int MaxReconnectAttempts { get; set; } = 10;
    public int InitialDelayMs { get; set; } = 1000;
    public int MaxDelayMs { get; set; } = 30000;
    public int HeartbeatIntervalMs { get; set; } = 30000;
    public int HeartbeatTimeoutMs { get; set; } = 10000;
}
