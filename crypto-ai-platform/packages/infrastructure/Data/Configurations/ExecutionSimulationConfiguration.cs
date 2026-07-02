using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExecutionSimulationConfiguration : IEntityTypeConfiguration<ExecutionSimulation>
{
    public void Configure(EntityTypeBuilder<ExecutionSimulation> builder)
    {
        builder.ToTable("ExecutionSimulations");

        // Primary key
        builder.HasKey(es => es.Id);

        // TenantId
        builder.HasIndex(es => es.TenantId);

        // RLS
        builder.HasQueryFilter(es => es.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(es => es.Name).HasMaxLength(255).IsRequired();
        builder.Property(es => es.Status).IsRequired();

        // Relationships
        builder.HasMany(es => es.Orders)
              .WithOne()
              .HasForeignKey(o => o.SimulationId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
