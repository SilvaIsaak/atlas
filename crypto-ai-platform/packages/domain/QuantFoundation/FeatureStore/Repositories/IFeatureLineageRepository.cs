namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Repositories;

public interface IFeatureLineageRepository
{
    Task<FeatureLineage?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<FeatureLineage?> GetByFeatureIdAsync(Guid featureId, CancellationToken cancellationToken = default);
    Task AddAsync(FeatureLineage lineage, CancellationToken cancellationToken = default);
}