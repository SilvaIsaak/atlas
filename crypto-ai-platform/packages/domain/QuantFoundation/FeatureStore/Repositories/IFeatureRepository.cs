using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Repositories;

public interface IFeatureRepository
{
    Task<Feature?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Feature>> GetAllAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task<Feature?> GetByNameAsync(TenantId tenantId, string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Feature>> GetApprovedAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(Feature feature, CancellationToken cancellationToken = default);
    Task UpdateAsync(Feature feature, CancellationToken cancellationToken = default);
}