using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Strategies;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class StrategyConfiguration : IEntityTypeConfiguration<Strategy>
{
    public void Configure(EntityTypeBuilder<Strategy> builder)
    {
        builder.ToTable("Strategies");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.Name });
    }
}

public class StrategyVersionConfiguration : IEntityTypeConfiguration<StrategyVersion>
{
    public void Configure(EntityTypeBuilder<StrategyVersion> builder)
    {
        builder.ToTable("StrategyVersions");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(x => x.StrategyId).IsRequired();
        builder.Property(x => x.Version).HasMaxLength(50).IsRequired();
        builder.Property(x => x.ConfigJson).HasMaxLength(5000);
        builder.Property(x => x.DeployedAt).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.StrategyId });
    }
}

public class StrategyExecutionConfiguration : IEntityTypeConfiguration<StrategyExecution>
{
    public void Configure(EntityTypeBuilder<StrategyExecution> builder)
    {
        builder.ToTable("StrategyExecutions");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(x => x.StrategyId).IsRequired();
        builder.Property(x => x.StartedAt).IsRequired();
        builder.Property(x => x.Status).HasMaxLength(50).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.StrategyId });
    }
}

public class StrategyResultConfiguration : IEntityTypeConfiguration<StrategyResult>
{
    public void Configure(EntityTypeBuilder<StrategyResult> builder)
    {
        builder.ToTable("StrategyResults");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(x => x.ExecutionId).IsRequired();
        builder.Property(x => x.TotalReturn).IsRequired();
        builder.Property(x => x.TradesCount).IsRequired();
        builder.Property(x => x.CalculatedAt).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.ExecutionId });
    }
}

public class StrategySignalConfiguration : IEntityTypeConfiguration<StrategySignal>
{
    public void Configure(EntityTypeBuilder<StrategySignal> builder)
    {
        builder.ToTable("StrategySignals");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(x => x.StrategyId).IsRequired();
        builder.Property(x => x.Symbol).HasMaxLength(50).IsRequired();
        builder.Property(x => x.SignalType).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.GeneratedAt).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.StrategyId, x.GeneratedAt });
    }
}
