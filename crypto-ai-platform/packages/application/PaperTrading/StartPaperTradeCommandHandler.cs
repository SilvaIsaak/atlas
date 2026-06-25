using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.PaperTrading;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.PaperTrading;

public class StartPaperTradeCommandHandler : IRequestHandler<StartPaperTradeCommand, StartPaperTradeResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public StartPaperTradeCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StartPaperTradeResponse> Handle(StartPaperTradeCommand request, CancellationToken cancellationToken)
    {
        var paperTrade = await _dbContext.PaperTrades
            .FirstOrDefaultAsync(pt => pt.Id == request.PaperTradeId && pt.UserId == request.UserId, cancellationToken);

        if (paperTrade == null)
        {
            throw new KeyNotFoundException("PaperTrade not found");
        }

        paperTrade.Status = PaperTradeStatus.Active;
        paperTrade.StartedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new StartPaperTradeResponse
        {
            PaperTradeId = paperTrade.Id,
            Status = paperTrade.Status.ToString()
        };
    }
}