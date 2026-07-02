using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class MarketDataAssetConfiguration : IEntityTypeConfiguration<MarketDataAsset>
{
    public void Configure(EntityTypeBuilder<MarketDataAsset> builder)
    {
        builder.ToTable("MarketDataAssets");

        // Primary key
        builder.HasKey(ma => ma.Id);

        // TenantId
        builder.HasIndex(ma => ma.TenantId);

        // RLS
        builder.HasQueryFilter(ma => ma.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(ma => ma.Symbol).HasMaxLength(50).IsRequired();
        builder.Property(ma => ma.Name).HasMaxLength(255).IsRequired();
        builder.Property(ma => ma.IsActive).IsRequired();

        // Relationships
        builder.HasOne<MarketDataSource>()
              .WithMany(mds => mds.Assets)
              .HasForeignKey(ma => ma.DataSourceId);
    }
}
