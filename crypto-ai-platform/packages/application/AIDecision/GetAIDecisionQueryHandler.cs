using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.AIDecision;

public class GetAIDecisionQueryHandler : IRequestHandler<GetAIDecisionQuery, GetAIDecisionResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAIDecisionQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAIDecisionResponse> Handle(GetAIDecisionQuery request, CancellationToken cancellationToken)
    {
        var aiDecision = await _dbContext.AIDecisions
            .FirstOrDefaultAsync(ad => ad.Id == request.AIDecisionId && ad.UserId == request.UserId, cancellationToken);

        if (aiDecision == null)
        {
            throw new KeyNotFoundException("AIDecision not found");
        }

        return new GetAIDecisionResponse(
            aiDecision.Id,
            aiDecision.UserId,
            aiDecision.StrategyId,
            aiDecision.Symbol,
            aiDecision.DecisionType,
            aiDecision.Confidence,
            aiDecision.Reasoning,
            aiDecision.SuggestedQuantity,
            aiDecision.SuggestedPrice,
            aiDecision.Executed,
            aiDecision.ExecutedAt,
            aiDecision.CreatedAt,
            aiDecision.UpdatedAt
        );
    }
}