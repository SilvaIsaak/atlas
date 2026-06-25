using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.PaperTrading;

public class GetPaperTradeQueryHandler : IRequestHandler<GetPaperTradeQuery, GetPaperTradeResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetPaperTradeQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPaperTradeResponse> Handle(GetPaperTradeQuery request, CancellationToken cancellationToken)
    {
        var paperTrade = await _dbContext.PaperTrades
            .FirstOrDefaultAsync(pt => pt.Id == request.PaperTradeId && pt.UserId == request.UserId, cancellationToken);

        if (paperTrade == null)
        {
            throw new KeyNotFoundException("PaperTrade not found");
        }

        return new GetPaperTradeResponse(
            paperTrade.Id,
            paperTrade.UserId,
            paperTrade.StrategyId,
            paperTrade.Name,
            paperTrade.Description,
            paperTrade.AssetSymbol,
            paperTrade.InitialCapital,
            paperTrade.CurrentCapital,
            paperTrade.Status,
            paperTrade.Orders,
            paperTrade.TotalReturn,
            paperTrade.StartedAt,
            paperTrade.StoppedAt,
            paperTrade.CreatedAt,
            paperTrade.UpdatedAt
        );
    }
}