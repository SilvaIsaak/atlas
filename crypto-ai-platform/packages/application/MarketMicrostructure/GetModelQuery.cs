using MediatR;

namespace CryptoAIPlatform.Application.MarketMicrostructure;

public record GetModelQuery(Guid ModelId) : IRequest<MarketMicrostructureModelDto?>;

public record GetModelsByAssetQuery(string AssetSymbol) : IRequest<IReadOnlyList<MarketMicrostructureModelDto>>;
