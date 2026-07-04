using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class DatasetMetadata : BaseEntity<Guid>
{
    public Guid DatasetVersionId { get; private set; }
    public string MetadataJson { get; private set; } = string.Empty;

    private DatasetMetadata() { }

    public static DatasetMetadata Create(
        Guid id,
        TenantId tenantId,
        Guid datasetVersionId,
        string metadataJson,
        Guid? createdBy = null)
    {
        return new DatasetMetadata
        {
            Id = id,
            TenantId = tenantId,
            DatasetVersionId = datasetVersionId,
            MetadataJson = metadataJson,
            CreatedBy = createdBy
        };
    }
}
