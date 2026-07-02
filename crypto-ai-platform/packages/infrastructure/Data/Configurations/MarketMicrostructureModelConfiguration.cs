using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class MarketMicrostructureModelConfiguration : IEntityTypeConfiguration<MarketMicrostructureModel>
{
    public void Configure(EntityTypeBuilder<MarketMicrostructureModel> builder)
    {
        builder.ToTable("MarketMicrostructureModels");

        // Primary key
        builder.HasKey(m => m.Id);

        // TenantId
        builder.HasIndex(m => m.TenantId);

        // RLS
        builder.HasQueryFilter(m => m.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(m => m.Name).HasMaxLength(255).IsRequired();
        builder.Property(m => m.AssetSymbol).HasMaxLength(50).IsRequired();
        builder.Property(m => m.IsActive).IsRequired();

        // Relationships
        builder.HasMany(m => m.SpreadData)
              .WithOne()
              .HasForeignKey(sd => sd.ModelId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.SlippageModels)
              .WithOne()
              .HasForeignKey(sm => sm.ModelId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.LiquidityProfiles)
              .WithOne()
              .HasForeignKey(lp => lp.ModelId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
