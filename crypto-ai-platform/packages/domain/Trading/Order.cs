using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Trading.Enums;
using CryptoAIPlatform.Domain.Trading.ValueObjects;

namespace CryptoAIPlatform.Domain.Trading;

public class Order : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<OrderFill> _fills = [];
    private readonly List<OrderFee> _fees = [];
    private readonly List<OrderStatusHistory> _statusHistory = [];

    public PortfolioId PortfolioId { get; private set; } = null!;
    public string Symbol { get; private set; } = string.Empty;
    public OrderSide Side { get; private set; }
    public OrderType Type { get; private set; }
    public TimeInForce TimeInForce { get; private set; }
    public OrderQuantity Quantity { get; private set; } = null!;
    public OrderQuantity FilledQuantity { get; private set; } = null!;
    public OrderPrice? Price { get; private set; }
    public StopLoss? StopLoss { get; private set; }
    public TakeProfit? TakeProfit { get; private set; }
    public Leverage? Leverage { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public DateTime? FilledAt { get; private set; }
    public DateTime? CancelledAt { get; private set; }

    public IReadOnlyCollection<OrderFill> Fills => _fills.AsReadOnly();
    public IReadOnlyCollection<OrderFee> Fees => _fees.AsReadOnly();
    public IReadOnlyCollection<OrderStatusHistory> StatusHistory => _statusHistory.AsReadOnly();

    private Order() { }

    public static Order Create(
        Guid id,
        TenantId tenantId,
        PortfolioId portfolioId,
        string symbol,
        OrderSide side,
        OrderType type,
        TimeInForce timeInForce,
        OrderQuantity quantity,
        OrderPrice? price,
        StopLoss? stopLoss,
        TakeProfit? takeProfit,
        Leverage? leverage,
        Guid? createdBy = null)
    {
        return new Order
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Symbol = symbol,
            Side = side,
            Type = type,
            TimeInForce = timeInForce,
            Quantity = quantity,
            FilledQuantity = new OrderQuantity(0),
            Price = price,
            StopLoss = stopLoss,
            TakeProfit = takeProfit,
            Leverage = leverage,
            Status = OrderStatus.Created,
            CreatedBy = createdBy
        };
    }

    public void Submit()
    {
        Status = OrderStatus.Submitted;
        SubmittedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        _statusHistory.Add(OrderStatusHistory.Create(
            Guid.NewGuid(), TenantId, Id, Status, DateTime.UtcNow, "Submitted"));
    }

    public void Cancel(string reason)
    {
        Status = OrderStatus.Cancelled;
        CancelledAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        _statusHistory.Add(OrderStatusHistory.Create(
            Guid.NewGuid(), TenantId, Id, Status, DateTime.UtcNow, reason));
    }

    public void Fill(OrderFill fill)
    {
        _fills.Add(fill);
        FilledQuantity = new OrderQuantity(FilledQuantity.Value + fill.Quantity.Value);
        Status = FilledQuantity.Value >= Quantity.Value 
            ? OrderStatus.Filled 
            : OrderStatus.PartiallyFilled;
        if (Status == OrderStatus.Filled)
            FilledAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        _statusHistory.Add(OrderStatusHistory.Create(
            Guid.NewGuid(), TenantId, Id, Status, DateTime.UtcNow, 
            Status == OrderStatus.Filled ? "Fully filled" : "Partially filled"));
    }

    public void AddFee(OrderFee fee) => _fees.Add(fee);
}
