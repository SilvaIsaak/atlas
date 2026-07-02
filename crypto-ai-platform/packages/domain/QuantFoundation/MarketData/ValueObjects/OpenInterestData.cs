namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

public record OpenInterestData(
    DateTime Timestamp,
    decimal OpenInterest);