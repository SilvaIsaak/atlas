using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Domain.RiskManagement;

public class RiskProfile : BaseEntity<Guid>, IAggregateRoot
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<RiskRule>? Rules { get; set; }
    public List<RiskAlert>? Alerts { get; set; }
    public bool IsActive { get; set; }
}