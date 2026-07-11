namespace CryptoAIPlatform.Domain.PortfolioAnalytics.ValueObjects;

public record SharpeRatio(decimal Value, decimal RiskFreeRate, int Periods);
