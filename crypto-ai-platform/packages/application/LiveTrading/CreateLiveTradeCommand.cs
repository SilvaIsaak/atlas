using MediatR;
using CryptoAIPlatform.Domain.LiveTrading;

namespace CryptoAIPlatform.Application.LiveTrading;

public record CreateLiveTradeCommand : IRequest<CreateLiveTradeResponse>
{
    public Guid UserId { get; init; }
    public Guid StrategyId { get; init; }
    public Guid ExecutionEngineId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public decimal InitialCapital { get; init; }
}

public record CreateLiveTradeResponse
{
    public Guid LiveTradeId { get; init; }
    public LiveTradeStatus Status { get; init; }
}