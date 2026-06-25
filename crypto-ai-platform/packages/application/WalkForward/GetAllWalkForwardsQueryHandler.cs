using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.WalkForward;

public class GetAllWalkForwardsQueryHandler : IRequestHandler<GetAllWalkForwardsQuery, List<GetWalkForwardResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllWalkForwardsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetWalkForwardResponse>> Handle(GetAllWalkForwardsQuery request, CancellationToken cancellationToken)
    {
        var walkForwards = await _dbContext.WalkForwards
            .Where(wf => wf.UserId == request.UserId)
            .OrderByDescending(wf => wf.CreatedAt)
            .Select(wf => new GetWalkForwardResponse(
                wf.Id,
                wf.UserId,
                wf.StrategyId,
                wf.Name,
                wf.Description,
                wf.AssetSymbol,
                wf.StartDate,
                wf.EndDate,
                wf.TrainingWindowDays,
                wf.TestingWindowDays,
                wf.InitialCapital,
                wf.Status,
                wf.WindowResults,
                wf.TotalOutOfSampleReturn,
                wf.AverageSharpeRatio,
                wf.StartedAt,
                wf.CompletedAt,
                wf.CreatedAt,
                wf.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return walkForwards;
    }
}