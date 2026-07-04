using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Exchanges;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ExchangeIntegrationConfiguration : IEntityTypeConfiguration<ExchangeIntegration>
{
    public void Configure(EntityTypeBuilder<ExchangeIntegration> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(x => x.Exchange)
            .WithMany()
            .HasForeignKey(x => x.ExchangeId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.Property(x => x.ApiKey)
            .IsRequired()
            .HasMaxLength(500);
            
        builder.Property(x => x.ApiSecret)
            .IsRequired()
            .HasMaxLength(500);
    }
}
