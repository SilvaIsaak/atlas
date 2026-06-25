using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Backtesting;

public class GetAllBacktestsQueryHandler : IRequestHandler<GetAllBacktestsQuery, List<GetBacktestResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllBacktestsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetBacktestResponse>> Handle(GetAllBacktestsQuery request, CancellationToken cancellationToken)
    {
        var backtests = await _dbContext.Backtests
            .Where(b => b.UserId == request.UserId)
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new GetBacktestResponse(
                b.Id,
                b.UserId,
                b.StrategyId,
                b.Name,
                b.Description,
                b.AssetSymbol,
                b.StartDate,
                b.EndDate,
                b.InitialCapital,
                b.Status,
                b.Result,
                b.StartedAt,
                b.CompletedAt,
                b.CreatedAt,
                b.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return backtests;
    }
}