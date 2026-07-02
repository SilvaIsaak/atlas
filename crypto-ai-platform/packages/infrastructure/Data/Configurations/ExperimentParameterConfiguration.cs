using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExperimentParameterConfiguration : IEntityTypeConfiguration<ExperimentParameter>
{
    public void Configure(EntityTypeBuilder<ExperimentParameter> builder)
    {
        builder.ToTable("ExperimentParameters");

        // Primary key
        builder.HasKey(p => p.Id);

        // TenantId
        builder.HasIndex(p => p.TenantId);

        // RLS
        builder.HasQueryFilter(p => p.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(p => p.Key).HasMaxLength(255).IsRequired();
        builder.Property(p => p.Value).HasMaxLength(2000).IsRequired();

        // Relationships
        builder.HasOne<Experiment>()
              .WithMany(e => e.Parameters)
              .HasForeignKey(p => p.ExperimentId);
    }
}
