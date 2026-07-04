using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class FeatureReference : BaseEntity<Guid>
{
    public Guid ReproducibilityPackageId { get; private set; }
    public Guid FeatureVersionId { get; private set; }
    public string? Description { get; private set; }

    private FeatureReference() { }

    public static FeatureReference Create(
        Guid id,
        TenantId tenantId,
        Guid reproducibilityPackageId,
        Guid featureVersionId,
        string? description,
        Guid? createdBy = null)
    {
        return new FeatureReference
        {
            Id = id,
            TenantId = tenantId,
            ReproducibilityPackageId = reproducibilityPackageId,
            FeatureVersionId = featureVersionId,
            Description = description,
            CreatedBy = createdBy
        };
    }
}
