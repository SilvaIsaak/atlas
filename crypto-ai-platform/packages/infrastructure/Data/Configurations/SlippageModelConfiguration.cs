using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class SlippageModelConfiguration : IEntityTypeConfiguration<SlippageModel>
{
    public void Configure(EntityTypeBuilder<SlippageModel> builder)
    {
        builder.ToTable("SlippageModels");

        // Primary key
        builder.HasKey(sm => sm.Id);

        // TenantId
        builder.HasIndex(sm => sm.TenantId);

        // RLS
        builder.HasQueryFilter(sm => sm.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(sm => sm.AssetSymbol).HasMaxLength(50).IsRequired();
        builder.Property(sm => sm.Parameters).HasJsonConversion();

        // Relationships
        builder.HasOne<MarketMicrostructureModel>()
              .WithMany(m => m.SlippageModels)
              .HasForeignKey(sm => sm.ModelId);
    }
}
