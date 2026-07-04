using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class LiquidityProfileConfiguration : IEntityTypeConfiguration<LiquidityProfile>
{
    public void Configure(EntityTypeBuilder<LiquidityProfile> builder)
    {
        builder.ToTable("LiquidityProfiles");

        // Primary key
        builder.HasKey(lp => lp.Id);

        // TenantId
        builder.HasIndex(lp => lp.TenantId);

        // RLS
        builder.HasQueryFilter(lp => lp.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(lp => lp.AssetSymbol).HasMaxLength(50).IsRequired();
        builder.Property(lp => lp.OrderBookLevels).HasJsonConversion();

        // Relationships
        builder.HasOne<MarketMicrostructureModel>()
              .WithMany(m => m.LiquidityProfiles)
              .HasForeignKey(lp => lp.ModelId);
    }
}
