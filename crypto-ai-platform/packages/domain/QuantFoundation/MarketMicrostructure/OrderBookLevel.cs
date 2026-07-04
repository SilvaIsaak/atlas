using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

public class OrderBookLevel : BaseEntity<Guid>
{
    public Guid ModelId { get; private set; }
    public int Level { get; private set; }
    public string Side { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public decimal Quantity { get; private set; }
    public DateTime Timestamp { get; private set; }

    private OrderBookLevel() { }

    public static OrderBookLevel Create(
        Guid id,
        TenantId tenantId,
        Guid modelId,
        int level,
        string side,
        decimal price,
        decimal quantity,
        DateTime timestamp,
        Guid? createdBy = null)
    {
        return new OrderBookLevel
        {
            Id = id,
            TenantId = tenantId,
            ModelId = modelId,
            Level = level,
            Side = side,
            Price = price,
            Quantity = quantity,
            Timestamp = timestamp,
            CreatedBy = createdBy
        };
    }
}
