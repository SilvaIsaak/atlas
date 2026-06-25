using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Strategies;

public class Strategy : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Código da estratégia (ex: C#, Python, etc.)
    public string AssetSymbol { get; set; } = string.Empty;
    public StrategyStatus Status { get; set; } = StrategyStatus.Draft;
    public Guid? ResearchStudyId { get; set; } // Relacionamento opcional com o Research Engine
}
