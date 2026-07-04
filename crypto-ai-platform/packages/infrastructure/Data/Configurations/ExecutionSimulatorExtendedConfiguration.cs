using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.ValueObjects;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExecutionFeeConfiguration : IEntityTypeConfiguration<ExecutionFee>
{
    public void Configure(EntityTypeBuilder<ExecutionFee> builder)
    {
        builder.ToTable("ExecutionFees");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.OwnsOne(x => x.Fee);
    }
}

public class ExecutionLatencyConfiguration : IEntityTypeConfiguration<ExecutionLatency>
{
    public void Configure(EntityTypeBuilder<ExecutionLatency> builder)
    {
        builder.ToTable("ExecutionLatencies");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.OwnsOne(x => x.Latency);
    }
}

public class ExecutionSlippageConfiguration : IEntityTypeConfiguration<ExecutionSlippage>
{
    public void Configure(EntityTypeBuilder<ExecutionSlippage> builder)
    {
        builder.ToTable("ExecutionSlippages");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.OwnsOne(x => x.Slippage);
    }
}

public class ExecutionStatisticsConfiguration : IEntityTypeConfiguration<ExecutionStatistics>
{
    public void Configure(EntityTypeBuilder<ExecutionStatistics> builder)
    {
        builder.ToTable("ExecutionStatistics");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class ExecutionTimelineConfiguration : IEntityTypeConfiguration<ExecutionTimeline>
{
    public void Configure(EntityTypeBuilder<ExecutionTimeline> builder)
    {
        builder.ToTable("ExecutionTimelines");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(x => x.Events).HasJsonConversion();
    }
}
