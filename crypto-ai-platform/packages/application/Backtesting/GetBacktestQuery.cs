using MediatR;
using CryptoAIPlatform.Domain.Backtesting;

namespace CryptoAIPlatform.Application.Backtesting;

public record GetBacktestQuery : IRequest<GetBacktestResponse>
{
    public Guid BacktestId { get; init; }
    public Guid UserId { get; init; }
}

public record GetBacktestResponse(
    Guid BacktestId,
    Guid UserId,
    Guid StrategyId,
    string Name,
    string Description,
    string AssetSymbol,
    DateTime StartDate,
    DateTime EndDate,
    decimal InitialCapital,
    BacktestStatus Status,
    BacktestResult? Result,
    DateTime? StartedAt,
    DateTime? CompletedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);