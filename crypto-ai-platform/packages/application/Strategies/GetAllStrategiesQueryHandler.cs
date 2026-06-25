using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Strategies;

public class GetAllStrategiesQueryHandler : IRequestHandler<GetAllStrategiesQuery, List<GetStrategyResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllStrategiesQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetStrategyResponse>> Handle(GetAllStrategiesQuery request, CancellationToken cancellationToken)
    {
        var strategies = await _dbContext.Strategies
            .Where(s => s.UserId == request.UserId)
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => new GetStrategyResponse(
                s.Id,
                s.UserId,
                s.Name,
                s.Description,
                s.Code,
                s.AssetSymbol,
                s.Status,
                s.ResearchStudyId,
                s.CreatedAt,
                s.UpdatedAt))
            .ToListAsync(cancellationToken);

        return strategies;
    }
}
