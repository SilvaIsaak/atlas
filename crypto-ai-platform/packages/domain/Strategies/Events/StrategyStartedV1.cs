using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Strategies.Events;

public class StrategyStartedV1 : DomainEvent
{
    public Guid StrategyId { get; init; }
    public Guid ExecutionId { get; init; }

    public StrategyStartedV1(TenantId tenantId, Guid strategyId, Guid executionId)
    {
        TenantId = tenantId;
        StrategyId = strategyId;
        ExecutionId = executionId;
    }
}

public class StrategyStoppedV1 : DomainEvent
{
    public Guid StrategyId { get; init; }
    public Guid ExecutionId { get; init; }

    public StrategyStoppedV1(TenantId tenantId, Guid strategyId, Guid executionId)
    {
        TenantId = tenantId;
        StrategyId = strategyId;
        ExecutionId = executionId;
    }
}
