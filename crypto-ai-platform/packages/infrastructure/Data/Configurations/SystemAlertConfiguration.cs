using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Monitoring;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class SystemAlertConfiguration : IEntityTypeConfiguration<SystemAlert>
{
    public void Configure(EntityTypeBuilder<SystemAlert> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.TenantId);
    }
}
