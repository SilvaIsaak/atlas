using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class SimulatedOrderConfiguration : IEntityTypeConfiguration<SimulatedOrder>
{
    public void Configure(EntityTypeBuilder<SimulatedOrder> builder)
    {
        builder.ToTable("SimulatedOrders");

        // Primary key
        builder.HasKey(o => o.Id);

        // TenantId
        builder.HasIndex(o => o.TenantId);

        // RLS
        builder.HasQueryFilter(o => o.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(o => o.Type).IsRequired();
        builder.Property(o => o.Symbol).HasMaxLength(50).IsRequired();
        builder.Property(o => o.Side).IsRequired();
        builder.Property(o => o.Quantity).IsRequired();
        builder.Property(o => o.Status).IsRequired();

        // Relationships
        builder.HasOne<ExecutionSimulation>()
              .WithMany(es => es.Orders)
              .HasForeignKey(o => o.SimulationId);

        builder.HasMany(o => o.Fills)
              .WithOne()
              .HasForeignKey(f => f.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
