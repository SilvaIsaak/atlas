using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Events;

public class ExecutionSimulationStartedV1 : DomainEvent
{
    public Guid SimulationId { get; init; }

    public ExecutionSimulationStartedV1(TenantId tenantId, Guid simulationId)
    {
        TenantId = tenantId;
        SimulationId = simulationId;
    }
}

public class ExecutionSimulationFailedV1 : DomainEvent
{
    public Guid SimulationId { get; init; }
    public string Reason { get; init; } = string.Empty;

    public ExecutionSimulationFailedV1(TenantId tenantId, Guid simulationId, string reason)
    {
        TenantId = tenantId;
        SimulationId = simulationId;
        Reason = reason;
    }
}

public class ExecutionFillCreatedV1 : DomainEvent
{
    public Guid SimulationId { get; init; }
    public Guid FillId { get; init; }
    public Guid OrderId { get; init; }

    public ExecutionFillCreatedV1(TenantId tenantId, Guid simulationId, Guid fillId, Guid orderId)
    {
        TenantId = tenantId;
        SimulationId = simulationId;
        FillId = fillId;
        OrderId = orderId;
    }
}

public class ExecutionStatisticsGeneratedV1 : DomainEvent
{
    public Guid SimulationId { get; init; }
    public Guid StatisticsId { get; init; }

    public ExecutionStatisticsGeneratedV1(TenantId tenantId, Guid simulationId, Guid statisticsId)
    {
        TenantId = tenantId;
        SimulationId = simulationId;
        StatisticsId = statisticsId;
    }
}
