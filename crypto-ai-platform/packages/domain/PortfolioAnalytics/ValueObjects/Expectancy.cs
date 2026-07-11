namespace CryptoAIPlatform.Domain.PortfolioAnalytics.ValueObjects;

public record Expectancy(decimal AverageProfit, decimal AverageLoss, decimal Total);
