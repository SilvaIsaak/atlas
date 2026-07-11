namespace CryptoAIPlatform.Domain.PortfolioAnalytics.ValueObjects;

public record SortinoRatio(decimal Value, decimal RiskFreeRate, int Periods);
