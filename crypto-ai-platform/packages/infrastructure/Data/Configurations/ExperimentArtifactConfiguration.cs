using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExperimentArtifactConfiguration : IEntityTypeConfiguration<ExperimentArtifact>
{
    public void Configure(EntityTypeBuilder<ExperimentArtifact> builder)
    {
        builder.ToTable("ExperimentArtifacts");

        // Primary key
        builder.HasKey(a => a.Id);

        // TenantId
        builder.HasIndex(a => a.TenantId);

        // RLS
        builder.HasQueryFilter(a => a.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(a => a.Name).HasMaxLength(255).IsRequired();
        builder.Property(a => a.Type).IsRequired();
        builder.Property(a => a.StoragePath).HasMaxLength(2000).IsRequired();

        // Relationships
        builder.HasOne<ExperimentRun>()
              .WithMany(r => r.Artifacts)
              .HasForeignKey(a => a.ExperimentRunId);
    }
}
