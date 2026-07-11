namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Context containing information for agent execution
/// </summary>
public interface IAgentContext
{
    /// <summary>
    /// Tenant identifier
    /// </summary>
    string TenantId { get; }
    
    /// <summary>
    /// User identifier (if applicable)
    /// </summary>
    string? UserId { get; }
    
    /// <summary>
    /// Correlation ID for tracing
    /// </summary>
    string CorrelationId { get; }
    
    /// <summary>
    /// Agent memory for this context
    /// </summary>
    IAgentMemory Memory { get; }
}
