namespace CryptoAIPlatform.Domain.Risk.Enums;

public enum RiskStatus
{
    Green,
    Yellow,
    Orange,
    Red,
    Critical
}

public enum RiskSeverity
{
    Low,
    Medium,
    High,
    Critical
}

public enum RiskType
{
    Market,
    Credit,
    Liquidity,
    Operational,
    Concentration,
    Margin
}

public enum RiskAction
{
    Allow,
    Warn,
    Reject,
    ForceClose
}

public enum MarginType
{
    Initial,
    Maintenance
}

public enum ExposureType
{
    Long,
    Short,
    Net
}

public enum ViolationStatus
{
    Open,
    Resolved,
    Acknowledged,
    Dismissed
}
