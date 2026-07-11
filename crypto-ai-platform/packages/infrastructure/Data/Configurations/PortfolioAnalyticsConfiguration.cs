using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.PortfolioAnalytics;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class PortfolioPerformanceReportConfiguration : IEntityTypeConfiguration<PortfolioPerformanceReport>
{
    public void Configure(EntityTypeBuilder<PortfolioPerformanceReport> builder)
    {
        builder.ToTable("PortfolioPerformanceReports");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(x => x.PortfolioId).IsRequired();
        builder.Property(x => x.CalculatedAt).IsRequired();
        builder.Property(x => x.SharpeRatio).HasJsonConversion();
        builder.Property(x => x.SortinoRatio).HasJsonConversion();
        builder.Property(x => x.CalmarRatio).HasJsonConversion();
        builder.Property(x => x.ProfitFactor).HasJsonConversion();
        builder.Property(x => x.WinRate).HasJsonConversion();
        builder.Property(x => x.Volatility).HasJsonConversion();
        builder.Property(x => x.Expectancy).HasJsonConversion();

        builder.HasMany(x => x.Snapshots)
               .WithOne()
               .HasForeignKey("PortfolioPerformanceReportId")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.TenantId, x.PortfolioId });
    }
}

public class PerformanceSnapshotConfiguration : IEntityTypeConfiguration<PerformanceSnapshot>
{
    public void Configure(EntityTypeBuilder<PerformanceSnapshot> builder)
    {
        builder.ToTable("PerformanceSnapshots");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(x => x.PortfolioId).IsRequired();
        builder.Property(x => x.Timestamp).IsRequired();
        builder.Property(x => x.TotalEquity).IsRequired();
        builder.Property(x => x.TotalReturn).IsRequired();
        builder.Property(x => x.DailyReturn).IsRequired();
        builder.Property(x => x.MonthlyReturn).IsRequired();
        builder.Property(x => x.AnnualReturn).IsRequired();

        builder.HasMany(x => x.EquityCurve)
               .WithOne()
               .HasForeignKey(x => x.SnapshotId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Drawdowns)
               .WithOne()
               .HasForeignKey(x => x.SnapshotId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Benchmark)
               .WithOne()
               .HasForeignKey<BenchmarkComparison>(x => x.SnapshotId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.TenantId, x.PortfolioId, x.Timestamp });
    }
}

public class EquityCurvePointConfiguration : IEntityTypeConfiguration<EquityCurvePoint>
{
    public void Configure(EntityTypeBuilder<EquityCurvePoint> builder)
    {
        builder.ToTable("EquityCurvePoints");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(x => x.SnapshotId).IsRequired();
        builder.Property(x => x.Timestamp).IsRequired();
        builder.Property(x => x.Equity).IsRequired();

        builder.HasIndex(x => new { x.TenantId, x.SnapshotId, x.Timestamp });
    }
}

public class DrawdownPointConfiguration : IEntityTypeConfiguration<DrawdownPoint>
{
    public void Configure(EntityTypeBuilder<DrawdownPoint> builder)
    {
        builder.ToTable("DrawdownPoints");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(x => x.SnapshotId).IsRequired();
        builder.Property(x => x.PeakAt).IsRequired();
        builder.Property(x => x.TroughAt).IsRequired();
        builder.Property(x => x.PeakEquity).IsRequired();
        builder.Property(x => x.TroughEquity).IsRequired();
        builder.Property(x => x.DrawdownPercentage).IsRequired();
        builder.Property(x => x.Duration).IsRequired();

        builder.HasIndex(x => new { x.TenantId, x.SnapshotId });
    }
}

public class BenchmarkComparisonConfiguration : IEntityTypeConfiguration<BenchmarkComparison>
{
    public void Configure(EntityTypeBuilder<BenchmarkComparison> builder)
    {
        builder.ToTable("BenchmarkComparisons");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(x => x.SnapshotId).IsRequired();
        builder.Property(x => x.BenchmarkName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.BenchmarkReturn).IsRequired();
        builder.Property(x => x.PortfolioReturn).IsRequired();
        builder.Property(x => x.Alpha).IsRequired();
        builder.Property(x => x.Beta).IsRequired();

        builder.HasIndex(x => new { x.TenantId, x.SnapshotId });
    }
}
