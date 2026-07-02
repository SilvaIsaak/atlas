namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

public record OrderBookLevel(
    decimal Price,
    decimal Quantity);