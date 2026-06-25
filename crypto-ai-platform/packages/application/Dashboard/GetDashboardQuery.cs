using MediatR;
using CryptoAIPlatform.Domain.LiveTrading;
using CryptoAIPlatform.Domain.PaperTrading;

namespace CryptoAIPlatform.Application.Dashboard;

public record GetDashboardQuery : IRequest<GetDashboardResponse>
{
    public Guid UserId { get; init; }
}

public record GetDashboardResponse(
    int TotalStrategies,
    int TotalBacktests,
    int TotalWalkForwards,
    int ActivePaperTrades,
    int ActiveLiveTrades,
    int UnreadNotifications,
    decimal? TotalPaperReturn,
    decimal? TotalLiveReturn
);
