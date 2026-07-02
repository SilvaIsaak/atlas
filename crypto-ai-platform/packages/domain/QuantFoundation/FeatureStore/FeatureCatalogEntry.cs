using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

public class FeatureCatalogEntry : BaseEntity<Guid>
{
    public Guid FeatureId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public string? UsageExamples { get; private set; }
    public string? PerformanceMetrics { get; private set; }

    private FeatureCatalogEntry() { }

    public static FeatureCatalogEntry Create(
        Guid id,
        TenantId tenantId,
        Guid featureId,
        string description,
        string? usageExamples = null,
        string? performanceMetrics = null,
        Guid? createdBy = null)
    {
        return new FeatureCatalogEntry
        {
            Id = id,
            TenantId = tenantId,
            FeatureId = featureId,
            Description = description,
            UsageExamples = usageExamples,
            PerformanceMetrics = performanceMetrics,
            CreatedBy = createdBy
        };
    }
}