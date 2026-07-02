using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExperimentMetricConfiguration : IEntityTypeConfiguration<ExperimentMetric>
{
    public void Configure(EntityTypeBuilder<ExperimentMetric> builder)
    {
        builder.ToTable("ExperimentMetrics");

        // Primary key
        builder.HasKey(m => m.Id);

        // TenantId
        builder.HasIndex(m => m.TenantId);

        // RLS
        builder.HasQueryFilter(m => m.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(m => m.Key).HasMaxLength(255).IsRequired();
        builder.Property(m => m.Name).HasMaxLength(255).IsRequired();
        builder.Property(m => m.Value).IsRequired();

        // Relationships
        builder.HasOne<ExperimentRun>()
              .WithMany(r => r.Metrics)
              .HasForeignKey(m => m.ExperimentRunId);
    }
}
