using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Backtesting;
using CryptoAIPlatform.Infrastructure.Data;

namespace CryptoAIPlatform.Application.Backtesting;

public class ExecuteBacktestCommandHandler : IRequestHandler<ExecuteBacktestCommand, ExecuteBacktestResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public ExecuteBacktestCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ExecuteBacktestResponse> Handle(ExecuteBacktestCommand request, CancellationToken cancellationToken)
    {
        var backtest = await _dbContext.Backtests
            .FirstOrDefaultAsync(b => b.Id == request.BacktestId && b.UserId == request.UserId, cancellationToken);

        if (backtest == null)
        {
            throw new KeyNotFoundException("Backtest not found");
        }

        backtest.Status = BacktestStatus.Running;
        backtest.StartedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);

        // Aqui normalmente seria a integração com um serviço de backtesting real, mas por agora vamos simular a conclusão
        backtest.Status = BacktestStatus.Completed;
        backtest.CompletedAt = DateTime.UtcNow;
        backtest.Result = new BacktestResult(
            TotalReturn: 15.5m,
            SharpeRatio: 1.8m,
            SortinoRatio: 2.1m,
            MaxDrawdown: 8.2m,
            NumberOfTrades: 42,
            WinRate: 65.0m,
            ProfitFactor: 1.9m,
            AverageWin: 250.0m,
            AverageLoss: 120.0m,
            AverageTradeDuration: TimeSpan.FromHours(4)
        );

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ExecuteBacktestResponse
        {
            BacktestId = backtest.Id,
            Status = backtest.Status.ToString()
        };
    }
}