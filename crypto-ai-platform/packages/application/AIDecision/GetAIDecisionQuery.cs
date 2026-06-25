using MediatR;
using CryptoAIPlatform.Domain.AIDecision;

namespace CryptoAIPlatform.Application.AIDecision;

public record GetAIDecisionQuery : IRequest<GetAIDecisionResponse>
{
    public Guid AIDecisionId { get; init; }
    public Guid UserId { get; init; }
}

public record GetAIDecisionResponse(
    Guid AIDecisionId,
    Guid UserId,
    Guid StrategyId,
    string Symbol,
    AIDecisionType DecisionType,
    decimal Confidence,
    string Reasoning,
    decimal? SuggestedQuantity,
    decimal? SuggestedPrice,
    bool Executed,
    DateTime? ExecutedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);