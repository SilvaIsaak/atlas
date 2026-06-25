using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.LiveTrading;

public class GetLiveTradeQueryHandler : IRequestHandler<GetLiveTradeQuery, GetLiveTradeResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetLiveTradeQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetLiveTradeResponse> Handle(GetLiveTradeQuery request, CancellationToken cancellationToken)
    {
        var liveTrade = await _dbContext.LiveTrades
            .FirstOrDefaultAsync(lt => lt.Id == request.LiveTradeId && lt.UserId == request.UserId, cancellationToken);

        if (liveTrade == null)
        {
            throw new KeyNotFoundException("LiveTrade not found");
        }

        return new GetLiveTradeResponse(
            liveTrade.Id,
            liveTrade.UserId,
            liveTrade.StrategyId,
            liveTrade.ExecutionEngineId,
            liveTrade.Name,
            liveTrade.AssetSymbol,
            liveTrade.InitialCapital,
            liveTrade.CurrentCapital,
            liveTrade.TotalReturn,
            liveTrade.Status,
            liveTrade.StartedAt,
            liveTrade.StoppedAt,
            liveTrade.CreatedAt,
            liveTrade.UpdatedAt
        );
    }
}