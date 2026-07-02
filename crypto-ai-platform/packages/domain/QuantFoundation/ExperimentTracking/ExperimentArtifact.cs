using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;

public class ExperimentArtifact : BaseEntity<Guid>
{
    public Guid ExperimentRunId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ExperimentArtifactType Type { get; private set; }
    public string StoragePath { get; private set; } = string.Empty;

    private ExperimentArtifact() { }

    public static ExperimentArtifact Create(
        Guid id,
        TenantId tenantId,
        Guid experimentRunId,
        string name,
        ExperimentArtifactType type,
        string storagePath,
        Guid? createdBy = null)
    {
        return new ExperimentArtifact
        {
            Id = id,
            TenantId = tenantId,
            ExperimentRunId = experimentRunId,
            Name = name,
            Type = type,
            StoragePath = storagePath,
            CreatedBy = createdBy
        };
    }
}