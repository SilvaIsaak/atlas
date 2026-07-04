using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class ExecutionLatency : BaseEntity<Guid>
{
    public Guid SimulationId { get; private set; }
    public Guid? OrderId { get; private set; }
    public ExecutionLatencyValue Latency { get; private set; } = null!;
    public DateTime Timestamp { get; private set; }

    private ExecutionLatency() { }

    public static ExecutionLatency Create(
        Guid id,
        TenantId tenantId,
        Guid simulationId,
        Guid? orderId,
        ExecutionLatencyValue latency,
        DateTime timestamp,
        Guid? createdBy = null)
    {
        return new ExecutionLatency
        {
            Id = id,
            TenantId = tenantId,
            SimulationId = simulationId,
            OrderId = orderId,
            Latency = latency,
            Timestamp = timestamp,
            CreatedBy = createdBy
        };
    }
}
