using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class DataQualityJobConfiguration : IEntityTypeConfiguration<DataQualityJob>
{
    public void Configure(EntityTypeBuilder<DataQualityJob> builder)
    {
        builder.ToTable("DataQualityJobs");

        // Primary key
        builder.HasKey(j => j.Id);

        // TenantId
        builder.HasIndex(j => j.TenantId);

        // RLS
        builder.HasQueryFilter(j => j.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(j => j.AssetSymbol).HasMaxLength(50).IsRequired();
        builder.Property(j => j.Status).IsRequired();

        // Relationships
        builder.HasMany(j => j.Anomalies)
              .WithOne()
              .HasForeignKey(a => a.JobId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
