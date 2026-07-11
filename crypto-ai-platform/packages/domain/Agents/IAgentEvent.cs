namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Interface for agent events
/// </summary>
public interface IAgentEvent
{
    /// <summary>
    /// Event identifier
    /// </summary>
    string EventId { get; }
    
    /// <summary>
    /// Agent identifier that emitted the event
    /// </summary>
    string AgentId { get; }
    
    /// <summary>
    /// Event type
    /// </summary>
    string Type { get; }
    
    /// <summary>
    /// Timestamp when the event occurred
    /// </summary>
    DateTime OccurredAt { get; }
    
    /// <summary>
    /// Event payload data
    /// </summary>
    object? Payload { get; }
}
