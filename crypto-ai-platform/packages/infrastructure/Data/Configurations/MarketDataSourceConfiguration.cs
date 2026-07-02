using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class MarketDataSourceConfiguration : IEntityTypeConfiguration<MarketDataSource>
{
    public void Configure(EntityTypeBuilder<MarketDataSource> builder)
    {
        builder.ToTable("MarketDataSources");

        // Primary key
        builder.HasKey(mds => mds.Id);

        // TenantId
        builder.HasIndex(mds => mds.TenantId);

        // RLS
        builder.HasQueryFilter(mds => mds.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(mds => mds.Name).HasMaxLength(255).IsRequired();
        builder.Property(mds => mds.BaseUrl).HasMaxLength(500).IsRequired();
        builder.Property(mds => mds.EncryptedApiKey).HasMaxLength(2000);
        builder.Property(mds => mds.ApiKeyNonce).HasMaxLength(100);
        builder.Property(mds => mds.ApiKeyTag).HasMaxLength(100);
        builder.Property(mds => mds.Type).IsRequired();
        builder.Property(mds => mds.IsActive).IsRequired();

        // Relationships
        builder.HasMany(mds => mds.IngestionJobs)
              .WithOne()
              .HasForeignKey(ing => ing.DataSourceId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(mds => mds.Assets)
              .WithOne()
              .HasForeignKey(ma => ma.DataSourceId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
