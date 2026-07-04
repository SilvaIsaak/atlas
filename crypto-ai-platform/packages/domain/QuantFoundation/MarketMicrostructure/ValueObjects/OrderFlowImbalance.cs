namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

public record OrderFlowImbalance(decimal BuyRatio, decimal SellRatio, decimal Imbalance);
