namespace CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.ValueObjects;

public record SlippageEstimate(decimal Expected, decimal WorstCase, decimal Confidence);
