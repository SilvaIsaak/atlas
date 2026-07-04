using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Risk.Enums;
using CryptoAIPlatform.Domain.Risk.ValueObjects;

namespace CryptoAIPlatform.Domain.Risk;

public class RiskAssessment : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public Guid? OrderId { get; private set; }
    public RiskStatus Status { get; private set; }
    public RiskScore Score { get; private set; } = null!;
    public DateTime AssessedAt { get; private set; }
    public List<RiskViolation> Violations { get; private set; } = new();
    public List<RiskMetric> Metrics { get; private set; } = new();

    private RiskAssessment() { }

    public static RiskAssessment Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        Guid? orderId,
        RiskStatus status,
        RiskScore score,
        DateTime assessedAt,
        Guid? createdBy = null)
    {
        return new RiskAssessment
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            OrderId = orderId,
            Status = status,
            Score = score,
            AssessedAt = assessedAt,
            CreatedBy = createdBy
        };
    }

    public void AddViolation(RiskViolation violation) => Violations.Add(violation);
    public void AddMetric(RiskMetric metric) => Metrics.Add(metric);
}

public class RiskViolation : BaseEntity<Guid>
{
    public Guid AssessmentId { get; private set; }
    public RiskType Type { get; private set; }
    public RiskSeverity Severity { get; private set; }
    public string RuleName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ViolationStatus Status { get; private set; }
    public DateTime DetectedAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    private RiskViolation() { }

    public static RiskViolation Create(
        Guid id,
        TenantId tenantId,
        Guid assessmentId,
        RiskType type,
        RiskSeverity severity,
        string ruleName,
        string description,
        DateTime detectedAt,
        Guid? createdBy = null)
    {
        return new RiskViolation
        {
            Id = id,
            TenantId = tenantId,
            AssessmentId = assessmentId,
            Type = type,
            Severity = severity,
            RuleName = ruleName,
            Description = description,
            Status = ViolationStatus.Open,
            DetectedAt = detectedAt,
            CreatedBy = createdBy
        };
    }

    public void Resolve()
    {
        Status = ViolationStatus.Resolved;
        ResolvedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class RiskMetric : BaseEntity<Guid>
{
    public Guid AssessmentId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public string Unit { get; private set; } = string.Empty;
    public DateTime CalculatedAt { get; private set; }

    private RiskMetric() { }

    public static RiskMetric Create(
        Guid id,
        TenantId tenantId,
        Guid assessmentId,
        string name,
        decimal value,
        string unit,
        DateTime calculatedAt,
        Guid? createdBy = null)
    {
        return new RiskMetric
        {
            Id = id,
            TenantId = tenantId,
            AssessmentId = assessmentId,
            Name = name,
            Value = value,
            Unit = unit,
            CalculatedAt = calculatedAt,
            CreatedBy = createdBy
        };
    }
}
