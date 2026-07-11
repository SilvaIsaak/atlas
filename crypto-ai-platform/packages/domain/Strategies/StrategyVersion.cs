using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;

namespace CryptoAIPlatform.Domain.Strategies;

public class StrategyVersion : BaseEntity<Guid>
{
    public Guid StrategyId { get; private set; }
    public string Version { get; private set; } = string.Empty;
    public string ConfigJson { get; private set; } = string.Empty;
    public DateTime DeployedAt { get; private set; }

    private StrategyVersion() { }

    public static StrategyVersion Create(
        Guid id,
        TenantId tenantId,
        Guid strategyId,
        string version,
        string configJson,
        DateTime deployedAt,
        Guid? createdBy = null)
    {
        return new StrategyVersion
        {
            Id = id,
            TenantId = tenantId,
            StrategyId = strategyId,
            Version = version,
            ConfigJson = configJson,
            DeployedAt = deployedAt,
            CreatedBy = createdBy
        };
    }
}

public class StrategyExecution : BaseEntity<Guid>
{
    public Guid StrategyId { get; private set; }
    public Guid? VersionId { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? EndedAt { get; private set; }
    public string Status { get; private set; } = string.Empty;

    private StrategyExecution() { }

    public static StrategyExecution Create(
        Guid id,
        TenantId tenantId,
        Guid strategyId,
        Guid? versionId,
        DateTime startedAt,
        string status,
        Guid? createdBy = null)
    {
        return new StrategyExecution
        {
            Id = id,
            TenantId = tenantId,
            StrategyId = strategyId,
            VersionId = versionId,
            StartedAt = startedAt,
            Status = status,
            CreatedBy = createdBy
        };
    }
}

public class StrategyResult : BaseEntity<Guid>
{
    public Guid ExecutionId { get; private set; }
    public decimal TotalReturn { get; private set; }
    public int TradesCount { get; private set; }
    public DateTime CalculatedAt { get; private set; }

    private StrategyResult() { }

    public static StrategyResult Create(
        Guid id,
        TenantId tenantId,
        Guid executionId,
        decimal totalReturn,
        int tradesCount,
        DateTime calculatedAt,
        Guid? createdBy = null)
    {
        return new StrategyResult
        {
            Id = id,
            TenantId = tenantId,
            ExecutionId = executionId,
            TotalReturn = totalReturn,
            TradesCount = tradesCount,
            CalculatedAt = calculatedAt,
            CreatedBy = createdBy
        };
    }
}

public class StrategySignal : BaseEntity<Guid>
{
    public Guid StrategyId { get; private set; }
    public string Symbol { get; private set; } = string.Empty;
    public string SignalType { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public DateTime GeneratedAt { get; private set; }

    private StrategySignal() { }

    public static StrategySignal Create(
        Guid id,
        TenantId tenantId,
        Guid strategyId,
        string symbol,
        string signalType,
        decimal price,
        DateTime generatedAt,
        Guid? createdBy = null)
    {
        return new StrategySignal
        {
            Id = id,
            TenantId = tenantId,
            StrategyId = strategyId,
            Symbol = symbol,
            SignalType = signalType,
            Price = price,
            GeneratedAt = generatedAt,
            CreatedBy = createdBy
        };
    }
}
