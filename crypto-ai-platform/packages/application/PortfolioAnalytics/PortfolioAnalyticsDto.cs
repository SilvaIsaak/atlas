namespace CryptoAIPlatform.Application.PortfolioAnalytics;

public record PortfolioAnalyticsDto(
    Guid Id,
    Guid PortfolioId,
    DateTime CalculatedAt,
    decimal? SharpeRatio,
    decimal? SortinoRatio,
    decimal? CalmarRatio,
    decimal? ProfitFactor,
    decimal? WinRate,
    decimal? Volatility);

public record PerformanceSnapshotDto(
    Guid Id,
    Guid PortfolioId,
    DateTime Timestamp,
    decimal TotalEquity,
    decimal TotalReturn,
    decimal DailyReturn,
    decimal MonthlyReturn,
    decimal AnnualReturn);

public record EquityCurvePointDto(
    DateTime Timestamp,
    decimal Equity);

public record DrawdownPointDto(
    DateTime PeakAt,
    DateTime TroughAt,
    decimal DrawdownPercentage);
