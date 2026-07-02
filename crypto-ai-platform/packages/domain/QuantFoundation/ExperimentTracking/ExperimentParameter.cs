using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

public class ExperimentParameter : BaseEntity<Guid>
{
    public Guid ExperimentId { get; private set; }
    public string Key { get; private set; } = string.Empty;
    public string Value { get; private set; } = string.Empty;

    private ExperimentParameter() { }

    public static ExperimentParameter Create(
        Guid id,
        TenantId tenantId,
        Guid experimentId,
        string key,
        string value,
        Guid? createdBy = null)
    {
        return new ExperimentParameter
        {
            Id = id,
            TenantId = tenantId,
            ExperimentId = experimentId,
            Key = key,
            Value = value,
            CreatedBy = createdBy
        };
    }
}