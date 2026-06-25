namespace CryptoAIPlatform.Domain.Execution;

public record ExecutionOrder(
    Guid OrderId,
    string ExchangeOrderId,
    string Symbol,
    string OrderType, // Market, Limit, Stop, StopLimit
    string Side, // Buy, Sell
    decimal Quantity,
    decimal? Price,
    decimal? StopPrice,
    decimal? ExecutedQuantity,
    decimal? AveragePrice,
    ExecutionStatus Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ExecutedAt
);