using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class TradeFlow : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public decimal BuyVolume { get; private set; }
    public decimal SellVolume { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private TradeFlow() { }

    public static TradeFlow Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        DateTime timestamp,
        decimal buyVolume,
        decimal sellVolume,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new TradeFlow
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            Timestamp = timestamp,
            BuyVolume = buyVolume,
            SellVolume = sellVolume,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
