using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.Trading.Enums;
using CryptoAIPlatform.Domain.Trading.ValueObjects;

namespace CryptoAIPlatform.Domain.Trading;

public class Portfolio : BaseEntity<Guid>, IAggregateRoot
{
    private readonly List<PortfolioAsset> _assets = [];
    private readonly List<PortfolioBalance> _balances = [];

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public PortfolioStatus Status { get; private set; }

    public IReadOnlyCollection<PortfolioAsset> Assets => _assets.AsReadOnly();
    public IReadOnlyCollection<PortfolioBalance> Balances => _balances.AsReadOnly();

    private Portfolio() { }

    public static Portfolio Create(
        Guid id,
        TenantId tenantId,
        string name,
        string? description,
        Guid? createdBy = null)
    {
        return new Portfolio
        {
            Id = id,
            TenantId = tenantId,
            Name = name,
            Description = description,
            Status = PortfolioStatus.Active,
            CreatedBy = createdBy
        };
    }

    public void AddAsset(PortfolioAsset asset) => _assets.Add(asset);
    public void AddBalance(PortfolioBalance balance) => _balances.Add(balance);
}

public class PortfolioAsset : BaseEntity<Guid>
{
    public Guid PortfolioId { get; private set; }
    public string Symbol { get; private set; } = string.Empty;
    public OrderQuantity Quantity { get; private set; } = null!;
    public OrderPrice? AvgEntryPrice { get; private set; }

    private PortfolioAsset() { }

    public static PortfolioAsset Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        string symbol,
        OrderQuantity quantity,
        OrderPrice? avgEntryPrice,
        Guid? createdBy = null)
    {
        return new PortfolioAsset
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Symbol = symbol,
            Quantity = quantity,
            AvgEntryPrice = avgEntryPrice,
            CreatedBy = createdBy
        };
    }
}

public class PortfolioBalance : BaseEntity<Guid>
{
    public Guid PortfolioId { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    public decimal Available { get; private set; }
    public decimal Locked { get; private set; }
    public DateTime Timestamp { get; private set; }

    private PortfolioBalance() { }

    public static PortfolioBalance Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        string currency,
        decimal available,
        decimal locked,
        DateTime timestamp,
        Guid? createdBy = null)
    {
        return new PortfolioBalance
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Currency = currency,
            Available = available,
            Locked = locked,
            Timestamp = timestamp,
            CreatedBy = createdBy
        };
    }
}

public class PortfolioSnapshot : BaseEntity<Guid>, IAggregateRoot
{
    public Guid PortfolioId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public decimal TotalEquity { get; private set; }
    public decimal TotalMarginUsed { get; private set; }
    public decimal TotalPnL { get; private set; }

    private PortfolioSnapshot() { }

    public static PortfolioSnapshot Create(
        Guid id,
        TenantId tenantId,
        Guid portfolioId,
        DateTime timestamp,
        decimal totalEquity,
        decimal totalMarginUsed,
        decimal totalPnL,
        Guid? createdBy = null)
    {
        return new PortfolioSnapshot
        {
            Id = id,
            TenantId = tenantId,
            PortfolioId = portfolioId,
            Timestamp = timestamp,
            TotalEquity = totalEquity,
            TotalMarginUsed = totalMarginUsed,
            TotalPnL = totalPnL,
            CreatedBy = createdBy
        };
    }
}
