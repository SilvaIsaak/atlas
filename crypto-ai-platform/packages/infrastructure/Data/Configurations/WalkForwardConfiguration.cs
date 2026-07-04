using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.WalkForward;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class WalkForwardConfiguration : IEntityTypeConfiguration<WalkForward>
{
    public void Configure(EntityTypeBuilder<WalkForward> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.HasIndex(x => x.TenantId);
    }
}
