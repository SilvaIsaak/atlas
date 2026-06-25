using MediatR;
using CryptoAIPlatform.Domain.LiveTrading;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.LiveTrading;

public class CreateLiveTradeCommandHandler : IRequestHandler<CreateLiveTradeCommand, CreateLiveTradeResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateLiveTradeCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateLiveTradeResponse> Handle(CreateLiveTradeCommand request, CancellationToken cancellationToken)
    {
        var liveTrade = new LiveTrade
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            StrategyId = request.StrategyId,
            ExecutionEngineId = request.ExecutionEngineId,
            Name = request.Name,
            AssetSymbol = request.AssetSymbol,
            InitialCapital = request.InitialCapital,
            CurrentCapital = request.InitialCapital,
            Status = LiveTradeStatus.Draft
        };

        await _dbContext.LiveTrades.AddAsync(liveTrade, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateLiveTradeResponse
        {
            LiveTradeId = liveTrade.Id,
            Status = liveTrade.Status
        };
    }
}