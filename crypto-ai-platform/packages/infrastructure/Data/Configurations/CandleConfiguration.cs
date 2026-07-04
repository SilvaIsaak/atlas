using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class CandleConfiguration : IEntityTypeConfiguration<Candle>
{
    public void Configure(EntityTypeBuilder<Candle> builder)
    {
        builder.ConfigureBaseEntity();
        builder.ConfigureTenantId();

        builder.Property(x => x.Exchange)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Symbol)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Timeframe)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.TimestampUtc)
            .IsRequired();

        builder.Property(x => x.Open)
            .HasColumnType("decimal(18, 8)");

        builder.Property(x => x.High)
            .HasColumnType("decimal(18, 8)");

        builder.Property(x => x.Low)
            .HasColumnType("decimal(18, 8)");

        builder.Property(x => x.Close)
            .HasColumnType("decimal(18, 8)");

        builder.Property(x => x.Volume)
            .HasColumnType("decimal(18, 8)");

        builder.Property(x => x.QuoteVolume)
            .HasColumnType("decimal(18, 8)");

        builder.HasIndex(x => new { x.TenantId, x.Exchange, x.Symbol, x.Timeframe, x.TimestampUtc })
            .IsUnique();
    }
}
