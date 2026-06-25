using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Monitoring;

public class SystemMetric : BaseEntity<Guid>, IAggregateRoot
{
    public MetricType Type { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string? Source { get; set; }
    public DateTime Timestamp { get; set; }
}
