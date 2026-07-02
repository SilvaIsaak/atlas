using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.DataQuality.Events;

public class AnomalyResolvedV1 : DomainEvent
{
    public Guid AnomalyId { get; init; }
    public Guid ResolvedBy { get; init; }

    public AnomalyResolvedV1(TenantId tenantId, Guid anomalyId, Guid resolvedBy)
    {
        TenantId = tenantId;
        AnomalyId = anomalyId;
        ResolvedBy = resolvedBy;
    }
}