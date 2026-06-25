using MediatR;
using CryptoAIPlatform.Domain.Strategies;

namespace CryptoAIPlatform.Application.Strategies;

public record GetStrategyQuery : IRequest<GetStrategyResponse>
{
    public Guid StrategyId { get; init; }
    public Guid UserId { get; init; }
}

public record GetStrategyResponse(
    Guid StrategyId,
    Guid UserId,
    string Name,
    string Description,
    string Code,
    string AssetSymbol,
    StrategyStatus Status,
    Guid? ResearchStudyId,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
