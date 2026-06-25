using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.WalkForward;

public class GetWalkForwardQueryHandler : IRequestHandler<GetWalkForwardQuery, GetWalkForwardResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetWalkForwardQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetWalkForwardResponse> Handle(GetWalkForwardQuery request, CancellationToken cancellationToken)
    {
        var walkForward = await _dbContext.WalkForwards
            .FirstOrDefaultAsync(wf => wf.Id == request.WalkForwardId && wf.UserId == request.UserId, cancellationToken);

        if (walkForward == null)
        {
            throw new KeyNotFoundException("WalkForward not found");
        }

        return new GetWalkForwardResponse(
            walkForward.Id,
            walkForward.UserId,
            walkForward.StrategyId,
            walkForward.Name,
            walkForward.Description,
            walkForward.AssetSymbol,
            walkForward.StartDate,
            walkForward.EndDate,
            walkForward.TrainingWindowDays,
            walkForward.TestingWindowDays,
            walkForward.InitialCapital,
            walkForward.Status,
            walkForward.WindowResults,
            walkForward.TotalOutOfSampleReturn,
            walkForward.AverageSharpeRatio,
            walkForward.StartedAt,
            walkForward.CompletedAt,
            walkForward.CreatedAt,
            walkForward.UpdatedAt
        );
    }
}