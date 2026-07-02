using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Events;

public class FeatureVersionedV1 : DomainEvent
{
    public Guid FeatureId { get; init; }
    public string Version { get; init; } = string.Empty;

    public FeatureVersionedV1(TenantId tenantId, Guid featureId, string version)
    {
        TenantId = tenantId;
        FeatureId = featureId;
        Version = version;
    }
}