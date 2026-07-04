using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.ValueObjects;

namespace CryptoAIPlatform.Infrastructure.QuantFoundation;

public interface IExecutionSimulatorService
{
    Task<ExecutionSimulation> RunSimulationAsync(TenantId tenantId, Guid simulationId, CancellationToken cancellationToken = default);
}

public class ExecutionSimulatorService : IExecutionSimulatorService
{
    private readonly IExecutionSimulationRepository _simulationRepo;
    private readonly ILogger<ExecutionSimulatorService> _logger;

    public ExecutionSimulatorService(
        IExecutionSimulationRepository simulationRepo,
        ILogger<ExecutionSimulatorService> logger)
    {
        _simulationRepo = simulationRepo;
        _logger = logger;
    }

    public async Task<ExecutionSimulation> RunSimulationAsync(TenantId tenantId, Guid simulationId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Running simulation: {Id}", simulationId);

        var sim = await _simulationRepo.GetByIdAsync(simulationId, cancellationToken);
        if (sim is null)
            throw new KeyNotFoundException($"Simulation {simulationId} not found");

        sim.Start();

        // Placeholder for actual simulation logic
        sim.Complete();

        await _simulationRepo.UpdateAsync(sim, cancellationToken);
        return sim;
    }
}
