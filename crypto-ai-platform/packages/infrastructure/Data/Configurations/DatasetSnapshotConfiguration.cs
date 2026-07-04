using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.Data.Configurations;

public class DatasetSnapshotConfiguration : IEntityTypeConfiguration<DatasetSnapshot>
{
    public void Configure(EntityTypeBuilder<DatasetSnapshot> builder)
    {
        builder.ToTable("DatasetSnapshots");

        // Primary key
        builder.HasKey(ds => ds.Id);

        // TenantId
        builder.HasIndex(ds => ds.TenantId);

        // RLS
        builder.HasQueryFilter(ds => ds.TenantId == ApplicationDbContext.CurrentTenantId);

        // Value objects
        builder.OwnsOne(ds => ds.Location, loc =>
        {
            loc.Property(l => l.Path).HasMaxLength(1000).IsRequired();
            loc.Property(l => l.Provider).HasMaxLength(100).IsRequired();
        });

        builder.OwnsOne(ds => ds.Hash, hash =>
        {
            hash.Property(h => h.Algorithm).HasMaxLength(50).IsRequired();
            hash.Property(h => h.Value).HasMaxLength(256).IsRequired();
        });

        // Relationships
        builder.HasOne<DatasetVersion>()
              .WithMany(dv => dv.Snapshots)
              .HasForeignKey(ds => ds.DatasetVersionId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

public class DatasetMetadataConfiguration : IEntityTypeConfiguration<DatasetMetadata>
{
    public void Configure(EntityTypeBuilder<DatasetMetadata> builder)
    {
        builder.ToTable("DatasetMetadata");

        // Primary key
        builder.HasKey(dm => dm.Id);

        // TenantId
        builder.HasIndex(dm => dm.TenantId);

        // RLS
        builder.HasQueryFilter(dm => dm.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(dm => dm.MetadataJson).IsRequired();

        // Relationships
        builder.HasOne<DatasetVersion>()
              .WithMany(dv => dv.Metadata)
              .HasForeignKey(dm => dm.DatasetVersionId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

public class DatasetSchemaConfiguration : IEntityTypeConfiguration<DatasetSchema>
{
    public void Configure(EntityTypeBuilder<DatasetSchema> builder)
    {
        builder.ToTable("DatasetSchemas");

        // Primary key
        builder.HasKey(ds => ds.Id);

        // TenantId
        builder.HasIndex(ds => ds.TenantId);

        // RLS
        builder.HasQueryFilter(ds => ds.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(ds => ds.SchemaJson).IsRequired();

        // Relationships
        builder.HasOne<DatasetVersion>()
              .WithMany(dv => dv.Schemas)
              .HasForeignKey(ds => ds.DatasetVersionId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

public class DatasetTagConfiguration : IEntityTypeConfiguration<DatasetTag>
{
    public void Configure(EntityTypeBuilder<DatasetTag> builder)
    {
        builder.ToTable("DatasetTags");

        // Primary key
        builder.HasKey(dt => dt.Id);

        // TenantId
        builder.HasIndex(dt => dt.TenantId);

        // RLS
        builder.HasQueryFilter(dt => dt.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(dt => dt.Tag).HasMaxLength(100).IsRequired();

        // Relationships
        builder.HasOne<ResearchDataset>()
              .WithMany(d => d.Tags)
              .HasForeignKey(dt => dt.DatasetId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}

public class DatasetPartitionConfiguration : IEntityTypeConfiguration<DatasetPartition>
{
    public void Configure(EntityTypeBuilder<DatasetPartition> builder)
    {
        builder.ToTable("DatasetPartitions");

        // Primary key
        builder.HasKey(dp => dp.Id);

        // TenantId
        builder.HasIndex(dp => dp.TenantId);

        // RLS
        builder.HasQueryFilter(dp => dp.TenantId == ApplicationDbContext.CurrentTenantId);

        // Properties
        builder.Property(dp => dp.PartitionKey).HasMaxLength(255).IsRequired();

        // Value objects
        builder.OwnsOne(dp => dp.Location, loc =>
        {
            loc.Property(l => l.Path).HasMaxLength(1000).IsRequired();
            loc.Property(l => l.Provider).HasMaxLength(100).IsRequired();
        });

        builder.OwnsOne(dp => dp.Hash, hash =>
        {
            hash.Property(h => h.Algorithm).HasMaxLength(50).IsRequired();
            hash.Property(h => h.Value).HasMaxLength(256).IsRequired();
        });

        builder.OwnsOne(dp => dp.Size, size =>
        {
            size.Property(s => s.Bytes).IsRequired();
            size.Property(s => s.HumanReadable).HasMaxLength(50).IsRequired();
        });

        // Relationships
        builder.HasOne<DatasetVersion>()
              .WithMany(dv => dv.Partitions)
              .HasForeignKey(dp => dp.DatasetVersionId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}
