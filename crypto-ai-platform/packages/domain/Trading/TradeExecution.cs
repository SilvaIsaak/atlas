using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Trading.Enums;
using CryptoAIPlatform.Domain.Trading.ValueObjects;

namespace CryptoAIPlatform.Domain.Trading;

public class TradeExecution : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<OrderFill> _fills = [];
    private readonly List<OrderFee> _fees = [];

    public Guid OrderId { get; private set; }
    public ExecutionStatus Status { get; private set; }
    public Slippage? Slippage { get; private set; }
    public Latency? Latency { get; private set; }
    public ExecutionCost? Cost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public IReadOnlyCollection<OrderFill> Fills => _fills.AsReadOnly();
    public IReadOnlyCollection<OrderFee> Fees => _fees.AsReadOnly();

    private TradeExecution() { }

    public static TradeExecution Create(
        Guid id,
        TenantId tenantId,
        Guid orderId,
        Guid? createdBy = null)
    {
        return new TradeExecution
        {
            Id = id,
            TenantId = tenantId,
            OrderId = orderId,
            Status = ExecutionStatus.Pending,
            CreatedBy = createdBy
        };
    }

    public void Start()
    {
        Status = ExecutionStatus.Executing;
        StartedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete(Slippage slippage, Latency latency, ExecutionCost cost)
    {
        Status = ExecutionStatus.Completed;
        Slippage = slippage;
        Latency = latency;
        Cost = cost;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        Status = ExecutionStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddFill(OrderFill fill) => _fills.Add(fill);
    public void AddFee(OrderFee fee) => _fees.Add(fee);
}

public record ExecutionCost(decimal Total, decimal Fees, decimal Slippage);
