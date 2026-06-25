using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;
using CryptoAIPlatform.Domain.Strategies;
using CryptoAIPlatform.Domain.Backtesting;
using CryptoAIPlatform.Domain.WalkForward;
using CryptoAIPlatform.Domain.PaperTrading;
using CryptoAIPlatform.Domain.LiveTrading;
using CryptoAIPlatform.Domain.Notifications;

namespace CryptoAIPlatform.Application.Dashboard;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, GetDashboardResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetDashboardQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetDashboardResponse> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var totalStrategies = await _dbContext.Strategies
            .CountAsync(s => s.UserId == request.UserId, cancellationToken);

        var totalBacktests = await _dbContext.Backtests
            .CountAsync(b => b.UserId == request.UserId, cancellationToken);

        var totalWalkForwards = await _dbContext.WalkForwards
            .CountAsync(w => w.UserId == request.UserId, cancellationToken);

        var activePaperTrades = await _dbContext.PaperTrades
            .CountAsync(p => p.UserId == request.UserId && p.Status == PaperTradeStatus.Active, cancellationToken);

        var activeLiveTrades = await _dbContext.LiveTrades
            .CountAsync(l => l.UserId == request.UserId && l.Status == LiveTradeStatus.Running, cancellationToken);

        var unreadNotifications = await _dbContext.Notifications
            .CountAsync(n => n.UserId == request.UserId && !n.IsRead, cancellationToken);

        var totalPaperReturn = await _dbContext.PaperTrades
            .Where(p => p.UserId == request.UserId && p.TotalReturn.HasValue)
            .SumAsync(p => p.TotalReturn, cancellationToken);

        var totalLiveReturn = await _dbContext.LiveTrades
            .Where(l => l.UserId == request.UserId && l.TotalReturn.HasValue)
            .SumAsync(l => l.TotalReturn, cancellationToken);

        return new GetDashboardResponse(
            TotalStrategies: totalStrategies,
            TotalBacktests: totalBacktests,
            TotalWalkForwards: totalWalkForwards,
            ActivePaperTrades: activePaperTrades,
            ActiveLiveTrades: activeLiveTrades,
            UnreadNotifications: unreadNotifications,
            TotalPaperReturn: totalPaperReturn,
            TotalLiveReturn: totalLiveReturn
        );
    }
}
