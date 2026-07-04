using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Risk;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class RiskAssessmentEntityConfiguration : IEntityTypeConfiguration<RiskAssessment>
{
    public void Configure(EntityTypeBuilder<RiskAssessment> builder)
    {
        builder.ToTable("RiskAssessments");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(a => a.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(a => a.PortfolioId).IsRequired();
        builder.Property(a => a.OrderId);
        builder.Property(a => a.Status).IsRequired();
        builder.Property(a => a.Score).HasJsonConversion().IsRequired();
        builder.Property(a => a.AssessedAt).IsRequired();

        builder.HasMany(a => a.Violations)
            .WithOne()
            .HasForeignKey(v => v.AssessmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Metrics)
            .WithOne()
            .HasForeignKey(m => m.AssessmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => new { a.TenantId, a.PortfolioId });
    }
}

public class RiskViolationEntityConfiguration : IEntityTypeConfiguration<RiskViolation>
{
    public void Configure(EntityTypeBuilder<RiskViolation> builder)
    {
        builder.ToTable("RiskViolations");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(v => v.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(v => v.AssessmentId).IsRequired();
        builder.Property(v => v.Type).IsRequired();
        builder.Property(v => v.Severity).IsRequired();
        builder.Property(v => v.RuleName).IsRequired().HasMaxLength(200);
        builder.Property(v => v.Description).HasMaxLength(1000);
        builder.Property(v => v.Status).IsRequired();
        builder.Property(v => v.DetectedAt).IsRequired();
        builder.Property(v => v.ResolvedAt);

        builder.HasIndex(v => new { v.TenantId, v.AssessmentId, v.Status });
    }
}

public class RiskMetricEntityConfiguration : IEntityTypeConfiguration<RiskMetric>
{
    public void Configure(EntityTypeBuilder<RiskMetric> builder)
    {
        builder.ToTable("RiskMetrics");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(m => m.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(m => m.AssessmentId).IsRequired();
        builder.Property(m => m.Name).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Value).IsRequired();
        builder.Property(m => m.Unit).IsRequired().HasMaxLength(50);
        builder.Property(m => m.CalculatedAt).IsRequired();

        builder.HasIndex(m => new { m.TenantId, m.AssessmentId, m.Name });
    }
}

public class RiskLimitEntityConfiguration : IEntityTypeConfiguration<RiskLimit>
{
    public void Configure(EntityTypeBuilder<RiskLimit> builder)
    {
        builder.ToTable("RiskLimits");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(l => l.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(l => l.PortfolioId).IsRequired();
        builder.Property(l => l.Name).IsRequired().HasMaxLength(200);
        builder.Property(l => l.Type).IsRequired();
        builder.Property(l => l.Threshold).IsRequired();
        builder.Property(l => l.Severity).IsRequired();
        builder.Property(l => l.Action).IsRequired();
        builder.Property(l => l.IsActive).IsRequired();

        builder.HasIndex(l => new { l.TenantId, l.PortfolioId, l.Type });
    }
}

public class RiskRuleEntityConfiguration : IEntityTypeConfiguration<RiskRule>
{
    public void Configure(EntityTypeBuilder<RiskRule> builder)
    {
        builder.ToTable("RiskRules");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(r => r.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(r => r.PortfolioId).IsRequired();
        builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Expression).IsRequired().HasMaxLength(1000);
        builder.Property(r => r.Severity).IsRequired();
        builder.Property(r => r.Action).IsRequired();
        builder.Property(r => r.IsActive).IsRequired();

        builder.HasIndex(r => new { r.TenantId, r.PortfolioId, r.IsActive });
    }
}

public class ExposureProfileEntityConfiguration : IEntityTypeConfiguration<ExposureProfile>
{
    public void Configure(EntityTypeBuilder<ExposureProfile> builder)
    {
        builder.ToTable("ExposureProfiles");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(e => e.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(e => e.PortfolioId).IsRequired();
        builder.Property(e => e.GeneratedAt).IsRequired();
        builder.Property(e => e.TotalExposure).HasJsonConversion().IsRequired();
        builder.Property(e => e.HighestConcentration).HasJsonConversion();

        builder.HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey(i => i.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.TenantId, e.PortfolioId });
    }
}

public class ExposureItemEntityConfiguration : IEntityTypeConfiguration<ExposureItem>
{
    public void Configure(EntityTypeBuilder<ExposureItem> builder)
    {
        builder.ToTable("ExposureItems");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(i => i.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(i => i.ProfileId).IsRequired();
        builder.Property(i => i.Symbol).IsRequired().HasMaxLength(100);
        builder.Property(i => i.Type).IsRequired();
        builder.Property(i => i.Size).IsRequired();
        builder.Property(i => i.NotionalValue).IsRequired();
        builder.Property(i => i.ConcentrationPercentage).IsRequired();

        builder.HasIndex(i => new { i.TenantId, i.ProfileId, i.Symbol });
    }
}

public class PortfolioRiskSnapshotEntityConfiguration : IEntityTypeConfiguration<PortfolioRiskSnapshot>
{
    public void Configure(EntityTypeBuilder<PortfolioRiskSnapshot> builder)
    {
        builder.ToTable("PortfolioRiskSnapshots");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(p => p.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(p => p.PortfolioId).IsRequired();
        builder.Property(p => p.Timestamp).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.MarginUsage).HasJsonConversion().IsRequired();
        builder.Property(p => p.Leverage).HasJsonConversion().IsRequired();
        builder.Property(p => p.VaR).HasJsonConversion();
        builder.Property(p => p.ExpectedShortfall).HasJsonConversion();

        builder.HasMany(p => p.VaRHistory)
            .WithOne()
            .HasForeignKey(v => v.SnapshotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.StressResults)
            .WithOne()
            .HasForeignKey(s => s.SnapshotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Drawdown)
            .WithOne()
            .HasForeignKey<DrawdownSnapshot>(d => d.SnapshotId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => new { p.TenantId, p.PortfolioId });
    }
}

public class VaRSnapshotEntityConfiguration : IEntityTypeConfiguration<VaRSnapshot>
{
    public void Configure(EntityTypeBuilder<VaRSnapshot> builder)
    {
        builder.ToTable("VaRSnapshots");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(v => v.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(v => v.SnapshotId).IsRequired();
        builder.Property(v => v.Value).HasJsonConversion().IsRequired();
        builder.Property(v => v.Timestamp).IsRequired();

        builder.HasIndex(v => new { v.TenantId, v.SnapshotId });
    }
}

public class StressScenarioResultEntityConfiguration : IEntityTypeConfiguration<StressScenarioResult>
{
    public void Configure(EntityTypeBuilder<StressScenarioResult> builder)
    {
        builder.ToTable("StressScenarioResults");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(s => s.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(s => s.SnapshotId).IsRequired();
        builder.Property(s => s.ScenarioName).IsRequired().HasMaxLength(200);
        builder.Property(s => s.PnL).IsRequired();
        builder.Property(s => s.PnLPercentage).IsRequired();
        builder.Property(s => s.RanAt);

        builder.HasIndex(s => new { s.TenantId, s.SnapshotId });
    }
}

public class DrawdownSnapshotEntityConfiguration : IEntityTypeConfiguration<DrawdownSnapshot>
{
    public void Configure(EntityTypeBuilder<DrawdownSnapshot> builder)
    {
        builder.ToTable("DrawdownSnapshots");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(d => d.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(d => d.SnapshotId).IsRequired();
        builder.Property(d => d.Value).IsRequired();
        builder.Property(d => d.Percentage).IsRequired();
        builder.Property(d => d.PeakAt).IsRequired();
        builder.Property(d => d.TroughAt).IsRequired();

        builder.HasIndex(d => new { d.TenantId, d.SnapshotId });
    }
}

public class MarginRequirementEntityConfiguration : IEntityTypeConfiguration<MarginRequirement>
{
    public void Configure(EntityTypeBuilder<MarginRequirement> builder)
    {
        builder.ToTable("MarginRequirements");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(m => m.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(m => m.PortfolioId).IsRequired();
        builder.Property(m => m.Type).IsRequired();
        builder.Property(m => m.InitialMargin).HasJsonConversion().IsRequired();
        builder.Property(m => m.MaintenanceMargin).HasJsonConversion().IsRequired();
        builder.Property(m => m.CalculatedAt).IsRequired();

        builder.HasIndex(m => new { m.TenantId, m.PortfolioId, m.Type });
    }
}

public class LiquidationLevelEntityConfiguration : IEntityTypeConfiguration<LiquidationLevel>
{
    public void Configure(EntityTypeBuilder<LiquidationLevel> builder)
    {
        builder.ToTable("LiquidationLevels");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(l => l.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(l => l.PositionId).IsRequired();
        builder.Property(l => l.Price).HasJsonConversion().IsRequired();
        builder.Property(l => l.CalculatedAt).IsRequired();

        builder.HasIndex(l => new { l.TenantId, l.PositionId });
    }
}
