using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExperimentRunConfiguration : IEntityTypeConfiguration<ExperimentRun>
{
    public void Configure(EntityTypeBuilder<ExperimentRun> builder)
    {
        builder.ToTable("ExperimentRuns");

        // Primary key
        builder.HasKey(r => r.Id);

        // TenantId
        builder.HasIndex(r => r.TenantId);

        // RLS
        builder.HasQueryFilter(r => r.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(r => r.Status).IsRequired();
        builder.Property(r => r.StrategyVersion).HasMaxLength(100);

        // Relationships
        builder.HasOne<Experiment>()
              .WithMany(e => e.Runs)
              .HasForeignKey(r => r.ExperimentId);

        builder.HasMany(r => r.Metrics)
              .WithOne()
              .HasForeignKey(m => m.ExperimentRunId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Artifacts)
              .WithOne()
              .HasForeignKey(a => a.ExperimentRunId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
