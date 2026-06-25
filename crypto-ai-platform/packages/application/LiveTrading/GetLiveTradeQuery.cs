using MediatR;
using CryptoAIPlatform.Domain.LiveTrading;

namespace CryptoAIPlatform.Application.LiveTrading;

public record GetLiveTradeQuery : IRequest<GetLiveTradeResponse>
{
    public Guid LiveTradeId { get; init; }
    public Guid UserId { get; init; }
}

public record GetLiveTradeResponse(
    Guid LiveTradeId,
    Guid UserId,
    Guid StrategyId,
    Guid ExecutionEngineId,
    string Name,
    string AssetSymbol,
    decimal InitialCapital,
    decimal? CurrentCapital,
    decimal? TotalReturn,
    LiveTradeStatus Status,
    DateTime? StartedAt,
    DateTime? StoppedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);