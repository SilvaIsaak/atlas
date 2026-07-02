using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

public class FeatureVersion : BaseEntity<Guid>
{
    public Guid FeatureId { get; private set; }
    public string Version { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;

    private FeatureVersion() { }

    public static FeatureVersion Create(
        Guid id,
        TenantId tenantId,
        Guid featureId,
        string version,
        string code,
        Guid? createdBy = null)
    {
        return new FeatureVersion
        {
            Id = id,
            TenantId = tenantId,
            FeatureId = featureId,
            Version = version,
            Code = code,
            CreatedBy = createdBy
        };
    }
}