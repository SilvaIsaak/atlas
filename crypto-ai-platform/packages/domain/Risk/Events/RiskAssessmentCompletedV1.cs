using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Risk.Enums;

namespace CryptoAIPlatform.Domain.Risk.Events;

public class RiskAssessmentCompletedV1 : DomainEvent
{
    public Guid AssessmentId { get; init; }
    public Guid PortfolioId { get; init; }
    public Guid? OrderId { get; init; }
    public RiskAssessmentCompletedV1(TenantId tenantId, Guid assessmentId, Guid portfolioId, Guid? orderId)
    {
        TenantId = tenantId;
        AssessmentId = assessmentId;
        PortfolioId = portfolioId;
        OrderId = orderId;
    }
}

public class RiskLimitExceededV1 : DomainEvent
{
    public Guid RiskLimitId { get; init; }
    public Guid PortfolioId { get; init; }
    public decimal CurrentValue { get; init; }
    public decimal Threshold { get; init; }

    public RiskLimitExceededV1(TenantId tenantId, Guid riskLimitId, Guid portfolioId, decimal currentValue, decimal threshold)
    {
        TenantId = tenantId;
        RiskLimitId = riskLimitId;
        PortfolioId = portfolioId;
        CurrentValue = currentValue;
        Threshold = threshold;
    }
}

public class RiskViolationDetectedV1 : DomainEvent
{
    public Guid ViolationId { get; init; }
    public Guid AssessmentId { get; init; }
    public string RuleName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public RiskViolationDetectedV1(TenantId tenantId, Guid violationId, Guid assessmentId, string ruleName, string description)
    {
        TenantId = tenantId;
        ViolationId = violationId;
        AssessmentId = assessmentId;
        RuleName = ruleName;
        Description = description;
    }
}

public class PositionRejectedByRiskV1 : DomainEvent
{
    public Guid OrderId { get; init; }
    public Guid PortfolioId { get; init; }

    public PositionRejectedByRiskV1(TenantId tenantId, Guid orderId, Guid portfolioId)
    {
        TenantId = tenantId;
        OrderId = orderId;
        PortfolioId = portfolioId;
    }
}

public class MarginCallTriggeredV1 : DomainEvent
{
    public Guid PortfolioId { get; init; }
    public decimal MarginUsage { get; init; }

    public MarginCallTriggeredV1(TenantId tenantId, Guid portfolioId, decimal marginUsage)
    {
        TenantId = tenantId;
        PortfolioId = portfolioId;
        MarginUsage = marginUsage;
    }
}

public class LiquidationTriggeredV1 : DomainEvent
{
    public Guid PositionId { get; init; }
    public decimal LiquidationPrice { get; init; }

    public LiquidationTriggeredV1(TenantId tenantId, Guid positionId, decimal liquidationPrice)
    {
        TenantId = tenantId;
        PositionId = positionId;
        LiquidationPrice = liquidationPrice;
    }
}

public class PortfolioRiskUpdatedV1 : DomainEvent
{
    public Guid SnapshotId { get; init; }
    public Guid PortfolioId { get; init; }
    public RiskStatus Status { get; init; }

    public PortfolioRiskUpdatedV1(TenantId tenantId, Guid snapshotId, Guid portfolioId, RiskStatus status)
    {
        TenantId = tenantId;
        SnapshotId = snapshotId;
        PortfolioId = portfolioId;
        Status = status;
    }
}
