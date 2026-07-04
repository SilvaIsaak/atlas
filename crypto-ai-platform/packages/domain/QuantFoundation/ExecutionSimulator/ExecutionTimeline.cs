using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class ExecutionTimeline : BaseEntity<Guid>
{
    public Guid SimulationId { get; private set; }
    public List<(DateTime Timestamp, string Event, string Details)> Events { get; private set; } = new();

    private ExecutionTimeline() { }

    public static ExecutionTimeline Create(
        Guid id,
        TenantId tenantId,
        Guid simulationId,
        List<(DateTime Timestamp, string Event, string Details)> events,
        Guid? createdBy = null)
    {
        return new ExecutionTimeline
        {
            Id = id,
            TenantId = tenantId,
            SimulationId = simulationId,
            Events = events,
            CreatedBy = createdBy
        };
    }
}
