using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.AIDecision;

public class AIDecision : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public Guid StrategyId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public AIDecisionType DecisionType { get; set; }
    public decimal Confidence { get; set; } // 0 to 1
    public string Reasoning { get; set; } = string.Empty;
    public decimal? SuggestedQuantity { get; set; }
    public decimal? SuggestedPrice { get; set; }
    public bool Executed { get; set; }
    public DateTime? ExecutedAt { get; set; }
}