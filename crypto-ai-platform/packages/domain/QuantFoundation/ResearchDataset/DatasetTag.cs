using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class DatasetTag : BaseEntity<Guid>
{
    public Guid DatasetId { get; private set; }
    public string Tag { get; private set; } = string.Empty;

    private DatasetTag() { }

    public static DatasetTag Create(
        Guid id,
        TenantId tenantId,
        Guid datasetId,
        string tag,
        Guid? createdBy = null)
    {
        return new DatasetTag
        {
            Id = id,
            TenantId = tenantId,
            DatasetId = datasetId,
            Tag = tag,
            CreatedBy = createdBy
        };
    }
}
