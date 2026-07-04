using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class OrderImbalance : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public OrderFlowImbalance Imbalance { get; private set; } = null!;
    public DateTime Timestamp { get; private set; }
    public string AssetSymbol { get; private set; } = string.Empty;

    private OrderImbalance() { }

    public static OrderImbalance Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        OrderFlowImbalance imbalance,
        DateTime timestamp,
        string assetSymbol,
        Guid? createdBy = null)
    {
        return new OrderImbalance
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            Imbalance = imbalance,
            Timestamp = timestamp,
            AssetSymbol = assetSymbol,
            CreatedBy = createdBy
        };
    }
}
