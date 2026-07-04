using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Events;

public class MetricRecordedV1 : DomainEvent
{
    public Guid ExperimentRunId { get; init; }
    public string Key { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal Value { get; init; }

    public MetricRecordedV1(TenantId tenantId, Guid experimentRunId, string key, string name, decimal value)
    {
        TenantId = tenantId;
        ExperimentRunId = experimentRunId;
        Key = key;
        Name = name;
        Value = value;
    }
}
