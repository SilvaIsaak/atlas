using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class SpreadData : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public decimal BidPrice { get; private set; }
    public decimal AskPrice { get; private set; }
    public decimal SpreadBps { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private SpreadData() { }

    public static SpreadData Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        DateTime timestamp,
        decimal bidPrice,
        decimal askPrice,
        decimal spreadBps,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new SpreadData
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            Timestamp = timestamp,
            BidPrice = bidPrice,
            AskPrice = askPrice,
            SpreadBps = spreadBps,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}