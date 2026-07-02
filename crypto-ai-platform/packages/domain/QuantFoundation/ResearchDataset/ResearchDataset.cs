using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class ResearchDataset : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<DatasetVersion> _versions = [];

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid OwnerId { get; private set; }
    public string Version { get; private set; } = string.Empty;
    public bool IsImmutable { get; private set; } = true;

    public IReadOnlyCollection<DatasetVersion> Versions => _versions.AsReadOnly();

    private ResearchDataset() { }

    public static ResearchDataset Create(
        Guid id,
        TenantId tenantId,
        string name,
        string description,
        Guid ownerId,
        string version,
        bool isImmutable = true,
        Guid? createdBy = null)
    {
        return new ResearchDataset
        {
            Id = id,
            TenantId = tenantId,
            Name = name,
            Description = description,
            OwnerId = ownerId,
            Version = version,
            IsImmutable = isImmutable,
            CreatedBy = createdBy
        };
    }

    public void AddVersion(DatasetVersion version)
    {
        _versions.Add(version);
    }
}