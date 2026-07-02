using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Repositories;

public interface IResearchDatasetRepository
{
    Task<ResearchDataset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResearchDataset>> GetAllAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResearchDataset>> GetByUserIdAsync(TenantId tenantId, Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(ResearchDataset dataset, CancellationToken cancellationToken = default);
    Task UpdateAsync(ResearchDataset dataset, CancellationToken cancellationToken = default);
}