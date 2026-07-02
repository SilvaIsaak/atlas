using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class DatasetVersionConfiguration : IEntityTypeConfiguration<DatasetVersion>
{
    public void Configure(EntityTypeBuilder<DatasetVersion> builder)
    {
        builder.ToTable("DatasetVersions");

        // Primary key
        builder.HasKey(dv => dv.Id);

        // TenantId
        builder.HasIndex(dv => dv.TenantId);

        // RLS
        builder.HasQueryFilter(dv => dv.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(dv => dv.Version).HasMaxLength(100).IsRequired();
        builder.Property(dv => dv.AssetSymbols).HasJsonConversion();

        // Relationships
        builder.HasOne<ResearchDataset>()
              .WithMany(d => d.Versions)
              .HasForeignKey(dv => dv.DatasetId);

        builder.HasMany(dv => dv.Transformations)
              .WithOne()
              .HasForeignKey(t => t.DatasetVersionId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
