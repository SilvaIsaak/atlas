using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.ValueObjects;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class EnvironmentSnapshotConfiguration : IEntityTypeConfiguration<EnvironmentSnapshot>
{
    public void Configure(EntityTypeBuilder<EnvironmentSnapshot> builder)
    {
        builder.ToTable("EnvironmentSnapshots");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.TenantId);
        builder.HasQueryFilter(e => e.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.OwnsOne(e => e.DockerImage);
        builder.OwnsOne(e => e.PythonEnvironment);
        builder.OwnsOne(e => e.DotNetEnvironment);
        builder.Property(e => e.EnvironmentVariables).HasJsonConversion();
    }
}

public class GitSnapshotConfiguration : IEntityTypeConfiguration<GitSnapshot>
{
    public void Configure(EntityTypeBuilder<GitSnapshot> builder)
    {
        builder.ToTable("GitSnapshots");

        builder.HasKey(g => g.Id);

        builder.HasIndex(g => g.TenantId);
        builder.HasQueryFilter(g => g.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.OwnsOne(g => g.CommitHash);
    }
}

public class DependencySnapshotConfiguration : IEntityTypeConfiguration<DependencySnapshot>
{
    public void Configure(EntityTypeBuilder<DependencySnapshot> builder)
    {
        builder.ToTable("DependencySnapshots");

        builder.HasKey(d => d.Id);

        builder.HasIndex(d => d.TenantId);
        builder.HasQueryFilter(d => d.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(d => d.Dependencies).HasJsonConversion();
        builder.OwnsOne(d => d.Checksum);
    }
}

public class DatasetReferenceConfiguration : IEntityTypeConfiguration<DatasetReference>
{
    public void Configure(EntityTypeBuilder<DatasetReference> builder)
    {
        builder.ToTable("DatasetReferences");

        builder.HasKey(d => d.Id);

        builder.HasIndex(d => d.TenantId);
        builder.HasQueryFilter(d => d.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class FeatureReferenceConfiguration : IEntityTypeConfiguration<FeatureReference>
{
    public void Configure(EntityTypeBuilder<FeatureReference> builder)
    {
        builder.ToTable("FeatureReferences");

        builder.HasKey(f => f.Id);

        builder.HasIndex(f => f.TenantId);
        builder.HasQueryFilter(f => f.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class ExperimentReferenceConfiguration : IEntityTypeConfiguration<ExperimentReference>
{
    public void Configure(EntityTypeBuilder<ExperimentReference> builder)
    {
        builder.ToTable("ExperimentReferences");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.TenantId);
        builder.HasQueryFilter(e => e.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class ArtifactReferenceConfiguration : IEntityTypeConfiguration<ArtifactReference>
{
    public void Configure(EntityTypeBuilder<ArtifactReference> builder)
    {
        builder.ToTable("ArtifactReferences");

        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.TenantId);
        builder.HasQueryFilter(a => a.TenantId == ApplicationDbContext.CurrentTenantId);
    }
}

public class ExecutionManifestConfiguration : IEntityTypeConfiguration<ExecutionManifest>
{
    public void Configure(EntityTypeBuilder<ExecutionManifest> builder)
    {
        builder.ToTable("ExecutionManifests");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.TenantId);
        builder.HasQueryFilter(e => e.TenantId == ApplicationDbContext.CurrentTenantId);

        builder.Property(e => e.ManifestJson).IsRequired();
        builder.OwnsOne(e => e.Hash);
        builder.OwnsOne(e => e.Fingerprint);
    }
}

public class ReproducibilityPackageExtendedConfiguration : IEntityTypeConfiguration<ReproducibilityPackage>
{
    public void Configure(EntityTypeBuilder<ReproducibilityPackage> builder)
    {
        builder.OwnsOne(r => r.EnvironmentInfo);
        builder.OwnsOne(r => r.Checksum);

        builder.HasMany(r => r.EnvironmentSnapshots)
            .WithOne()
            .HasForeignKey(e => e.ReproducibilityPackageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(r => r.GitSnapshots)
            .WithOne()
            .HasForeignKey(g => g.ReproducibilityPackageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(r => r.DependencySnapshots)
            .WithOne()
            .HasForeignKey(d => d.ReproducibilityPackageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(r => r.DatasetReferences)
            .WithOne()
            .HasForeignKey(d => d.ReproducibilityPackageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(r => r.FeatureReferences)
            .WithOne()
            .HasForeignKey(f => f.ReproducibilityPackageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(r => r.ExperimentReferences)
            .WithOne()
            .HasForeignKey(e => e.ReproducibilityPackageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(r => r.ArtifactReferences)
            .WithOne()
            .HasForeignKey(a => a.ReproducibilityPackageId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(r => r.ExecutionManifests)
            .WithOne()
            .HasForeignKey(e => e.ReproducibilityPackageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
