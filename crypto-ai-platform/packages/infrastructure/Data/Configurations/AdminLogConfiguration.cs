using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Admin;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class AdminLogConfiguration : IEntityTypeConfiguration<AdminLog>
{
    public void Configure(EntityTypeBuilder<AdminLog> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.TenantId);
    }
}
