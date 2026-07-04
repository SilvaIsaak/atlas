using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Abstractions;

namespace CryptoAIPlatform.Infrastructure.Data.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasJsonConversion<TProperty>(
        this PropertyBuilder<TProperty> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<TProperty>(v, (JsonSerializerOptions?)null)!);
    }
    
    public static void ConfigureTenantId(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity<Guid>).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<TenantId>("TenantId")
                    .HasConversion(
                        v => v.Value,
                        v => TenantId.From(v));
            }
        }
    }

    public static EntityTypeBuilder<TEntity> ConfigureBaseEntity<TEntity>(
        this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity<Guid>
    {
        builder.HasKey(x => x.Id);
        builder.Ignore(x => x.DomainEvents);
        return builder;
    }

    public static EntityTypeBuilder<TEntity> ConfigureTenantId<TEntity>(
        this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity<Guid>
    {
        builder.Property(x => x.TenantId)
            .HasConversion(
                v => v.Value,
                v => TenantId.From(v));
        builder.HasIndex(x => x.TenantId);
        return builder;
    }
}
