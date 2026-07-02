using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class SimulatedFill : BaseEntity<Guid>
{
    public Guid OrderId { get; private set; }
    public decimal Price { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal Fee { get; private set; }
    public DateTime Timestamp { get; private set; }

    private SimulatedFill() { }

    public static SimulatedFill Create(
        Guid id,
        TenantId tenantId,
        Guid orderId,
        decimal price,
        decimal quantity,
        decimal fee,
        DateTime timestamp,
        Guid? createdBy = null)
    {
        return new SimulatedFill
        {
            Id = id,
            TenantId = tenantId,
            OrderId = orderId,
            Price = price,
            Quantity = quantity,
            Fee = fee,
            Timestamp = timestamp,
            CreatedBy = createdBy
        };
    }
}