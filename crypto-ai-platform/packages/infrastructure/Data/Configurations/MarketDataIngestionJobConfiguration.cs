using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class MarketDataIngestionJobConfiguration : IEntityTypeConfiguration<MarketDataIngestionJob>
{
    public void Configure(EntityTypeBuilder<MarketDataIngestionJob> builder)
    {
        builder.ToTable("MarketDataIngestionJobs");

        // Primary key
        builder.HasKey(j => j.Id);

        // TenantId
        builder.HasIndex(j => j.TenantId);

        // RLS
        builder.HasQueryFilter(j => j.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(j => j.AssetSymbol).HasMaxLength(50).IsRequired();
        builder.Property(j => j.DataType).IsRequired();
        builder.Property(j => j.Status).IsRequired();

        // Relationships
        builder.HasOne<MarketDataSource>()
              .WithMany(mds => mds.IngestionJobs)
              .HasForeignKey(j => j.DataSourceId);
    }
}
