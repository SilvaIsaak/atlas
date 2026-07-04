using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Trading.Enums;
using CryptoAIPlatform.Domain.Trading.ValueObjects;

namespace CryptoAIPlatform.Domain.Trading;

public class RiskProfile : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public RiskLevel RiskLevel { get; private set; }
    public decimal MaxDrawdown { get; private set; }
    public decimal MaxPositionSize { get; private set; }
    public decimal MaxDailyLoss { get; private set; }
    public Leverage MaxLeverage { get; private set; } = null!;

    private RiskProfile() { }

    public static RiskProfile Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        RiskLevel riskLevel,
        decimal maxDrawdown,
        decimal maxPositionSize,
        decimal maxDailyLoss,
        Leverage maxLeverage,
        Guid? createdBy = null)
    {
        return new RiskProfile
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            RiskLevel = riskLevel,
            MaxDrawdown = maxDrawdown,
            MaxPositionSize = maxPositionSize,
            MaxDailyLoss = maxDailyLoss,
            MaxLeverage = maxLeverage,
            CreatedBy = createdBy
        };
    }
}
