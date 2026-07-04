namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

public record ExecutionCost(decimal TotalCost, decimal Slippage, decimal Fees);
