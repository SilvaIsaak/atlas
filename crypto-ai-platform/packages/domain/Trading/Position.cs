using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Trading.Enums;
using CryptoAIPlatform.Domain.Trading.ValueObjects;

namespace CryptoAIPlatform.Domain.Trading;

public class Position : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<PositionLeg> _legs = [];
    private readonly List<PositionPnL> _pnls = [];

    public PortfolioId PortfolioId { get; private set; } = null!;
    public string Symbol { get; private set; } = string.Empty;
    public OrderQuantity Quantity { get; private set; } = null!;
    public OrderPrice EntryPrice { get; private set; } = null!;
    public OrderPrice? CurrentPrice { get; private set; }
    public Leverage Leverage { get; private set; } = null!;
    public Margin Margin { get; private set; } = null!;
    public StopLoss? StopLoss { get; private set; }
    public TakeProfit? TakeProfit { get; private set; }
    public PositionStatus Status { get; private set; }
    public DateTime? OpenedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }

    public IReadOnlyCollection<PositionLeg> Legs => _legs.AsReadOnly();
    public IReadOnlyCollection<PositionPnL> PnLs => _pnls.AsReadOnly();

    private Position() { }

    public static Position Create(
        Guid id,
        TenantId tenantId,
        PortfolioId portfolioId,
        string symbol,
        OrderQuantity quantity,
        OrderPrice entryPrice,
        Leverage leverage,
        Margin margin,
        StopLoss? stopLoss,
        TakeProfit? takeProfit,
        Guid? createdBy = null)
    {
        return new Position
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Symbol = symbol,
            Quantity = quantity,
            EntryPrice = entryPrice,
            Leverage = leverage,
            Margin = margin,
            StopLoss = stopLoss,
            TakeProfit = takeProfit,
            Status = PositionStatus.Open,
            OpenedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };
    }

    public void AddLeg(PositionLeg leg) => _legs.Add(leg);
    public void AddPnL(PositionPnL pnl) => _pnls.Add(pnl);
    public void Close()
    {
        Status = PositionStatus.Closed;
        ClosedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class PositionLeg : BaseEntity<Guid>
{
    public Guid PositionId { get; private set; }
    public OrderPrice Price { get; private set; } = null!;
    public OrderQuantity Quantity { get; private set; } = null!;
    public DateTime Timestamp { get; private set; }

    private PositionLeg() { }

    public static PositionLeg Create(
        Guid id,
        TenantId tenantId,
        Guid positionId,
        OrderPrice price,
        OrderQuantity quantity,
        DateTime timestamp,
        Guid? createdBy = null)
    {
        return new PositionLeg
        {
            Id = id,
            TenantId = tenantId,
            PositionId = positionId,
            Price = price,
            Quantity = quantity,
            Timestamp = timestamp,
            CreatedBy = createdBy
        };
    }
}

public class PositionPnL : BaseEntity<Guid>
{
    public Guid PositionId { get; private set; }
    public PnL Value { get; private set; } = null!;
    public DateTime Timestamp { get; private set; }

    private PositionPnL() { }

    public static PositionPnL Create(
        Guid id,
        TenantId tenantId,
        Guid positionId,
        PnL value,
        DateTime timestamp,
        Guid? createdBy = null)
    {
        return new PositionPnL
        {
            Id = id,
            TenantId = tenantId,
            PositionId = positionId,
            Value = value,
            Timestamp = timestamp,
            CreatedBy = createdBy
        };
    }
}
