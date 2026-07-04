namespace CryptoAIPlatform.Domain.Trading.Enums;

public enum OrderStatus
{
    Created,
    Submitted,
    PartiallyFilled,
    Filled,
    Cancelled,
    Rejected,
    Expired
}

public enum OrderSide
{
    Buy,
    Sell
}

public enum OrderType
{
    Market,
    Limit,
    Stop,
    StopLimit,
    TakeProfit,
    TakeProfitLimit
}

public enum TimeInForce
{
    GTC,
    IOC,
    FOK,
    GTD
}

public enum PositionStatus
{
    Open,
    Closed,
    PartiallyClosed
}

public enum PortfolioStatus
{
    Active,
    Suspended,
    Closed
}

public enum RiskLevel
{
    Low,
    Medium,
    High,
    Critical
}

public enum ExecutionStatus
{
    Pending,
    Executing,
    Completed,
    Failed
}
