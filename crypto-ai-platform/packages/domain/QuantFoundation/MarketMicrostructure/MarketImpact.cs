using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class MarketImpact : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public ImpactCost ImpactCost { get; private set; } = null!;
    public decimal OrderSize { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private MarketImpact() { }

    public static MarketImpact Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        ImpactCost impactCost,
        decimal orderSize,
        DateTime timestamp,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new MarketImpact
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            ImpactCost = impactCost,
            OrderSize = orderSize,
            Timestamp = timestamp,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
