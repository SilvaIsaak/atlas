using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.PortfolioAnalytics.ValueObjects;

namespace CryptoAIPlatform.Domain.PortfolioAnalytics;

public class PortfolioPerformanceReport : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public DateTime CalculatedAt { get; private set; }
    public SharpeRatio? SharpeRatio { get; private set; }
    public SortinoRatio? SortinoRatio { get; private set; }
    public CalmarRatio? CalmarRatio { get; private set; }
    public ProfitFactor? ProfitFactor { get; private set; }
    public WinRate? WinRate { get; private set; }
    public Volatility? Volatility { get; private set; }
    public Expectancy? Expectancy { get; private set; }

    public List<PerformanceSnapshot> Snapshots { get; private set; } = new();

    private PortfolioPerformanceReport() { }

    public static PortfolioPerformanceReport Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        DateTime calculatedAt,
        Guid? createdBy = null)
    {
        return new PortfolioPerformanceReport
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            CalculatedAt = calculatedAt,
            CreatedBy = createdBy
        };
    }

    public void UpdateMetrics(
        SharpeRatio? sharpeRatio,
        SortinoRatio? sortinoRatio,
        CalmarRatio? calmarRatio,
        ProfitFactor? profitFactor,
        WinRate? winRate,
        Volatility? volatility,
        Expectancy? expectancy)
    {
        SharpeRatio = sharpeRatio;
        SortinoRatio = sortinoRatio;
        CalmarRatio = calmarRatio;
        ProfitFactor = profitFactor;
        WinRate = winRate;
        Volatility = volatility;
        Expectancy = expectancy;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class PerformanceSnapshot : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public decimal TotalEquity { get; private set; }
    public decimal TotalReturn { get; private set; }
    public decimal DailyReturn { get; private set; }
    public decimal MonthlyReturn { get; private set; }
    public decimal AnnualReturn { get; private set; }

    public List<EquityCurvePoint> EquityCurve { get; private set; } = new();
    public List<DrawdownPoint> Drawdowns { get; private set; } = new();
    public BenchmarkComparison? Benchmark { get; private set; }

    private PerformanceSnapshot() { }

    public static PerformanceSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        DateTime timestamp,
        decimal totalEquity,
        decimal totalReturn,
        decimal dailyReturn,
        decimal monthlyReturn,
        decimal annualReturn,
        Guid? createdBy = null)
    {
        return new PerformanceSnapshot
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Timestamp = timestamp,
            TotalEquity = totalEquity,
            TotalReturn = totalReturn,
            DailyReturn = dailyReturn,
            MonthlyReturn = monthlyReturn,
            AnnualReturn = annualReturn,
            CreatedBy = createdBy
        };
    }
}

public class EquityCurvePoint : BaseEntity<Guid>
{
    public Guid SnapshotId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public decimal Equity { get; private set; }

    private EquityCurvePoint() { }

    public static EquityCurvePoint Create(
        Guid id,
        TenantId tenantId,
        Guid snapshotId,
        DateTime timestamp,
        decimal equity,
        Guid? createdBy = null)
    {
        return new EquityCurvePoint
        {
            Id = id,
            TenantId = tenantId,
            SnapshotId = snapshotId,
            Timestamp = timestamp,
            Equity = equity,
            CreatedBy = createdBy
        };
    }
}

public class DrawdownPoint : BaseEntity<Guid>
{
    public Guid SnapshotId { get; private set; }
    public DateTime PeakAt { get; private set; }
    public DateTime TroughAt { get; private set; }
    public decimal PeakEquity { get; private set; }
    public decimal TroughEquity { get; private set; }
    public decimal DrawdownPercentage { get; private set; }
    public TimeSpan Duration { get; private set; }

    private DrawdownPoint() { }

    public static DrawdownPoint Create(
        Guid id,
        TenantId tenantId,
        Guid snapshotId,
        DateTime peakAt,
        DateTime troughAt,
        decimal peakEquity,
        decimal troughEquity,
        decimal drawdownPercentage,
        TimeSpan duration,
        Guid? createdBy = null)
    {
        return new DrawdownPoint
        {
            Id = id,
            TenantId = tenantId,
            SnapshotId = snapshotId,
            PeakAt = peakAt,
            TroughAt = troughAt,
            PeakEquity = peakEquity,
            TroughEquity = troughEquity,
            DrawdownPercentage = drawdownPercentage,
            Duration = duration,
            CreatedBy = createdBy
        };
    }
}

public class BenchmarkComparison : BaseEntity<Guid>
{
    public Guid SnapshotId { get; private set; }
    public string BenchmarkName { get; private set; } = string.Empty;
    public decimal BenchmarkReturn { get; private set; }
    public decimal PortfolioReturn { get; private set; }
    public decimal Alpha { get; private set; }
    public decimal Beta { get; private set; }

    private BenchmarkComparison() { }

    public static BenchmarkComparison Create(
        Guid id,
        TenantId tenantId,
        Guid snapshotId,
        string benchmarkName,
        decimal benchmarkReturn,
        decimal portfolioReturn,
        decimal alpha,
        decimal beta,
        Guid? createdBy = null)
    {
        return new BenchmarkComparison
        {
            Id = id,
            TenantId = tenantId,
            SnapshotId = snapshotId,
            BenchmarkName = benchmarkName,
            BenchmarkReturn = benchmarkReturn,
            PortfolioReturn = portfolioReturn,
            Alpha = alpha,
            Beta = beta,
            CreatedBy = createdBy
        };
    }
}
