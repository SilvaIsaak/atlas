using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Strategies;

namespace CryptoAIPlatform.Domain.WalkForward;

public class WalkForward : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public Guid StrategyId { get; set; }
    public Strategy? Strategy { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssetSymbol { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TrainingWindowDays { get; set; }
    public int TestingWindowDays { get; set; }
    public decimal InitialCapital { get; set; }
    public WalkForwardStatus Status { get; set; } = WalkForwardStatus.Pending;
    public List<WalkForwardWindowResult>? WindowResults { get; set; }
    public decimal? TotalOutOfSampleReturn { get; set; }
    public decimal? AverageSharpeRatio { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}