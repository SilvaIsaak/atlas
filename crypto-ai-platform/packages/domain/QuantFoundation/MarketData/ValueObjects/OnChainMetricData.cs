namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

public record OnChainMetricData(
    DateTime Timestamp,
    string MetricName,
    decimal MetricValue);