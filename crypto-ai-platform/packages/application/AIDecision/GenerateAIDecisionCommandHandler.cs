using MediatR;
using CryptoAIPlatform.Domain.AIDecision;
using DomainAIDecision = CryptoAIPlatform.Domain.AIDecision.AIDecision;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.AIDecision;

public class GenerateAIDecisionCommandHandler : IRequestHandler<GenerateAIDecisionCommand, GenerateAIDecisionResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GenerateAIDecisionCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GenerateAIDecisionResponse> Handle(GenerateAIDecisionCommand request, CancellationToken cancellationToken)
    {
        // Simulação de geração de decisão AI
        var random = new Random();
        var decisionTypes = new[] { AIDecisionType.Buy, AIDecisionType.Sell, AIDecisionType.Hold };
        var selectedDecision = decisionTypes[random.Next(decisionTypes.Length)];
        var confidence = (decimal)(random.NextDouble() * 0.5 + 0.5); // 0.5 to 1.0

        var aiDecision = new DomainAIDecision
        {
            UserId = request.UserId,
            StrategyId = request.StrategyId,
            Symbol = request.Symbol,
            DecisionType = selectedDecision,
            Confidence = confidence,
            Reasoning = "Análise de indicadores técnicos e padrões de mercado sugere esta decisão.",
            SuggestedQuantity = 10,
            SuggestedPrice = 50000,
            Executed = false
        };

        await _dbContext.AIDecisions.AddAsync(aiDecision, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new GenerateAIDecisionResponse
        {
            AIDecisionId = aiDecision.Id,
            DecisionType = aiDecision.DecisionType,
            Confidence = aiDecision.Confidence,
            Reasoning = aiDecision.Reasoning
        };
    }
}