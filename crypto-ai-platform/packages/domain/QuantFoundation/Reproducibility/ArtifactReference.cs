using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class ArtifactReference : BaseEntity<Guid>
{
    public Guid ReproducibilityPackageId { get; private set; }
    public Guid ArtifactId { get; private set; }
    public string? Description { get; private set; }

    private ArtifactReference() { }

    public static ArtifactReference Create(
        Guid id,
        TenantId tenantId,
        Guid reproducibilityPackageId,
        Guid artifactId,
        string? description,
        Guid? createdBy = null)
    {
        return new ArtifactReference
        {
            Id = id,
            TenantId = tenantId,
            ReproducibilityPackageId = reproducibilityPackageId,
            ArtifactId = artifactId,
            Description = description,
            CreatedBy = createdBy
        };
    }
}
