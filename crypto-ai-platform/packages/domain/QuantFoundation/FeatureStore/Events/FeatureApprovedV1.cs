using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Events;

public class FeatureApprovedV1 : DomainEvent
{
    public Guid FeatureId { get; init; }
    public Guid ApprovedBy { get; init; }

    public FeatureApprovedV1(TenantId tenantId, Guid featureId, Guid approvedBy)
    {
        TenantId = tenantId;
        FeatureId = featureId;
        ApprovedBy = approvedBy;
    }
}