using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class DatasetSnapshot : BaseEntity<Guid>
{
    public Guid DatasetVersionId { get; private set; }
    public DateTime SnapshotDate { get; private set; }
    public DatasetLocation Location { get; private set; } = null!;
    public DatasetHash Hash { get; private set; } = null!;

    private DatasetSnapshot() { }

    public static DatasetSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid datasetVersionId,
        DateTime snapshotDate,
        DatasetLocation location,
        DatasetHash hash,
        Guid? createdBy = null)
    {
        return new DatasetSnapshot
        {
            Id = id,
            TenantId = tenantId,
            DatasetVersionId = datasetVersionId,
            SnapshotDate = snapshotDate,
            Location = location,
            Hash = hash,
            CreatedBy = createdBy
        };
    }
}
