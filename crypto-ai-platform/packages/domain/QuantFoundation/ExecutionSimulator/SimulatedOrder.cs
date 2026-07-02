using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.Enums;

namespace CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;

public class SimulatedOrder : BaseEntity<Guid>
{
    private readonly List<SimulatedFill> _fills = [];

    public Guid SimulationId { get; private set; }
    public SimulatedOrderType Type { get; private set; }
    public string Symbol { get; private set; } = string.Empty;
    public SimulatedOrderSide Side { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal? Price { get; private set; }
    public SimulatedOrderStatus Status { get; private set; }

    public IReadOnlyCollection<SimulatedFill> Fills => _fills.AsReadOnly();

    private SimulatedOrder() { }

    public static SimulatedOrder Create(
        Guid id,
        TenantId tenantId,
        Guid simulationId,
        SimulatedOrderType type,
        string symbol,
        SimulatedOrderSide side,
        decimal quantity,
        decimal? price = null,
        Guid? createdBy = null)
    {
        return new SimulatedOrder
        {
            Id = id,
            TenantId = tenantId,
            SimulationId = simulationId,
            Type = type,
            Symbol = symbol,
            Side = side,
            Quantity = quantity,
            Price = price,
            Status = SimulatedOrderStatus.Pending,
            CreatedBy = createdBy
        };
    }

    public void AddFill(SimulatedFill fill)
    {
        _fills.Add(fill);
        // Update status based on fills
        var totalFilled = _fills.Sum(f => f.Quantity);
        if (totalFilled >= Quantity)
        {
            Status = SimulatedOrderStatus.Filled;
        }
        else if (totalFilled > 0)
        {
            Status = SimulatedOrderStatus.PartiallyFilled;
        }
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        Status = SimulatedOrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        Status = SimulatedOrderStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }
}