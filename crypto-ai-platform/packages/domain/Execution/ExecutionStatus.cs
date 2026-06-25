namespace CryptoAIPlatform.Domain.Execution;

public enum ExecutionStatus
{
    Pending,
    Submitted,
    PartiallyFilled,
    Filled,
    Cancelled,
    Rejected,
    Failed
}