using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class ExperimentReference : BaseEntity<Guid>
{
    public Guid ReproducibilityPackageId { get; private set; }
    public Guid ExperimentId { get; private set; }
    public string? Description { get; private set; }

    private ExperimentReference() { }

    public static ExperimentReference Create(
        Guid id,
        TenantId tenantId,
        Guid reproducibilityPackageId,
        Guid experimentId,
        string? description,
        Guid? createdBy = null)
    {
        return new ExperimentReference
        {
            Id = id,
            TenantId = tenantId,
            ReproducibilityPackageId = reproducibilityPackageId,
            ExperimentId = experimentId,
            Description = description,
            CreatedBy = createdBy
        };
    }
}
