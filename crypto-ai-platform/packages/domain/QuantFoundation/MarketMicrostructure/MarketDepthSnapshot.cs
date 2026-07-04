using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class MarketDepthSnapshot : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public List<DepthLevel> BidLevels { get; private set; } = new();
    public List<DepthLevel> AskLevels { get; private set; } = new();
    public DateTime Timestamp { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private MarketDepthSnapshot() { }

    public static MarketDepthSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        List<DepthLevel> bidLevels,
        List<DepthLevel> askLevels,
        DateTime timestamp,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new MarketDepthSnapshot
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            BidLevels = bidLevels,
            AskLevels = askLevels,
            Timestamp = timestamp,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
