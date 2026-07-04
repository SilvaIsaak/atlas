using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Risk.Enums;
using CryptoAIPlatform.Domain.Risk.ValueObjects;

namespace CryptoAIPlatform.Domain.Risk;

public class PortfolioRiskSnapshot : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public RiskStatus Status { get; private set; }
    public MarginUsage MarginUsage { get; private set; } = null!;
    public PortfolioLeverage Leverage { get; private set; } = null!;
    public VaRValue? VaR { get; private set; }
    public ExpectedShortfall? ExpectedShortfall { get; private set; }
    public DrawdownSnapshot? Drawdown { get; private set; }
    public List<VaRSnapshot> VaRHistory { get; private set; } = new();
    public List<StressScenarioResult> StressResults { get; private set; } = new();

    private PortfolioRiskSnapshot() { }

    public static PortfolioRiskSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        DateTime timestamp,
        RiskStatus status,
        MarginUsage marginUsage,
        PortfolioLeverage leverage,
        VaRValue? var,
        ExpectedShortfall? expectedShortfall,
        DrawdownSnapshot? drawdown,
        Guid? createdBy = null)
    {
        return new PortfolioRiskSnapshot
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Timestamp = timestamp,
            Status = status,
            MarginUsage = marginUsage,
            Leverage = leverage,
            VaR = var,
            ExpectedShortfall = expectedShortfall,
            Drawdown = drawdown,
            CreatedBy = createdBy
        };
    }

    public void AddVaRSnapshot(VaRSnapshot snapshot) => VaRHistory.Add(snapshot);
    public void AddStressResult(StressScenarioResult result) => StressResults.Add(result);
}

public class DrawdownSnapshot : BaseEntity<Guid>
{
    public Guid SnapshotId { get; private set; }
    public decimal Value { get; private set; }
    public decimal Percentage { get; private set; }
    public DateTime PeakAt { get; private set; }
    public DateTime TroughAt { get; private set; }

    private DrawdownSnapshot() { }

    public static DrawdownSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid snapshotId,
        decimal value,
        decimal percentage,
        DateTime peakAt,
        DateTime troughAt,
        Guid? createdBy = null)
    {
        return new DrawdownSnapshot
        {
            Id = id,
            TenantId = tenantId,
            SnapshotId = snapshotId,
            Value = value,
            Percentage = percentage,
            PeakAt = peakAt,
            TroughAt = troughAt,
            CreatedBy = createdBy
        };
    }
}

public class VaRSnapshot : BaseEntity<Guid>
{
    public Guid SnapshotId { get; private set; }
    public VaRValue Value { get; private set; } = null!;
    public DateTime Timestamp { get; private set; }

    private VaRSnapshot() { }

    public static VaRSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid snapshotId,
        VaRValue value,
        DateTime timestamp,
        Guid? createdBy = null)
    {
        return new VaRSnapshot
        {
            Id = id,
            TenantId = tenantId,
            SnapshotId = snapshotId,
            Value = value,
            Timestamp = timestamp,
            CreatedBy = createdBy
        };
    }
}

public class StressScenarioResult : BaseEntity<Guid>
{
    public Guid SnapshotId { get; private set; }
    public string ScenarioName { get; private set; } = string.Empty;
    public decimal PnL { get; private set; }
    public decimal PnLPercentage { get; private set; }
    public DateTime? RanAt { get; private set; }

    private StressScenarioResult() { }

    public static StressScenarioResult Create(
        Guid id,
        TenantId tenantId,
        Guid snapshotId,
        string scenarioName,
        decimal pnl,
        decimal pnlPercentage,
        DateTime? ranAt,
        Guid? createdBy = null)
    {
        return new StressScenarioResult
        {
            Id = id,
            TenantId = tenantId,
            SnapshotId = snapshotId,
            ScenarioName = scenarioName,
            PnL = pnl,
            PnLPercentage = pnlPercentage,
            RanAt = ranAt,
            CreatedBy = createdBy
        };
    }
}

public class MarginRequirement : BaseEntity<Guid>
{
    public Guid PortfolioId { get; private set; }
    public MarginType Type { get; private set; }
    public InitialMargin InitialMargin { get; private set; } = null!;
    public MaintenanceMargin MaintenanceMargin { get; private set; } = null!;
    public DateTime CalculatedAt { get; private set; }

    private MarginRequirement() { }

    public static MarginRequirement Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        MarginType type,
        InitialMargin initialMargin,
        MaintenanceMargin maintenanceMargin,
        DateTime calculatedAt,
        Guid? createdBy = null)
    {
        return new MarginRequirement
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Type = type,
            InitialMargin = initialMargin,
            MaintenanceMargin = maintenanceMargin,
            CalculatedAt = calculatedAt,
            CreatedBy = createdBy
        };
    }
}

public class LiquidationLevel : BaseEntity<Guid>
{
    public Guid PositionId { get; private set; }
    public LiquidationPrice Price { get; private set; } = null!;
    public DateTime CalculatedAt { get; private set; }

    private LiquidationLevel() { }

    public static LiquidationLevel Create(
        Guid id,
        TenantId tenantId,
        Guid positionId,
        LiquidationPrice price,
        DateTime calculatedAt,
        Guid? createdBy = null)
    {
        return new LiquidationLevel
        {
            Id = id,
            TenantId = tenantId,
            PositionId = positionId,
            Price = price,
            CalculatedAt = calculatedAt,
            CreatedBy = createdBy
        };
    }
}
