using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData;

public class MarketDataAsset : BaseEntity<Guid>
{
    public Guid DataSourceId { get; private set; }
    public string Symbol { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private MarketDataAsset() { }

    public static MarketDataAsset Create(
        Guid id,
        TenantId tenantId,
        Guid dataSourceId,
        string symbol,
        string name,
        bool isActive = true,
        Guid? createdBy = null)
    {
        return new MarketDataAsset
        {
            Id = id,
            TenantId = tenantId,
            DataSourceId = dataSourceId,
            Symbol = symbol,
            Name = name,
            IsActive = isActive,
            CreatedBy = createdBy
        };
    }
}