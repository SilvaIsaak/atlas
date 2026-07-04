using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.PaperTrading;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class PaperTradeConfiguration : IEntityTypeConfiguration<PaperTrade>
{
    public void Configure(EntityTypeBuilder<PaperTrade> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.HasIndex(x => x.TenantId);
    }
}
