using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Events;

public class DatasetCreatedV1 : DomainEvent
{
    public Guid DatasetId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid OwnerId { get; init; }

    public DatasetCreatedV1(TenantId tenantId, Guid datasetId, string name, Guid ownerId)
    {
        TenantId = tenantId;
        DatasetId = datasetId;
        Name = name;
        OwnerId = ownerId;
    }
}