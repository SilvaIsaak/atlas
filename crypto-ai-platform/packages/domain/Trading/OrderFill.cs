using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Trading.ValueObjects;

namespace CryptoAIPlatform.Domain.Trading;

public class OrderFill : BaseEntity<Guid>
{
    public Guid OrderId { get; private set; }
    public OrderPrice Price { get; private set; } = null!;
    public OrderQuantity Quantity { get; private set; } = null!;
    public Fee Fee { get; private set; } = null!;
    public DateTime FilledAt { get; private set; }

    private OrderFill() { }

    public static OrderFill Create(
        Guid id,
        TenantId tenantId,
        Guid orderId,
        OrderPrice price,
        OrderQuantity quantity,
        Fee fee,
        DateTime filledAt,
        Guid? createdBy = null)
    {
        return new OrderFill
        {
            Id = id,
            TenantId = tenantId,
            OrderId = orderId,
            Price = price,
            Quantity = quantity,
            Fee = fee,
            FilledAt = filledAt,
            CreatedBy = createdBy
        };
    }
}

public class OrderFee : BaseEntity<Guid>
{
    public Guid OrderId { get; private set; }
    public Fee Fee { get; private set; } = null!;
    public DateTime OccurredAt { get; private set; }

    private OrderFee() { }

    public static OrderFee Create(
        Guid id,
        TenantId tenantId,
        Guid orderId,
        Fee fee,
        DateTime occurredAt,
        Guid? createdBy = null)
    {
        return new OrderFee
        {
            Id = id,
            TenantId = tenantId,
            OrderId = orderId,
            Fee = fee,
            OccurredAt = occurredAt,
            CreatedBy = createdBy
        };
    }
}

public class OrderStatusHistory : BaseEntity<Guid>
{
    public Guid OrderId { get; private set; }
    public Enums.OrderStatus Status { get; private set; }
    public DateTime OccurredAt { get; private set; }
    public string? Reason { get; private set; }

    private OrderStatusHistory() { }

    public static OrderStatusHistory Create(
        Guid id,
        TenantId tenantId,
        Guid orderId,
        Enums.OrderStatus status,
        DateTime occurredAt,
        string? reason,
        Guid? createdBy = null)
    {
        return new OrderStatusHistory
        {
            Id = id,
            TenantId = tenantId,
            OrderId = orderId,
            Status = status,
            OccurredAt = occurredAt,
            Reason = reason,
            CreatedBy = createdBy
        };
    }
}
