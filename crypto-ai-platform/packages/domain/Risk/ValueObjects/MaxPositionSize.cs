namespace CryptoAIPlatform.Domain.Risk.ValueObjects;

public record MaxPositionSize(decimal Value);
public record MaxExposure(decimal Value);
public record MaxDrawdown(decimal Value, decimal Percentage);
public record DailyLossLimit(decimal Value);
public record PortfolioLeverage(decimal Value);
public record MarginUsage(decimal Used, decimal Available, decimal UsedPercentage);
public record MaintenanceMargin(decimal Value);
public record InitialMargin(decimal Value);
public record VaRValue(decimal Value, decimal ConfidenceLevel, TimeSpan Horizon);
public record ExpectedShortfall(decimal Value, decimal ConfidenceLevel, TimeSpan Horizon);
public record LiquidationPrice(decimal Value);
public record ConcentrationRisk(decimal Value, string Asset);
public record CorrelationScore(decimal Value, string AssetPair);
public record RiskScore(decimal Score, Risk.Enums.RiskStatus Status);
