using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.Research;

public class ResearchStudy : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AssetSymbol { get; set; } = string.Empty;
    public List<string> IndicatorsUsed { get; set; } = new List<string>();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ResearchResult? Result { get; set; }
    public DateTime? ExecutedAt { get; set; }
}
