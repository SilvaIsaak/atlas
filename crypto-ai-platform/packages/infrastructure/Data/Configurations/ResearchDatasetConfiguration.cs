using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ResearchDatasetConfiguration : IEntityTypeConfiguration<ResearchDataset>
{
    public void Configure(EntityTypeBuilder<ResearchDataset> builder)
    {
        builder.ToTable("ResearchDatasets");

        // Primary key
        builder.HasKey(d => d.Id);

        // TenantId
        builder.HasIndex(d => d.TenantId);

        // RLS
        builder.HasQueryFilter(d => d.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(d => d.Name).HasMaxLength(255).IsRequired();
        builder.Property(d => d.Description).HasMaxLength(2000);
        builder.Property(d => d.OwnerId).IsRequired();
        builder.Property(d => d.Version).HasMaxLength(100).IsRequired();
        builder.Property(d => d.IsImmutable).IsRequired();

        // Relationships
        builder.HasMany(d => d.Versions)
              .WithOne()
              .HasForeignKey(dv => dv.DatasetId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
