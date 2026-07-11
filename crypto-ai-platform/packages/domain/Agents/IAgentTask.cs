namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Base interface for all agent tasks
/// </summary>
public interface IAgentTask
{
    /// <summary>
    /// Unique task identifier
    /// </summary>
    string TaskId { get; }
    
    /// <summary>
    /// Tenant identifier for multi-tenancy
    /// </summary>
    string TenantId { get; }
    
    /// <summary>
    /// Task type name
    /// </summary>
    string Type { get; }
    
    /// <summary>
    /// Timestamp when the task was created
    /// </summary>
    DateTime CreatedAt { get; }
}
