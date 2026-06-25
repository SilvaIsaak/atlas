using MediatR;

namespace CryptoAIPlatform.Application.Backtesting;

public record ExecuteBacktestCommand : IRequest<ExecuteBacktestResponse>
{
    public Guid BacktestId { get; init; }
    public Guid UserId { get; init; }
}

public record ExecuteBacktestResponse
{
    public Guid BacktestId { get; init; }
    public string Status { get; init; } = string.Empty;
}