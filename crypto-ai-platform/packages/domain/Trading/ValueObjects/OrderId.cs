namespace CryptoAIPlatform.Domain.Trading.ValueObjects;

public record OrderId(Guid Value);
public record PositionId(Guid Value);
public record PortfolioId(Guid Value);
public record OrderPrice(decimal Value);
public record OrderQuantity(decimal Value);
public record StopLoss(decimal? Value);
public record TakeProfit(decimal? Value);
public record Leverage(decimal Value);
public record Margin(decimal Used, decimal Available);
public record Fee(decimal Amount, string Currency, string Type);
public record PnL(decimal Unrealized, decimal Realized);
public record Drawdown(decimal Value, decimal Percentage);
public record ExecutionTime(DateTime Value);
public record Slippage(decimal Bps, decimal Amount);
public record Latency(TimeSpan Value);
