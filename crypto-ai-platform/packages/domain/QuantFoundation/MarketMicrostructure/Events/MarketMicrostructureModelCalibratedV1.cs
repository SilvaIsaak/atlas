using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Events;

public class MarketMicrostructureModelCalibratedV1 : DomainEvent
{
    public Guid ModelId { get; init; }
    public string AssetSymbol { get; init; } = string.Empty;

    public MarketMicrostructureModelCalibratedV1(TenantId tenantId, Guid modelId, string assetSymbol)
    {
        TenantId = tenantId;
        ModelId = modelId;
        AssetSymbol = assetSymbol;
    }
}