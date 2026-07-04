using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class VWAPSnapshot : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public decimal VWAP { get; private set; }
    public decimal TotalVolume { get; private set; }
    public DateTime StartTimestamp { get; private set; }
    public DateTime EndTimestamp { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private VWAPSnapshot() { }

    public static VWAPSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        decimal vwap,
        decimal totalVolume,
        DateTime startTimestamp,
        DateTime endTimestamp,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new VWAPSnapshot
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            VWAP = vwap,
            TotalVolume = totalVolume,
            StartTimestamp = startTimestamp,
            EndTimestamp = endTimestamp,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
