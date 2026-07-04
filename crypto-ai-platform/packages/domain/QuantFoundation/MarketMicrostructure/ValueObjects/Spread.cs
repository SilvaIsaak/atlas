namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

public record Spread(decimal BidPrice, decimal AskPrice, decimal SpreadBps);
