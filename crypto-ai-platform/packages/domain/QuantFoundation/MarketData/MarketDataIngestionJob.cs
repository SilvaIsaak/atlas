using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData;

public class MarketDataIngestionJob : BaseEntity<Guid>
{
    public Guid DataSourceId { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;
    public MarketDataType DataType { get; private set; }
    public MarketDataIngestionStatus Status { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private MarketDataIngestionJob() { }

    public static MarketDataIngestionJob Create(
        Guid id,
        TenantId tenantId,
        Guid dataSourceId,
        string assetSymbol,
        MarketDataType dataType,
        Guid? createdBy = null)
    {
        return new MarketDataIngestionJob
        {
            Id = id,
            TenantId = tenantId,
            DataSourceId = dataSourceId,
            AssetSymbol = assetSymbol,
            DataType = dataType,
            Status = MarketDataIngestionStatus.Pending,
            CreatedBy = createdBy
        };
    }

    public void Start()
    {
        Status = MarketDataIngestionStatus.Running;
        StartedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        Status = MarketDataIngestionStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        Status = MarketDataIngestionStatus.Failed;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}