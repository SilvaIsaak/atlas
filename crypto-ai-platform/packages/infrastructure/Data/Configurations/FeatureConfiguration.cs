using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.ToTable("Features");

        // Primary key
        builder.HasKey(f => f.Id);

        // TenantId
        builder.HasIndex(f => f.TenantId);

        // RLS
        builder.HasQueryFilter(f => f.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(f => f.Name).HasMaxLength(255).IsRequired();
        builder.Property(f => f.Description).HasMaxLength(2000);
        builder.Property(f => f.Code).HasMaxLength(5000);
        builder.Property(f => f.Maturity).IsRequired();
        builder.Property(f => f.OwnerId).IsRequired();
        builder.Property(f => f.IsApproved).IsRequired();

        // Relationships
        builder.HasMany(f => f.Versions)
              .WithOne()
              .HasForeignKey(fv => fv.FeatureId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(f => f.CatalogEntries)
              .WithOne()
              .HasForeignKey(ce => ce.FeatureId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
