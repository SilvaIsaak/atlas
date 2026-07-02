using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class FeatureVersionConfiguration : IEntityTypeConfiguration<FeatureVersion>
{
    public void Configure(EntityTypeBuilder<FeatureVersion> builder)
    {
        builder.ToTable("FeatureVersions");

        // Primary key
        builder.HasKey(fv => fv.Id);

        // TenantId
        builder.HasIndex(fv => fv.TenantId);

        // RLS
        builder.HasQueryFilter(fv => fv.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(fv => fv.Version).HasMaxLength(100).IsRequired();
        builder.Property(fv => fv.Code).HasMaxLength(5000);

        // Relationships
        builder.HasOne<Feature>()
              .WithMany(f => f.Versions)
              .HasForeignKey(fv => fv.FeatureId);
    }
}
