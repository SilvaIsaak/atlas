using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.MarketData.ValueObjects;

public record LiquidationData(
    DateTime Timestamp,
    decimal Price,
    decimal Quantity,
    SimulatedOrderSide Side,
    string LiquidationType);