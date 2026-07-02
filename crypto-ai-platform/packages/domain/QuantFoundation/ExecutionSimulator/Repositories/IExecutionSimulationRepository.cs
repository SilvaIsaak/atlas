using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Repositories;

public interface IExecutionSimulationRepository
{
    Task<ExecutionSimulation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExecutionSimulation>> GetAllAsync(TenantId tenantId, CancellationToken cancellationToken = default);
    Task AddAsync(ExecutionSimulation simulation, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExecutionSimulation simulation, CancellationToken cancellationToken = default);
}