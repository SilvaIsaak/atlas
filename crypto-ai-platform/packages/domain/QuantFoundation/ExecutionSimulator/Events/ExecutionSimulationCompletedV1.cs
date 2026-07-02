using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Events;

public class ExecutionSimulationCompletedV1 : DomainEvent
{
    public Guid SimulationId { get; init; }
    public string Status { get; init; } = string.Empty;

    public ExecutionSimulationCompletedV1(TenantId tenantId, Guid simulationId, string status)
    {
        TenantId = tenantId;
        SimulationId = simulationId;
        Status = status;
    }
}