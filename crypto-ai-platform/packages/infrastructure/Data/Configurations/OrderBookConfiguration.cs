using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class OrderBookConfiguration : IEntityTypeConfiguration<OrderBook>
{
    public void Configure(EntityTypeBuilder<OrderBook> builder)
    {
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();

        builder.Property(x => x.Exchange)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Symbol)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.TimestampUtc)
            .IsRequired();

        builder.OwnsMany(x => x.Bids, b =>
        {
            b.ToJson();
            b.Property(x => x.Price).HasColumnType("decimal(18, 8)");
            b.Property(x => x.Quantity).HasColumnType("decimal(18, 8)");
        });

        builder.OwnsMany(x => x.Asks, a =>
        {
            a.ToJson();
            a.Property(x => x.Price).HasColumnType("decimal(18, 8)");
            a.Property(x => x.Quantity).HasColumnType("decimal(18, 8)");
        });

        builder.HasIndex(x => new { x.TenantId, x.Exchange, x.Symbol, x.TimestampUtc });
    }
}
