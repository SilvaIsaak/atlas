using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.AIDecision;

public class GetAllAIDecisionsQueryHandler : IRequestHandler<GetAllAIDecisionsQuery, List<GetAIDecisionResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllAIDecisionsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetAIDecisionResponse>> Handle(GetAllAIDecisionsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.AIDecisions.Where(ad => ad.UserId == request.UserId);

        if (request.StrategyId.HasValue)
        {
            query = query.Where(ad => ad.StrategyId == request.StrategyId.Value);
        }

        var aiDecisions = await query
            .OrderByDescending(ad => ad.CreatedAt)
            .Select(ad => new GetAIDecisionResponse(
                ad.Id,
                ad.UserId,
                ad.StrategyId,
                ad.Symbol,
                ad.DecisionType,
                ad.Confidence,
                ad.Reasoning,
                ad.SuggestedQuantity,
                ad.SuggestedPrice,
                ad.Executed,
                ad.ExecutedAt,
                ad.CreatedAt,
                ad.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return aiDecisions;
    }
}