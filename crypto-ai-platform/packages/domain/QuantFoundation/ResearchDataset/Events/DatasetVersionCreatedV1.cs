using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Events;

public class DatasetVersionCreatedV1 : DomainEvent
{
    public Guid DatasetId { get; init; }
    public Guid VersionId { get; init; }
    public string Version { get; init; } = string.Empty;

    public DatasetVersionCreatedV1(TenantId tenantId, Guid datasetId, Guid versionId, string version)
    {
        TenantId = tenantId;
        DatasetId = datasetId;
        VersionId = versionId;
        Version = version;
    }
}
