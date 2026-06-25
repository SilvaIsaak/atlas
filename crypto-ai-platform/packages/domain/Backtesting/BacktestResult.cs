namespace CryptoAIPlatform.Domain.Backtesting;

public record BacktestResult(
    decimal TotalReturn,
    decimal SharpeRatio,
    decimal SortinoRatio,
    decimal MaxDrawdown,
    int NumberOfTrades,
    decimal WinRate,
    decimal ProfitFactor,
    decimal AverageWin,
    decimal AverageLoss,
    TimeSpan AverageTradeDuration
);