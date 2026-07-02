namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

public record OhlcvData(
    DateTime Timestamp,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal Volume);