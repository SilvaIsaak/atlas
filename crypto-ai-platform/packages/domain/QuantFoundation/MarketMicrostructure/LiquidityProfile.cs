using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class LiquidityProfile : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;
    public string? OrderBookLevels { get; private set; }

    private LiquidityProfile() { }

    public static LiquidityProfile Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        string assetSymbol,
        string? orderBookLevels = null,
        Guid? createdBy = null)
    {
        return new LiquidityProfile
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            AssetSymbol = assetSymbol,
            OrderBookLevels = orderBookLevels,
            CreatedBy = createdBy
        };
    }
}