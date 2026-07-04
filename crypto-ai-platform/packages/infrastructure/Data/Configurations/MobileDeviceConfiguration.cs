using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Mobile;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class MobileDeviceConfiguration : IEntityTypeConfiguration<MobileDevice>
{
    public void Configure(EntityTypeBuilder<MobileDevice> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.TenantId);
    }
}
