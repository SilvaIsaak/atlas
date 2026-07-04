using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Events;

public class DatasetPublishedV1 : DomainEvent
{
    public Guid DatasetId { get; init; }
    public Guid VersionId { get; init; }

    public DatasetPublishedV1(TenantId tenantId, Guid datasetId, Guid versionId)
    {
        TenantId = tenantId;
        DatasetId = datasetId;
        VersionId = versionId;
    }
}
