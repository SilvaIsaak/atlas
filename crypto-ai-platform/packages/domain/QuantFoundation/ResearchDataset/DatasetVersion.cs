using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Events;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class DatasetVersion : BaseEntity<Guid>
{
    private readonly List<DatasetTransformation> _transformations = [];
    private readonly List<DatasetSnapshot> _snapshots = [];
    private readonly List<DatasetPartition> _partitions = [];
    private readonly List<DatasetMetadata> _metadata = [];
    private readonly List<DatasetSchema> _schemas = [];

    public Guid DatasetId { get; private set; }
    public string Version { get; private set; } = string.Empty;
    public DateTime PeriodStart { get; private set; }
    public DateTime PeriodEnd { get; private set; }
    public IReadOnlyList<string> AssetSymbols { get; private set; } = [];
    public bool IsPublished { get; private set; }
    public bool IsArchived { get; private set; }
    public DatasetLocation? Location { get; private set; }
    public DatasetHash? Hash { get; private set; }
    public DatasetStatistics? Statistics { get; private set; }
    public DatasetChecksum? Checksum { get; private set; }

    public IReadOnlyCollection<DatasetTransformation> Transformations => _transformations.AsReadOnly();
    public IReadOnlyCollection<DatasetSnapshot> Snapshots => _snapshots.AsReadOnly();
    public IReadOnlyCollection<DatasetPartition> Partitions => _partitions.AsReadOnly();
    public IReadOnlyCollection<DatasetMetadata> Metadata => _metadata.AsReadOnly();
    public IReadOnlyCollection<DatasetSchema> Schemas => _schemas.AsReadOnly();

    private DatasetVersion() { }

    public static DatasetVersion Create(
        Guid id,
        TenantId tenantId,
        Guid datasetId,
        string version,
        DateTime periodStart,
        DateTime periodEnd,
        IReadOnlyList<string> assetSymbols,
        Guid? createdBy = null)
    {
        var datasetVersion = new DatasetVersion
        {
            Id = id,
            TenantId = tenantId,
            DatasetId = datasetId,
            Version = version,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            AssetSymbols = assetSymbols,
            CreatedBy = createdBy
        };

        datasetVersion.AddDomainEvent(new DatasetVersionCreatedV1(tenantId, datasetId, id, version));

        return datasetVersion;
    }

    public void AddTransformation(DatasetTransformation transformation)
    {
        _transformations.Add(transformation);
    }

    public void AddSnapshot(DatasetSnapshot snapshot)
    {
        _snapshots.Add(snapshot);
    }

    public void AddPartition(DatasetPartition partition)
    {
        _partitions.Add(partition);
    }

    public void AddMetadata(DatasetMetadata metadata)
    {
        _metadata.Add(metadata);
    }

    public void AddSchema(DatasetSchema schema)
    {
        _schemas.Add(schema);
    }

    public void Publish()
    {
        IsPublished = true;
        IsArchived = false;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new DatasetPublishedV1(TenantId, DatasetId, Id));
    }

    public void Archive()
    {
        IsPublished = false;
        IsArchived = true;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new DatasetArchivedV1(TenantId, DatasetId, Id));
    }
}