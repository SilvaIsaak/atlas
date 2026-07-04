namespace CryptoAIPlatform.Application.Trading;

public record OrderDto(
    Guid Id,
    Guid PortfolioId,
    string Symbol,
    string Side,
    string Type,
    string TimeInForce,
    decimal Quantity,
    decimal? Price,
    string Status,
    DateTime CreatedAt);

public record PositionDto(
    Guid Id,
    Guid PortfolioId,
    string Symbol,
    decimal Quantity,
    decimal EntryPrice,
    string Status,
    DateTime? OpenedAt);

public record PortfolioDto(
    Guid Id,
    string Name,
    string Status,
    DateTime CreatedAt);

public record RiskProfileDto(
    Guid Id,
    Guid PortfolioId,
    string RiskLevel,
    decimal MaxDrawdown);

public record TradeExecutionDto(
    Guid Id,
    Guid OrderId,
    string Status,
    DateTime? StartedAt);
