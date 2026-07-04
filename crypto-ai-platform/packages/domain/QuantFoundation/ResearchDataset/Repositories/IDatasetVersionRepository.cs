using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Repositories;

public interface IDatasetVersionRepository
{
    Task<DatasetVersion?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DatasetVersion>> GetByDatasetIdAsync(Guid datasetId, TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(DatasetVersion version, CancellationToken cancellationToken = default);
    Task UpdateAsync(DatasetVersion version, CancellationToken cancellationToken = default);
}
