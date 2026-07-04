using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class BidAskSpread : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public Spread Spread { get; private set; } = null!;
    public DateTime Timestamp { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private BidAskSpread() { }

    public static BidAskSpread Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        Spread spread,
        DateTime timestamp,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new BidAskSpread
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            Spread = spread,
            Timestamp = timestamp,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
