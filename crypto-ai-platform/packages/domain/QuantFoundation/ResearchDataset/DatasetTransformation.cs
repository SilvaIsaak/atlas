using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class DatasetTransformation : BaseEntity<Guid>
{
    public Guid DatasetVersionId { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;

    private DatasetTransformation() { }

    public static DatasetTransformation Create(
        Guid id,
        TenantId tenantId,
        Guid datasetVersionId,
        string type,
        string code,
        Guid? createdBy = null)
    {
        return new DatasetTransformation
        {
            Id = id,
            TenantId = tenantId,
            DatasetVersionId = datasetVersionId,
            Type = type,
            Code = code,
            CreatedBy = createdBy
        };
    }
}