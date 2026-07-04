namespace CryptoAIPlatform.Application.MarketMicrostructure;

public record MarketMicrostructureModelDto(
    Guid Id,
    string Name,
    string AssetSymbol,
    DateTime CalibratedAt,
    bool IsActive);

public record SpreadDto(decimal Bid, decimal Ask, decimal SpreadBps);
