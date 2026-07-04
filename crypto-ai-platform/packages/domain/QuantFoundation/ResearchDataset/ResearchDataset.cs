using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Events;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;

public class ResearchDataset : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<DatasetVersion> _versions = [];
    private readonly List<DatasetTag> _tags = [];

    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid OwnerId { get; private set; }
    public string Version { get; private set; } = string.Empty;
    public bool IsImmutable { get; private set; } = true;

    public IReadOnlyCollection<DatasetVersion> Versions => _versions.AsReadOnly();
    public IReadOnlyCollection<DatasetTag> Tags => _tags.AsReadOnly();

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
        var dataset = new ResearchDataset
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

        dataset.AddDomainEvent(new DatasetCreatedV1(tenantId, id, name, ownerId));

        return dataset;
    }

    public void AddVersion(DatasetVersion version)
    {
        _versions.Add(version);
    }

    public void AddTag(DatasetTag tag)
    {
        _tags.Add(tag);
    }
}