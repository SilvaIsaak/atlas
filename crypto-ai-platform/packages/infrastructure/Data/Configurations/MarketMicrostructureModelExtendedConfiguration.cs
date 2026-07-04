using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class OrderBookLevelConfiguration : IEntityTypeConfiguration<OrderBookLevel>
{
    public void Configure(EntityTypeBuilder<OrderBookLevel> builder)
    {
        builder.ToTable("OrderBookLevels");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class BidAskSpreadConfiguration : IEntityTypeConfiguration<BidAskSpread>
{
    public void Configure(EntityTypeBuilder<BidAskSpread> builder)
    {
        builder.ToTable("BidAskSpreads");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.OwnsOne(x => x.Spread);
    }
}

public class LiquiditySnapshotConfiguration : IEntityTypeConfiguration<LiquiditySnapshot>
{
    public void Configure(EntityTypeBuilder<LiquiditySnapshot> builder)
    {
        builder.ToTable("LiquiditySnapshots");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.OwnsOne(x => x.LiquidityScore);
    }
}

public class MarketImpactConfiguration : IEntityTypeConfiguration<MarketImpact>
{
    public void Configure(EntityTypeBuilder<MarketImpact> builder)
    {
        builder.ToTable("MarketImpacts");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.OwnsOne(x => x.ImpactCost);
    }
}

public class TradeFlowConfiguration : IEntityTypeConfiguration<TradeFlow>
{
    public void Configure(EntityTypeBuilder<TradeFlow> builder)
    {
        builder.ToTable("TradeFlows");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class OrderImbalanceConfiguration : IEntityTypeConfiguration<OrderImbalance>
{
    public void Configure(EntityTypeBuilder<OrderImbalance> builder)
    {
        builder.ToTable("OrderImbalances");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.OwnsOne(x => x.Imbalance);
    }
}

public class VolumeProfileConfiguration : IEntityTypeConfiguration<VolumeProfile>
{
    public void Configure(EntityTypeBuilder<VolumeProfile> builder)
    {
        builder.ToTable("VolumeProfiles");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(x => x.VolumeBuckets).HasJsonConversion();
    }
}

public class VWAPSnapshotConfiguration : IEntityTypeConfiguration<VWAPSnapshot>
{
    public void Configure(EntityTypeBuilder<VWAPSnapshot> builder)
    {
        builder.ToTable("VWAPSnapshots");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class TWAPSnapshotConfiguration : IEntityTypeConfiguration<TWAPSnapshot>
{
    public void Configure(EntityTypeBuilder<TWAPSnapshot> builder)
    {
        builder.ToTable("TWAPSnapshots");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class MarketDepthSnapshotConfiguration : IEntityTypeConfiguration<MarketDepthSnapshot>
{
    public void Configure(EntityTypeBuilder<MarketDepthSnapshot> builder)
    {
        builder.ToTable("MarketDepthSnapshots");
        builder.HasIndex(x => x.TenantId);
        builder.HasQueryFilter(x => x.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(x => x.BidLevels).HasJsonConversion();
        builder.Property(x => x.AskLevels).HasJsonConversion();
    }
}
