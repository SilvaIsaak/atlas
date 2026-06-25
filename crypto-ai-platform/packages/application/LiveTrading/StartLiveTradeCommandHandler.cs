using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.LiveTrading;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.LiveTrading;

public class StartLiveTradeCommandHandler : IRequestHandler<StartLiveTradeCommand, StartLiveTradeResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public StartLiveTradeCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StartLiveTradeResponse> Handle(StartLiveTradeCommand request, CancellationToken cancellationToken)
    {
        var liveTrade = await _dbContext.LiveTrades
            .FirstOrDefaultAsync(lt => lt.Id == request.LiveTradeId && lt.UserId == request.UserId, cancellationToken);

        if (liveTrade == null)
        {
            throw new KeyNotFoundException("LiveTrade not found");
        }

        liveTrade.Status = LiveTradeStatus.Running;
        liveTrade.StartedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new StartLiveTradeResponse
        {
            LiveTradeId = liveTrade.Id,
            Status = liveTrade.Status.ToString()
        };
    }
}