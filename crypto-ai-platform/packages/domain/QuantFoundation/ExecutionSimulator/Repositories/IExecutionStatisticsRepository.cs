using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Repositories;

public interface IExecutionStatisticsRepository
{
    Task<ExecutionStatistics?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ExecutionStatistics>> GetBySimulationIdAsync(Guid simulationId, TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(ExecutionStatistics statistics, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExecutionStatistics statistics, CancellationToken cancellationToken = default);
}
