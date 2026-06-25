using MediatR;
using CryptoAIPlatform.Domain.AIDecision;

namespace CryptoAIPlatform.Application.AIDecision;

public record GenerateAIDecisionCommand : IRequest<GenerateAIDecisionResponse>
{
    public Guid UserId { get; init; }
    public Guid StrategyId { get; init; }
    public string Symbol { get; init; } = string.Empty;
    public AIModelConfig? ModelConfig { get; init; }
}

public record GenerateAIDecisionResponse
{
    public Guid AIDecisionId { get; init; }
    public AIDecisionType DecisionType { get; init; }
    public decimal Confidence { get; init; }
    public string Reasoning { get; init; } = string.Empty;
}