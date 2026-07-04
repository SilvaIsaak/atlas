using MediatR;

namespace CryptoAIPlatform.Application.MarketMicrostructure;

public record CreateModelCommand(
    string Name,
    string AssetSymbol) : IRequest<MarketMicrostructureModelDto>;

public record CalculateSpreadCommand(
    Guid ModelId,
    decimal BidPrice,
    decimal AskPrice) : IRequest<SpreadDto>;

public record CalculateVWAPCommand(
    Guid ModelId,
    DateTime Start,
    DateTime End) : IRequest<decimal>;
