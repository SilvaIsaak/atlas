using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Events;

public class DatasetValidatedV1 : DomainEvent
{
    public Guid DatasetId { get; init; }
    public Guid VersionId { get; init; }
    public bool IsValid { get; init; }
    public string? ValidationError { get; init; }

    public DatasetValidatedV1(TenantId tenantId, Guid datasetId, Guid versionId, bool isValid, string? validationError = null)
    {
        TenantId = tenantId;
        DatasetId = datasetId;
        VersionId = versionId;
        IsValid = isValid;
        ValidationError = validationError;
    }
}
