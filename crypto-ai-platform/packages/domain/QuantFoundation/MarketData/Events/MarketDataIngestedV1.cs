using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.Events;

public class MarketDataIngestedV1 : DomainEvent
{
    public Guid DataSourceId { get; init; }
    public string AssetSymbol { get; init; } = string.Empty;
    public string DataType { get; init; } = string.Empty;
    public Guid IngestionJobId { get; init; }

    public MarketDataIngestedV1(TenantId tenantId, Guid dataSourceId, string assetSymbol, string dataType, Guid ingestionJobId)
    {
        TenantId = tenantId;
        DataSourceId = dataSourceId;
        AssetSymbol = assetSymbol;
        DataType = dataType;
        IngestionJobId = ingestionJobId;
    }
}