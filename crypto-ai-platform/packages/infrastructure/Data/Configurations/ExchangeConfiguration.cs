using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Exchanges;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExchangeConfiguration : IEntityTypeConfiguration<Exchange>
{
    public void Configure(EntityTypeBuilder<Exchange> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.HasIndex(x => x.Code).IsUnique();
    }
}
