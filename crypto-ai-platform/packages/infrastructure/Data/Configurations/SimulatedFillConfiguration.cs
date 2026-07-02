using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class SimulatedFillConfiguration : IEntityTypeConfiguration<SimulatedFill>
{
    public void Configure(EntityTypeBuilder<SimulatedFill> builder)
    {
        builder.ToTable("SimulatedFills");

        // Primary key
        builder.HasKey(f => f.Id);

        // TenantId
        builder.HasIndex(f => f.TenantId);

        // RLS
        builder.HasQueryFilter(f => f.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(f => f.Price).IsRequired();
        builder.Property(f => f.Quantity).IsRequired();
        builder.Property(f => f.Fee).IsRequired();

        // Relationships
        builder.HasOne<SimulatedOrder>()
              .WithMany(o => o.Fills)
              .HasForeignKey(f => f.OrderId);
    }
}
