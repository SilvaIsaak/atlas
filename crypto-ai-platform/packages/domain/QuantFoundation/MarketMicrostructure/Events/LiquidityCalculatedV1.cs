using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Events;

public class LiquidityCalculatedV1 : DomainEvent
{
    public Guid ModelId { get; init; }
    public decimal Score { get; init; }
    public string AssetSymbol { get; init; } = string.Empty;

    public LiquidityCalculatedV1(TenantId tenantId, Guid modelId, decimal score, string assetSymbol)
    {
        TenantId = tenantId;
        ModelId = modelId;
        Score = score;
        AssetSymbol = assetSymbol;
    }
}
