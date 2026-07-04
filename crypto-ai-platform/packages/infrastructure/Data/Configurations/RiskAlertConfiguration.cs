using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.RiskManagement;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class RiskAlertConfiguration : IEntityTypeConfiguration<RiskAlert>
{
    public void Configure(EntityTypeBuilder<RiskAlert> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.RiskProfile)
            .WithMany()
            .HasForeignKey(x => x.RiskProfileId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasIndex(x => x.TenantId);
    }
}
