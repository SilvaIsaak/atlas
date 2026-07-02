using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class FeatureCatalogEntryConfiguration : IEntityTypeConfiguration<FeatureCatalogEntry>
{
    public void Configure(EntityTypeBuilder<FeatureCatalogEntry> builder)
    {
        builder.ToTable("FeatureCatalogEntries");

        // Primary key
        builder.HasKey(ce => ce.Id);

        // TenantId
        builder.HasIndex(ce => ce.TenantId);

        // RLS
        builder.HasQueryFilter(ce => ce.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(ce => ce.Description).HasMaxLength(2000);
        builder.Property(ce => ce.UsageExamples).HasMaxLength(5000);
        builder.Property(ce => ce.PerformanceMetrics).HasMaxLength(5000);

        // Relationships
        builder.HasOne<Feature>()
              .WithMany(f => f.CatalogEntries)
              .HasForeignKey(ce => ce.FeatureId);
    }
}
