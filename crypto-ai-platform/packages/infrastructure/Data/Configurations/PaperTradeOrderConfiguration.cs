using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.PaperTrading;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class PaperTradeOrderConfiguration : IEntityTypeConfiguration<PaperTradeOrder>
{
    public void Configure(EntityTypeBuilder<PaperTradeOrder> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.PaperTrade)
            .WithMany()
            .HasForeignKey(x => x.PaperTradeId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(x => x.TenantId);
    }
}
