using MediatR;

namespace CryptoAIPlatform.Application.Trading;

public record CreateOrderCommand(
    Guid PortfolioId,
    string Symbol,
    string Side,
    string Type,
    string TimeInForce,
    decimal Quantity,
    decimal? Price = null,
    decimal? StopLoss = null,
    decimal? TakeProfit = null,
    decimal? Leverage = null) : IRequest<OrderDto>;

public record CancelOrderCommand(
    Guid OrderId,
    string Reason) : IRequest<OrderDto>;

public record ExecuteOrderCommand(
    Guid OrderId) : IRequest<TradeExecutionDto>;

public record OpenPositionCommand(
    Guid PortfolioId,
    string Symbol,
    decimal Quantity,
    decimal EntryPrice,
    decimal Leverage) : IRequest<PositionDto>;

public record ClosePositionCommand(
    Guid PositionId) : IRequest<PositionDto>;

public record UpdatePortfolioCommand(
    Guid PortfolioId,
    string? Name,
    string? Description) : IRequest<PortfolioDto>;
