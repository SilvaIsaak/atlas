using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class ReproducibilityPackageConfiguration : IEntityTypeConfiguration<ReproducibilityPackage>
{
    public void Configure(EntityTypeBuilder<ReproducibilityPackage> builder)
    {
        builder.ToTable("ReproducibilityPackages");

        // Primary key
        builder.HasKey(p => p.Id);

        // TenantId
        builder.HasIndex(p => p.TenantId);

        // RLS
        builder.HasQueryFilter(p => p.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.EnvironmentInfo).HasJsonConversion();
    }
}
