using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class AnomalyConfiguration : IEntityTypeConfiguration<Anomaly>
{
    public void Configure(EntityTypeBuilder<Anomaly> builder)
    {
        builder.ToTable("Anomalies");

        // Primary key
        builder.HasKey(a => a.Id);

        // TenantId
        builder.HasIndex(a => a.TenantId);

        // RLS
        builder.HasQueryFilter(a => a.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(a => a.AssetSymbol).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Type).IsRequired();
        builder.Property(a => a.Severity).IsRequired();
        builder.Property(a => a.IsResolved).IsRequired();
    }
}
