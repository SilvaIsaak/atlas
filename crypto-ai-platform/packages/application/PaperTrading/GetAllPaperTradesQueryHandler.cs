using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.PaperTrading;

public class GetAllPaperTradesQueryHandler : IRequestHandler<GetAllPaperTradesQuery, List<GetPaperTradeResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllPaperTradesQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetPaperTradeResponse>> Handle(GetAllPaperTradesQuery request, CancellationToken cancellationToken)
    {
        var paperTrades = await _dbContext.PaperTrades
            .Where(pt => pt.UserId == request.UserId)
            .OrderByDescending(pt => pt.CreatedAt)
            .Select(pt => new GetPaperTradeResponse(
                pt.Id,
                pt.UserId,
                pt.StrategyId,
                pt.Name,
                pt.Description,
                pt.AssetSymbol,
                pt.InitialCapital,
                pt.CurrentCapital,
                pt.Status,
                pt.Orders,
                pt.TotalReturn,
                pt.StartedAt,
                pt.StoppedAt,
                pt.CreatedAt,
                pt.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return paperTrades;
    }
}