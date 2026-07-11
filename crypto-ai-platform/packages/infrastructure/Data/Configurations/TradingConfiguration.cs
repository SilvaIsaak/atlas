using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Trading;
using CryptoAIPlatform.Domain.Trading.ValueObjects;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(o => o.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(o => o.PortfolioId)
            .HasConversion(v => v.Value, v => new PortfolioId(v));
        builder.Property(o => o.Quantity)
            .HasConversion(v => v.Value, v => new OrderQuantity(v));
        builder.Property(o => o.FilledQuantity)
            .HasConversion(v => v.Value, v => new OrderQuantity(v));
        builder.Property(o => o.Price)
            .HasConversion(v => v!.Value, v => new OrderPrice(v));
        builder.Property(o => o.StopLoss)
            .HasConversion(v => v!.Value, v => new StopLoss(v));
        builder.Property(o => o.TakeProfit)
            .HasConversion(v => v!.Value, v => new TakeProfit(v));
        builder.Property(o => o.Leverage)
            .HasConversion(v => v!.Value, v => new Leverage(v));

        builder.HasMany(o => o.Fills)
              .WithOne()
              .HasForeignKey(f => f.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(o => o.Fees)
              .WithOne()
              .HasForeignKey(f => f.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(o => o.StatusHistory)
              .WithOne()
              .HasForeignKey(s => s.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("Positions");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(p => p.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(p => p.PortfolioId)
            .HasConversion(v => v.Value, v => new PortfolioId(v));
        builder.Property(p => p.Quantity)
            .HasConversion(v => v.Value, v => new OrderQuantity(v));
        builder.Property(p => p.EntryPrice)
            .HasConversion(v => v.Value, v => new OrderPrice(v));
        builder.Property(p => p.CurrentPrice)
            .HasConversion(v => v!.Value, v => new OrderPrice(v));
        builder.Property(p => p.Leverage)
            .HasConversion(v => v.Value, v => new Leverage(v));
        builder.Property(p => p.Margin)
            .HasJsonConversion();
        builder.Property(p => p.StopLoss)
            .HasConversion(v => v!.Value, v => new StopLoss(v));
        builder.Property(p => p.TakeProfit)
            .HasConversion(v => v!.Value, v => new TakeProfit(v));

        builder.HasMany(p => p.Legs)
              .WithOne()
              .HasForeignKey(l => l.PositionId)
              .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.PnLs)
              .WithOne()
              .HasForeignKey(l => l.PositionId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
{
    public void Configure(EntityTypeBuilder<Portfolio> builder)
    {
        builder.ToTable("Portfolios");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(p => p.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.HasMany(p => p.Assets)
              .WithOne()
              .HasForeignKey(a => a.PortfolioId)
              .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(p => p.Balances)
              .WithOne()
              .HasForeignKey(b => b.PortfolioId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

public class PortfolioSnapshotConfiguration : IEntityTypeConfiguration<PortfolioSnapshot>
{
    public void Configure(EntityTypeBuilder<PortfolioSnapshot> builder)
    {
        builder.ToTable("PortfolioSnapshots");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(p => p.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class TradingRiskProfileConfiguration : IEntityTypeConfiguration<RiskProfile>
{
    public void Configure(EntityTypeBuilder<RiskProfile> builder)
    {
        builder.ToTable("RiskProfiles");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(r => r.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(r => r.MaxLeverage)
            .HasConversion(v => v.Value, v => new Leverage(v));
    }
}

public class TradeExecutionConfiguration : IEntityTypeConfiguration<TradeExecution>
{
    public void Configure(EntityTypeBuilder<TradeExecution> builder)
    {
        builder.ToTable("TradeExecutions");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(t => t.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(t => t.Slippage)
            .HasJsonConversion();
        builder.Property(t => t.Latency)
            .HasJsonConversion();
        builder.Property(t => t.Cost)
            .HasJsonConversion();

        builder.HasMany(t => t.Fills)
              .WithOne()
              .HasForeignKey(f => f.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(t => t.Fees)
              .WithOne()
              .HasForeignKey(f => f.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

public class OrderFillConfiguration : IEntityTypeConfiguration<OrderFill>
{
    public void Configure(EntityTypeBuilder<OrderFill> builder)
    {
        builder.ToTable("OrderFills");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(f => f.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(f => f.Price)
            .HasConversion(v => v.Value, v => new OrderPrice(v));
        builder.Property(f => f.Quantity)
            .HasConversion(v => v.Value, v => new OrderQuantity(v));
        builder.Property(f => f.Fee)
            .HasJsonConversion();
    }
}

public class OrderFeeConfiguration : IEntityTypeConfiguration<OrderFee>
{
    public void Configure(EntityTypeBuilder<OrderFee> builder)
    {
        builder.ToTable("OrderFees");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(f => f.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(f => f.Fee)
            .HasJsonConversion();
    }
}

public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
{
    public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
    {
        builder.ToTable("OrderStatusHistory");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(h => h.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class PositionLegConfiguration : IEntityTypeConfiguration<PositionLeg>
{
    public void Configure(EntityTypeBuilder<PositionLeg> builder)
    {
        builder.ToTable("PositionLegs");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(l => l.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(l => l.Price)
            .HasConversion(v => v.Value, v => new OrderPrice(v));
        builder.Property(l => l.Quantity)
            .HasConversion(v => v.Value, v => new OrderQuantity(v));
    }
}

public class PositionPnLConfiguration : IEntityTypeConfiguration<PositionPnL>
{
    public void Configure(EntityTypeBuilder<PositionPnL> builder)
    {
        builder.ToTable("PositionPnLs");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(p => p.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(p => p.Value)
            .HasJsonConversion();
    }
}

public class PortfolioAssetConfiguration : IEntityTypeConfiguration<PortfolioAsset>
{
    public void Configure(EntityTypeBuilder<PortfolioAsset> builder)
    {
        builder.ToTable("PortfolioAssets");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(a => a.TenantId == ApplicationDbContext.CurrentTenantId);
        builder.Property(a => a.Quantity)
            .HasConversion(v => v.Value, v => new OrderQuantity(v));
        builder.Property(a => a.AvgEntryPrice)
            .HasConversion(v => v!.Value, v => new OrderPrice(v));
    }
}

public class PortfolioBalanceConfiguration : IEntityTypeConfiguration<PortfolioBalance>
{
    public void Configure(EntityTypeBuilder<PortfolioBalance> builder)
    {
        builder.ToTable("PortfolioBalances");
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();
        builder.HasQueryFilter(b => b.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}
