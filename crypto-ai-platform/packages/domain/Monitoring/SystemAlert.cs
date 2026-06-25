using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Monitoring;

public class SystemAlert : BaseEntity<Guid>, IAggregateRoot
{
    public MetricType RelatedMetricType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Severity { get; set; } = string.Empty;
    public bool Acknowledged { get; set; } = false;
    public DateTime? AcknowledgedAt { get; set; }
}
