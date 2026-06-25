using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Strategies;

namespace CryptoAIPlatform.Domain.PaperTrading;

public class PaperTrade : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public Guid StrategyId { get; set; }
    public Strategy? Strategy { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssetSymbol { get; set; } = string.Empty;
    public decimal InitialCapital { get; set; }
    public decimal CurrentCapital { get; set; }
    public PaperTradeStatus Status { get; set; } = PaperTradeStatus.Draft;
    public List<PaperTradeOrder>? Orders { get; set; }
    public decimal? TotalReturn { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? StoppedAt { get; set; }
}