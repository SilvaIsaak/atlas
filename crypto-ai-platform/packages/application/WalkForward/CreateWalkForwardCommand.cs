using MediatR;
using CryptoAIPlatform.Domain.WalkForward;

namespace CryptoAIPlatform.Application.WalkForward;

public record CreateWalkForwardCommand : IRequest<CreateWalkForwardResponse>
{
    public Guid UserId { get; init; }
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string AssetSymbol { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int TrainingWindowDays { get; init; }
    public int TestingWindowDays { get; init; }
    public decimal InitialCapital { get; init; }
}

public record CreateWalkForwardResponse
{
    public Guid WalkForwardId { get; init; }
    public Guid StrategyId { get; init; }
    public string Name { get; init; } = string.Empty;
    public WalkForwardStatus Status { get; init; }
}