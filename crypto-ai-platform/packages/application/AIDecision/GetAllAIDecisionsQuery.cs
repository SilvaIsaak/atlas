using MediatR;

namespace CryptoAIPlatform.Application.AIDecision;

public record GetAllAIDecisionsQuery : IRequest<List<GetAIDecisionResponse>>
{
    public Guid UserId { get; init; }
    public Guid? StrategyId { get; init; }
}