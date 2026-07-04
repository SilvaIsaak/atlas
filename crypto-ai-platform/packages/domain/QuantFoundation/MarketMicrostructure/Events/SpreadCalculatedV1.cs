using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Events;

public class SpreadCalculatedV1 : DomainEvent
{
    public Guid ModelId { get; init; }
    public decimal SpreadBps { get; init; }
    public string AssetSymbol { get; init; } = string.Empty;

    public SpreadCalculatedV1(TenantId tenantId, Guid modelId, decimal spreadBps, string assetSymbol)
    {
        TenantId = tenantId;
        ModelId = modelId;
        SpreadBps = spreadBps;
        AssetSymbol = assetSymbol;
    }
}
