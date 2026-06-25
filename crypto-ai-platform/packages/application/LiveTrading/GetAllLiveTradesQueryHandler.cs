using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.LiveTrading;

public class GetAllLiveTradesQueryHandler : IRequestHandler<GetAllLiveTradesQuery, List<GetLiveTradeResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllLiveTradesQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetLiveTradeResponse>> Handle(GetAllLiveTradesQuery request, CancellationToken cancellationToken)
    {
        var liveTrades = await _dbContext.LiveTrades
            .Where(lt => lt.UserId == request.UserId)
            .OrderByDescending(lt => lt.CreatedAt)
            .Select(lt => new GetLiveTradeResponse(
                lt.Id,
                lt.UserId,
                lt.StrategyId,
                lt.ExecutionEngineId,
                lt.Name,
                lt.AssetSymbol,
                lt.InitialCapital,
                lt.CurrentCapital,
                lt.TotalReturn,
                lt.Status,
                lt.StartedAt,
                lt.StoppedAt,
                lt.CreatedAt,
                lt.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return liveTrades;
    }
}