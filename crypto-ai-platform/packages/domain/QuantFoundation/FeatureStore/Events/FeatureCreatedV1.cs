using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Events;

public class FeatureCreatedV1 : DomainEvent
{
    public Guid FeatureId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid OwnerId { get; init; }

    public FeatureCreatedV1(TenantId tenantId, Guid featureId, string name, Guid ownerId)
    {
        TenantId = tenantId;
        FeatureId = featureId;
        Name = name;
        OwnerId = ownerId;
    }
}