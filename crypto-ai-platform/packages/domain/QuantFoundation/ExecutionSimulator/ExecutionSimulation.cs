using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Events;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class ExecutionSimulation : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<SimulatedOrder> _orders = [];
    private readonly List<ExecutionFee> _fees = [];
    private readonly List<ExecutionLatency> _latencies = [];
    private readonly List<ExecutionSlippage> _slippages = [];
    private readonly List<ExecutionStatistics> _statistics = [];
    private readonly List<ExecutionTimeline> _timelines = [];

    public string Name { get; private set; } = string.Empty;
    public Guid? MicrostructureModelId { get; private set; }
    public ExecutionSimulationStatus Status { get; private set; }

    public IReadOnlyCollection<SimulatedOrder> Orders => _orders.AsReadOnly();
    public IReadOnlyCollection<ExecutionFee> Fees => _fees.AsReadOnly();
    public IReadOnlyCollection<ExecutionLatency> Latencies => _latencies.AsReadOnly();
    public IReadOnlyCollection<ExecutionSlippage> Slippages => _slippages.AsReadOnly();
    public IReadOnlyCollection<ExecutionStatistics> Statistics => _statistics.AsReadOnly();
    public IReadOnlyCollection<ExecutionTimeline> Timelines => _timelines.AsReadOnly();

    private ExecutionSimulation() { }

    public static ExecutionSimulation Create(
        Guid id,
        TenantId tenantId,
        string name,
        Guid? microstructureModelId = null,
        Guid? createdBy = null)
    {
        return new ExecutionSimulation
        {
            Id = id,
            TenantId = tenantId,
            Name = name,
            MicrostructureModelId = microstructureModelId,
            Status = ExecutionSimulationStatus.Draft,
            CreatedBy = createdBy
        };
    }

    public void Start()
    {
        Status = ExecutionSimulationStatus.Running;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new ExecutionSimulationStartedV1(TenantId, Id));
    }

    public void Complete()
    {
        Status = ExecutionSimulationStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail(string reason)
    {
        Status = ExecutionSimulationStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new ExecutionSimulationFailedV1(TenantId, Id, reason));
    }

    public void AddOrder(SimulatedOrder order) => _orders.Add(order);
    public void AddFee(ExecutionFee fee) => _fees.Add(fee);
    public void AddLatency(ExecutionLatency latency) => _latencies.Add(latency);
    public void AddSlippage(ExecutionSlippage slippage) => _slippages.Add(slippage);
    public void AddStatistics(ExecutionStatistics stats)
    {
        _statistics.Add(stats);
        AddDomainEvent(new ExecutionStatisticsGeneratedV1(TenantId, Id, stats.Id));
    }
    public void AddTimeline(ExecutionTimeline timeline) => _timelines.Add(timeline);
}