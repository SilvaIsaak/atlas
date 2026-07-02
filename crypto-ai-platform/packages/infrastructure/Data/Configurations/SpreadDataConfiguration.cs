using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class SpreadDataConfiguration : IEntityTypeConfiguration<SpreadData>
{
    public void Configure(EntityTypeBuilder<SpreadData> builder)
    {
        builder.ToTable("SpreadData");

        // Primary key
        builder.HasKey(sd => sd.Id);

        // TenantId
        builder.HasIndex(sd => sd.TenantId);

        // RLS
        builder.HasQueryFilter(sd => sd.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(sd => sd.AssetSymbol).HasMaxLength(50).IsRequired();

        // Relationships
        builder.HasOne<MarketMicrostructureModel>()
              .WithMany(m => m.SpreadData)
              .HasForeignKey(sd => sd.ModelId);
    }
}
