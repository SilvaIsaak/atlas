using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class FeatureLineageConfiguration : IEntityTypeConfiguration<FeatureLineage>
{
    public void Configure(EntityTypeBuilder<FeatureLineage> builder)
    {
        builder.ToTable("FeatureLineages");

        // Primary key
        builder.HasKey(fl => fl.Id);

        // TenantId
        builder.HasIndex(fl => fl.TenantId);

        // RLS
        builder.HasQueryFilter(fl => fl.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties (FeatureLineageNode as JSON)
        builder.Property(fl => fl.Nodes).HasJsonConversion();
    }
}
