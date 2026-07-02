using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class ExecutionSimulation : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<SimulatedOrder> _orders = [];

    public string Name { get; private set; } = string.Empty;
    public Guid? MicrostructureModelId { get; private set; }
    public ExecutionSimulationStatus Status { get; private set; }

    public IReadOnlyCollection<SimulatedOrder> Orders => _orders.AsReadOnly();

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
    }

    public void Complete()
    {
        Status = ExecutionSimulationStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        Status = ExecutionSimulationStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddOrder(SimulatedOrder order)
    {
        _orders.Add(order);
    }
}