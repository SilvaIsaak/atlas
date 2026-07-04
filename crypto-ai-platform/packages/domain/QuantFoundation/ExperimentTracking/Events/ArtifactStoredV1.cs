using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Events;

public class ArtifactStoredV1 : DomainEvent
{
    public Guid ExperimentRunId { get; init; }
    public string Name { get; init; } = string.Empty;
    public ExperimentArtifactType Type { get; init; }
    public string StoragePath { get; init; } = string.Empty;

    public ArtifactStoredV1(TenantId tenantId, Guid experimentRunId, string name, ExperimentArtifactType type, string storagePath)
    {
        TenantId = tenantId;
        ExperimentRunId = experimentRunId;
        Name = name;
        Type = type;
        StoragePath = storagePath;
    }
}
