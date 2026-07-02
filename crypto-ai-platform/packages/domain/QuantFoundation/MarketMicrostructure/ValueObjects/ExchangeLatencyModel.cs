namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

public record ExchangeLatencyModel(
    int MeanLatencyMs,
    int P99LatencyMs,
    int P999LatencyMs);