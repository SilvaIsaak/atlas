using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Events;

public class FeatureVersionedV1 : DomainEvent
{
    public Guid FeatureId { get; init; }
    public string FeatureVersion { get; init; } = string.Empty;

    public FeatureVersionedV1(TenantId tenantId, Guid featureId, string featureVersion)
    {
        TenantId = tenantId;
        FeatureId = featureId;
        FeatureVersion = featureVersion;
    }
}