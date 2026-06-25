using MediatR;
using CryptoAIPlatform.Domain.PaperTrading;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.PaperTrading;

public class CreatePaperTradeCommandHandler : IRequestHandler<CreatePaperTradeCommand, CreatePaperTradeResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreatePaperTradeCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatePaperTradeResponse> Handle(CreatePaperTradeCommand request, CancellationToken cancellationToken)
    {
        var paperTrade = new PaperTrade
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            StrategyId = request.StrategyId,
            Name = request.Name,
            Description = request.Description,
            AssetSymbol = request.AssetSymbol,
            InitialCapital = request.InitialCapital,
            CurrentCapital = request.InitialCapital,
            Status = PaperTradeStatus.Draft
        };

        await _dbContext.PaperTrades.AddAsync(paperTrade, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreatePaperTradeResponse
        {
            PaperTradeId = paperTrade.Id,
            Name = paperTrade.Name,
            Status = paperTrade.Status
        };
    }
}