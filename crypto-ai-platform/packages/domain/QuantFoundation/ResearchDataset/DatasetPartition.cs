using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class DatasetPartition : BaseEntity<Guid>
{
    public Guid DatasetVersionId { get; private set; }
    public string PartitionKey { get; private set; } = string.Empty;
    public DatasetLocation Location { get; private set; } = null!;
    public DatasetHash Hash { get; private set; } = null!;
    public DatasetSize Size { get; private set; } = null!;

    private DatasetPartition() { }

    public static DatasetPartition Create(
        Guid id,
        TenantId tenantId,
        Guid datasetVersionId,
        string partitionKey,
        DatasetLocation location,
        DatasetHash hash,
        DatasetSize size,
        Guid? createdBy = null)
    {
        return new DatasetPartition
        {
            Id = id,
            TenantId = tenantId,
            DatasetVersionId = datasetVersionId,
            PartitionKey = partitionKey,
            Location = location,
            Hash = hash,
            Size = size,
            CreatedBy = createdBy
        };
    }
}
