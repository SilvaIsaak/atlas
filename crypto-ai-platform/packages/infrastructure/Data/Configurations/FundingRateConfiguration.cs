using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class FundingRateConfiguration : IEntityTypeConfiguration<FundingRate>
{
    public void Configure(EntityTypeBuilder<FundingRate> builder)
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

        builder.Property(x => x.FundingTimeUtc)
            .IsRequired();

        builder.Property(x => x.Rate)
            .HasColumnType("decimal(18, 8)");

        builder.HasIndex(x => new { x.TenantId, x.Exchange, x.Symbol, x.TimestampUtc });
    }
}
