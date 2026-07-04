using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class TWAPSnapshot : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public decimal TWAP { get; private set; }
    public DateTime StartTimestamp { get; private set; }
    public DateTime EndTimestamp { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private TWAPSnapshot() { }

    public static TWAPSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        decimal twap,
        DateTime startTimestamp,
        DateTime endTimestamp,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new TWAPSnapshot
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            TWAP = twap,
            StartTimestamp = startTimestamp,
            EndTimestamp = endTimestamp,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
