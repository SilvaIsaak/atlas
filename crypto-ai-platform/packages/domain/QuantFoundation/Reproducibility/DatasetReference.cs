using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;

public class DatasetReference : BaseEntity<Guid>
{
    public Guid ReproducibilityPackageId { get; private set; }
    public Guid DatasetVersionId { get; private set; }
    public string? Description { get; private set; }

    private DatasetReference() { }

    public static DatasetReference Create(
        Guid id,
        TenantId tenantId,
        Guid reproducibilityPackageId,
        Guid datasetVersionId,
        string? description,
        Guid? createdBy = null)
    {
        return new DatasetReference
        {
            Id = id,
            TenantId = tenantId,
            ReproducibilityPackageId = reproducibilityPackageId,
            DatasetVersionId = datasetVersionId,
            Description = description,
            CreatedBy = createdBy
        };
    }
}
