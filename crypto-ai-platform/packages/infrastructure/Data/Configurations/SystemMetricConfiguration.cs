using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Monitoring;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class SystemMetricConfiguration : IEntityTypeConfiguration<SystemMetric>
{
    public void Configure(EntityTypeBuilder<SystemMetric> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.TenantId);
        builder.HasIndex(x => x.CreatedAt);
    }
}
