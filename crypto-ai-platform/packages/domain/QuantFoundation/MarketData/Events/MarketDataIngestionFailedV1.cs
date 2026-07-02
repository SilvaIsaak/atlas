using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.Events;

public class MarketDataIngestionFailedV1 : DomainEvent
{
    public Guid DataSourceId { get; init; }
    public string AssetSymbol { get; init; } = string.Empty;
    public string ErrorMessage { get; init; } = string.Empty;

    public MarketDataIngestionFailedV1(TenantId tenantId, Guid dataSourceId, string assetSymbol, string errorMessage)
    {
        TenantId = tenantId;
        DataSourceId = dataSourceId;
        AssetSymbol = assetSymbol;
        ErrorMessage = errorMessage;
    }
}