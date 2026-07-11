using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Notifications.Events;

public class OrderCreatedNotificationV1 : DomainEvent
{
    public Guid OrderId { get; init; }
    public string Symbol { get; init; }

    public OrderCreatedNotificationV1(TenantId tenantId, Guid orderId, string symbol)
    {
        TenantId = tenantId;
        OrderId = orderId;
        Symbol = symbol;
    }
}

public class OrderExecutedNotificationV1 : DomainEvent
{
    public Guid OrderId { get; init; }
    public decimal Price { get; init; }

    public OrderExecutedNotificationV1(TenantId tenantId, Guid orderId, decimal price)
    {
        TenantId = tenantId;
        OrderId = orderId;
        Price = price;
    }
}

public class StopTriggeredNotificationV1 : DomainEvent
{
    public Guid OrderId { get; init; }
    public decimal Price { get; init; }

    public StopTriggeredNotificationV1(TenantId tenantId, Guid orderId, decimal price)
    {
        TenantId = tenantId;
        OrderId = orderId;
        Price = price;
    }
}

public class TakeProfitNotificationV1 : DomainEvent
{
    public Guid OrderId { get; init; }
    public decimal Price { get; init; }

    public TakeProfitNotificationV1(TenantId tenantId, Guid orderId, decimal price)
    {
        TenantId = tenantId;
        OrderId = orderId;
        Price = price;
    }
}

public class CriticalFailureNotificationV1 : DomainEvent
{
    public string Message { get; init; }

    public CriticalFailureNotificationV1(TenantId tenantId, string message)
    {
        TenantId = tenantId;
        Message = message;
    }
}
