using MediatR;
using CryptoAIPlatform.Domain.Strategies;

namespace CryptoAIPlatform.Application.Strategies;

public record ChangeStrategyStatusCommand : IRequest<ChangeStrategyStatusResponse>
{
    public Guid StrategyId { get; init; }
    public Guid UserId { get; init; }
    public StrategyStatus NewStatus { get; init; }
}

public record ChangeStrategyStatusResponse
{
    public Guid StrategyId { get; init; }
    public StrategyStatus Status { get; init; }
}
