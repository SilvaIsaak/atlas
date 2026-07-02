namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

public record OrderBookSnapshot(
    DateTime Timestamp,
    IReadOnlyList<OrderBookLevel> Bids,
    IReadOnlyList<OrderBookLevel> Asks);