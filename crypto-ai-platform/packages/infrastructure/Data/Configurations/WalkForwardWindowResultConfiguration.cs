using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.WalkForward;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class WalkForwardWindowResultConfiguration : IEntityTypeConfiguration<WalkForwardWindowResult>
{
    public void Configure(EntityTypeBuilder<WalkForwardWindowResult> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.WalkForward)
            .WithMany()
            .HasForeignKey(x => x.WalkForwardId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(x => x.TenantId);
    }
}
