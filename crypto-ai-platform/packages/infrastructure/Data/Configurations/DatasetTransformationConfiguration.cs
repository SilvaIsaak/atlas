using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class DatasetTransformationConfiguration : IEntityTypeConfiguration<DatasetTransformation>
{
    public void Configure(EntityTypeBuilder<DatasetTransformation> builder)
    {
        builder.ToTable("DatasetTransformations");

        // Primary key
        builder.HasKey(t => t.Id);

        // TenantId
        builder.HasIndex(t => t.TenantId);

        // RLS
        builder.HasQueryFilter(t => t.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(t => t.Type).HasMaxLength(255).IsRequired();
        builder.Property(t => t.Code).HasMaxLength(5000);

        // Relationships
        builder.HasOne<DatasetVersion>()
              .WithMany(dv => dv.Transformations)
              .HasForeignKey(t => t.DatasetVersionId);
    }
}
