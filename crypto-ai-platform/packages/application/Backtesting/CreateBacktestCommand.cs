using MediatR;
using CryptoAIPlatform.Domain.Backtesting;

namespace CryptoAIPlatform.Application.Backtesting;

public record CreateBacktestCommand : IRequest<CreateBacktestResponse>
{
    public Guid UserId { get; init; }
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal InitialCapital { get; init; }
}

public record CreateBacktestResponse
{
    public Guid BacktestId { get; init; }
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal InitialCapital { get; init; }
    public BacktestStatus Status { get; init; }
}