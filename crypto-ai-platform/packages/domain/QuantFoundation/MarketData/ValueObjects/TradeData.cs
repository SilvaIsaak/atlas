using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

public record TradeData(
    string TradeId,
    DateTime Timestamp,
    decimal Price,
    decimal Quantity,
    SimulatedOrderSide Side);