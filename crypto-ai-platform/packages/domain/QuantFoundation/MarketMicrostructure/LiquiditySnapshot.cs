using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class LiquiditySnapshot : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public LiquidityScore LiquidityScore { get; private set; } = null!;
    public DateTime Timestamp { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private LiquiditySnapshot() { }

    public static LiquiditySnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        LiquidityScore liquidityScore,
        DateTime timestamp,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new LiquiditySnapshot
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            LiquidityScore = liquidityScore,
            Timestamp = timestamp,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
