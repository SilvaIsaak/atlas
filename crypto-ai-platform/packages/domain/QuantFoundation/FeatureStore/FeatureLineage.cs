using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;

public class FeatureLineage : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<FeatureLineageNode> _nodes = [];

    public Guid FeatureId { get; private set; }
    public IReadOnlyCollection<FeatureLineageNode> Nodes => _nodes.AsReadOnly();

    private FeatureLineage() { }

    public static FeatureLineage Create(
        Guid id,
        TenantId tenantId,
        Guid featureId,
        IReadOnlyList<FeatureLineageNode> nodes,
        Guid? createdBy = null)
    {
        var lineage = new FeatureLineage
        {
            Id = id,
            TenantId = tenantId,
            FeatureId = featureId,
            CreatedBy = createdBy
        };
        foreach (var node in nodes)
            lineage._nodes.Add(node);
        return lineage;
    }
}