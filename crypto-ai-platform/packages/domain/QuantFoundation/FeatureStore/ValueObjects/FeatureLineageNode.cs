namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.ValueObjects;

public record FeatureLineageNode(
    string NodeId,
    string Type,
    IReadOnlyList<string> ParentNodeIds,
    string? Metadata = null);