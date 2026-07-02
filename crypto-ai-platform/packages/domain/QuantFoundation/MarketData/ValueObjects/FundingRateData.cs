namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

public record FundingRateData(
    DateTime Timestamp,
    decimal FundingRate,
    DateTime NextFundingTime);