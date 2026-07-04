using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Repositories;

public interface IExecutionManifestRepository
{
    Task<ExecutionManifest?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExecutionManifest>> GetByPackageIdAsync(Guid packageId, TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(ExecutionManifest manifest, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExecutionManifest manifest, CancellationToken cancellationToken = default);
}
