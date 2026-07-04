using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Trading.Events;

public class OrderCreatedV1 : DomainEvent
{
    public Guid OrderId { get; init; }
    public string Symbol { get; init; } = string.Empty;
}

public class OrderSubmittedV1 : DomainEvent
{
    public Guid OrderId { get; init; }
}

public class OrderFilledV1 : DomainEvent
{
    public Guid OrderId { get; init; }
    public decimal FilledQuantity { get; init; }
}

public class OrderCancelledV1 : DomainEvent
{
    public Guid OrderId { get; init; }
    public string Reason { get; init; } = string.Empty;
}

public class PositionOpenedV1 : DomainEvent
{
    public Guid PositionId { get; init; }
    public string Symbol { get; init; } = string.Empty;
}

public class PositionClosedV1 : DomainEvent
{
    public Guid PositionId { get; init; }
    public string Symbol { get; init; } = string.Empty;
}

public class PortfolioUpdatedV1 : DomainEvent
{
    public Guid PortfolioId { get; init; }
}

public class RiskLimitExceededV1 : DomainEvent
{
    public Guid PortfolioId { get; init; }
    public string RiskType { get; init; } = string.Empty;
}

public class TradeExecutedV1 : DomainEvent
{
    public Guid TradeExecutionId { get; init; }
    public Guid OrderId { get; init; }
}
