using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.LiveTrading;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class LiveTradeConfiguration : IEntityTypeConfiguration<LiveTrade>
{
    public void Configure(EntityTypeBuilder<LiveTrade> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.HasIndex(x => x.TenantId);
    }
}
