using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class DatasetVersion : BaseEntity<Guid>
{
    private readonly List<DatasetTransformation> _transformations = [];

    public Guid DatasetId { get; private set; }
    public string Version { get; private set; } = string.Empty;
    public DateTime PeriodStart { get; private set; }
    public DateTime PeriodEnd { get; private set; }
    public IReadOnlyList<string> AssetSymbols { get; private set; } = [];

    public IReadOnlyCollection<DatasetTransformation> Transformations => _transformations.AsReadOnly();

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
        return new DatasetVersion
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
    }

    public void AddTransformation(DatasetTransformation transformation)
    {
        _transformations.Add(transformation);
    }
}