using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Repositories;

public interface IExperimentRepository
{
    Task<Experiment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Experiment>> GetByUserIdAsync(TenantId tenantId, Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Experiment experiment, CancellationToken cancellationToken = default);
    Task UpdateAsync(Experiment experiment, CancellationToken cancellationToken = default);
}