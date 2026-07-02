using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExperimentConfiguration : IEntityTypeConfiguration<Experiment>
{
    public void Configure(EntityTypeBuilder<Experiment> builder)
    {
        builder.ToTable("Experiments");

        // Primary key
        builder.HasKey(e => e.Id);

        // TenantId
        builder.HasIndex(e => e.TenantId);

        // RLS
        builder.HasQueryFilter(e => e.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(e => e.Name).HasMaxLength(255).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(2000);
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.OwnerId).IsRequired();

        // Relationships
        builder.HasMany(e => e.Parameters)
              .WithOne()
              .HasForeignKey(p => p.ExperimentId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Runs)
              .WithOne()
              .HasForeignKey(r => r.ExperimentId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
