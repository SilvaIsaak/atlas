using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Risk.Enums;
using CryptoAIPlatform.Domain.Risk.ValueObjects;

namespace CryptoAIPlatform.Domain.Risk;

public class RiskLimit : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public RiskType Type { get; private set; }
    public decimal Threshold { get; private set; }
    public RiskSeverity Severity { get; private set; }
    public RiskAction Action { get; private set; }
    public bool IsActive { get; private set; }

    private RiskLimit() { }

    public static RiskLimit Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        string name,
        RiskType type,
        decimal threshold,
        RiskSeverity severity,
        RiskAction action,
        bool isActive = true,
        Guid? createdBy = null)
    {
        return new RiskLimit
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Name = name,
            Type = type,
            Threshold = threshold,
            Severity = severity,
            Action = action,
            IsActive = isActive,
            CreatedBy = createdBy
        };
    }
}

public class RiskRule : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Expression { get; private set; } = string.Empty;
    public RiskSeverity Severity { get; private set; }
    public RiskAction Action { get; private set; }
    public bool IsActive { get; private set; }

    private RiskRule() { }

    public static RiskRule Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        string name,
        string expression,
        RiskSeverity severity,
        RiskAction action,
        bool isActive = true,
        Guid? createdBy = null)
    {
        return new RiskRule
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Name = name,
            Expression = expression,
            Severity = severity,
            Action = action,
            IsActive = isActive,
            CreatedBy = createdBy
        };
    }
}
