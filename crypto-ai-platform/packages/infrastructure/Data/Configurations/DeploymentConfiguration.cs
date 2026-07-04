using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Deployment;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class DeploymentConfiguration : IEntityTypeConfiguration<Deployment>
{
    public void Configure(EntityTypeBuilder<Deployment> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.TenantId);
    }
}
