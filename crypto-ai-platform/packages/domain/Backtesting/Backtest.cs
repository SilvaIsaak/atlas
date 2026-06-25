using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Strategies;

namespace CryptoAIPlatform.Domain.Backtesting;

public class Backtest : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public Guid StrategyId { get; set; }
    public Strategy? Strategy { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssetSymbol { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal InitialCapital { get; set; }
    public BacktestStatus Status { get; set; } = BacktestStatus.Pending;
    public BacktestResult? Result { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}