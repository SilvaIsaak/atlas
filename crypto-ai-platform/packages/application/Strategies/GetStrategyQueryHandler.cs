using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Strategies;

public class GetStrategyQueryHandler : IRequestHandler<GetStrategyQuery, GetStrategyResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetStrategyQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetStrategyResponse> Handle(GetStrategyQuery request, CancellationToken cancellationToken)
    {
        var strategy = await _dbContext.Strategies
            .FirstOrDefaultAsync(s => s.Id == request.StrategyId && s.UserId == request.UserId, cancellationToken);

        if (strategy == null)
        {
            throw new KeyNotFoundException("Strategy not found");
        }

        return new GetStrategyResponse(
            strategy.Id,
            strategy.UserId,
            strategy.Name,
            strategy.Description,
            strategy.Code,
            strategy.AssetSymbol,
            strategy.Status,
            strategy.ResearchStudyId,
            strategy.CreatedAt,
            strategy.UpdatedAt);
    }
}
