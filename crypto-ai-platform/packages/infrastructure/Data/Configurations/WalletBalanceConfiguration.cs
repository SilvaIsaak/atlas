using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Wallets;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class WalletBalanceConfiguration : IEntityTypeConfiguration<WalletBalance>
{
    public void Configure(EntityTypeBuilder<WalletBalance> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Asset)
            .IsRequired()
            .HasMaxLength(20);
            
        builder.Property(x => x.Free)
            .HasPrecision(18, 8);
            
        builder.Property(x => x.Locked)
            .HasPrecision(18, 8);
    }
}
