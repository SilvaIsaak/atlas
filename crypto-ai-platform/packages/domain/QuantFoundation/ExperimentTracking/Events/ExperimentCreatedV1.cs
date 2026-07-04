using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Events;

public class ExperimentCreatedV1 : DomainEvent
{
    public Guid ExperimentId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid OwnerId { get; init; }

    public ExperimentCreatedV1(TenantId tenantId, Guid experimentId, string name, Guid ownerId)
    {
        TenantId = tenantId;
        ExperimentId = experimentId;
        Name = name;
        OwnerId = ownerId;
    }
}
