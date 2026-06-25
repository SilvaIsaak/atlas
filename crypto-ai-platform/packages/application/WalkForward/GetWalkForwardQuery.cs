using MediatR;
using CryptoAIPlatform.Domain.WalkForward;

namespace CryptoAIPlatform.Application.WalkForward;

public record GetWalkForwardQuery : IRequest<GetWalkForwardResponse>
{
    public Guid WalkForwardId { get; init; }
    public Guid UserId { get; init; }
}

public record GetWalkForwardResponse(
    Guid WalkForwardId,
    Guid UserId,
    Guid StrategyId,
    string Name,
    string Description,
    string AssetSymbol,
    DateTime StartDate,
    DateTime EndDate,
    int TrainingWindowDays,
    int TestingWindowDays,
    decimal InitialCapital,
    WalkForwardStatus Status,
    List<WalkForwardWindowResult>? WindowResults,
    decimal? TotalOutOfSampleReturn,
    decimal? AverageSharpeRatio,
    DateTime? StartedAt,
    DateTime? CompletedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);