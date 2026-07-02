using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

public class ExperimentMetric : BaseEntity<Guid>
{
    public Guid ExperimentRunId { get; private set; }
    public string Key { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public decimal Value { get; private set; }

    private ExperimentMetric() { }

    public static ExperimentMetric Create(
        Guid id,
        TenantId tenantId,
        Guid experimentRunId,
        string key,
        string name,
        decimal value,
        Guid? createdBy = null)
    {
        return new ExperimentMetric
        {
            Id = id,
            TenantId = tenantId,
            ExperimentRunId = experimentRunId,
            Key = key,
            Name = name,
            Value = value,
            CreatedBy = createdBy
        };
    }
}