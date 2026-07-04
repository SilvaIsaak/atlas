using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class DatasetSchema : BaseEntity<Guid>
{
    public Guid DatasetVersionId { get; private set; }
    public string SchemaJson { get; private set; } = string.Empty;

    private DatasetSchema() { }

    public static DatasetSchema Create(
        Guid id,
        TenantId tenantId,
        Guid datasetVersionId,
        string schemaJson,
        Guid? createdBy = null)
    {
        return new DatasetSchema
        {
            Id = id,
            TenantId = tenantId,
            DatasetVersionId = datasetVersionId,
            SchemaJson = schemaJson,
            CreatedBy = createdBy
        };
    }
}
