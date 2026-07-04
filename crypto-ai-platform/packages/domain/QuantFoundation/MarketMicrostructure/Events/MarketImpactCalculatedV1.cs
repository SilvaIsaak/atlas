using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Events;

public class MarketImpactCalculatedV1 : DomainEvent
{
    public Guid ModelId { get; init; }
    public decimal ImpactCostBps { get; init; }
    public string AssetSymbol { get; init; } = string.Empty;

    public MarketImpactCalculatedV1(TenantId tenantId, Guid modelId, decimal impactCostBps, string assetSymbol)
    {
        TenantId = tenantId;
        ModelId = modelId;
        ImpactCostBps = impactCostBps;
        AssetSymbol = assetSymbol;
    }
}
