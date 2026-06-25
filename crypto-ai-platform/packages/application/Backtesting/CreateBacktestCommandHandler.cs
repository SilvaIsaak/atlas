using MediatR;
using CryptoAIPlatform.Domain.Backtesting;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Backtesting;

public class CreateBacktestCommandHandler : IRequestHandler<CreateBacktestCommand, CreateBacktestResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateBacktestCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateBacktestResponse> Handle(CreateBacktestCommand request, CancellationToken cancellationToken)
    {
        var backtest = new Backtest
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            StrategyId = request.StrategyId,
            Name = request.Name,
            Description = request.Description,
            AssetSymbol = request.AssetSymbol,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            InitialCapital = request.InitialCapital,
            Status = BacktestStatus.Pending
        };

        await _dbContext.Backtests.AddAsync(backtest, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateBacktestResponse
        {
            BacktestId = backtest.Id,
            StrategyId = backtest.StrategyId,
            Name = backtest.Name,
            Description = backtest.Description,
            AssetSymbol = backtest.AssetSymbol,
            StartDate = backtest.StartDate,
            EndDate = backtest.EndDate,
            InitialCapital = backtest.InitialCapital,
            Status = backtest.Status
        };
    }
}