using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Strategies;
using CryptoAIPlatform.Domain.Execution;

namespace CryptoAIPlatform.Domain.LiveTrading;

public class LiveTrade : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public Guid StrategyId { get; set; }
    public Strategy? Strategy { get; set; }
    public Guid ExecutionEngineId { get; set; }
    public ExecutionEngine? ExecutionEngine { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AssetSymbol { get; set; } = string.Empty;
    public decimal InitialCapital { get; set; }
    public decimal? CurrentCapital { get; set; }
    public decimal? TotalReturn { get; set; }
    public LiveTradeStatus Status { get; set; } = LiveTradeStatus.Draft;
    public DateTime? StartedAt { get; set; }
    public DateTime? StoppedAt { get; set; }
}