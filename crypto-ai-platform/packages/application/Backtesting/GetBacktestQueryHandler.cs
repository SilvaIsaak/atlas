using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Backtesting;

public class GetBacktestQueryHandler : IRequestHandler<GetBacktestQuery, GetBacktestResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetBacktestQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBacktestResponse> Handle(GetBacktestQuery request, CancellationToken cancellationToken)
    {
        var backtest = await _dbContext.Backtests
            .FirstOrDefaultAsync(b => b.Id == request.BacktestId && b.UserId == request.UserId, cancellationToken);

        if (backtest == null)
        {
            throw new KeyNotFoundException("Backtest not found");
        }

        return new GetBacktestResponse(
            backtest.Id,
            backtest.UserId,
            backtest.StrategyId,
            backtest.Name,
            backtest.Description,
            backtest.AssetSymbol,
            backtest.StartDate,
            backtest.EndDate,
            backtest.InitialCapital,
            backtest.Status,
            backtest.Result,
            backtest.StartedAt,
            backtest.CompletedAt,
            backtest.CreatedAt,
            backtest.UpdatedAt
        );
    }
}