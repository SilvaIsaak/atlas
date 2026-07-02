using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
}
