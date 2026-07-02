using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.DataQuality;

public class DataQualityJob : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<Anomaly> _anomalies = [];

    public string AssetSymbol { get; private set; } = string.Empty;
    public DateTime PeriodStart { get; private set; }
    public DateTime PeriodEnd { get; private set; }
    public DataQualityJobStatus Status { get; private set; }
    public int TotalChecksCount { get; private set; }
    public int AnomaliesCount { get; private set; }

    public IReadOnlyCollection<Anomaly> Anomalies => _anomalies.AsReadOnly();

    private DataQualityJob() { }

    public static DataQualityJob Create(
        Guid id,
        TenantId tenantId,
        string assetSymbol,
        DateTime periodStart,
        DateTime periodEnd,
        Guid? createdBy = null)
    {
        return new DataQualityJob
        {
            Id = id,
            TenantId = tenantId,
            AssetSymbol = assetSymbol,
            PeriodStart = periodStart,
            PeriodEnd = periodEnd,
            Status = DataQualityJobStatus.Pending,
            TotalChecksCount = 0,
            AnomaliesCount = 0,
            CreatedBy = createdBy
        };
    }

    public void Start()
    {
        Status = DataQualityJobStatus.Running;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete(int totalChecks, int anomaliesCount)
    {
        Status = DataQualityJobStatus.Completed;
        TotalChecksCount = totalChecks;
        AnomaliesCount = anomaliesCount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        Status = DataQualityJobStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddAnomaly(Anomaly anomaly)
    {
        _anomalies.Add(anomaly);
    }
}