namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Status of an AI Agent
/// </summary>
public enum AgentStatus
{
    /// <summary>
    /// Agent is not initialized
    /// </summary>
    NotInitialized,
    
    /// <summary>
    /// Agent is idle and waiting for tasks
    /// </summary>
    Idle,
    
    /// <summary>
    /// Agent is currently processing a task
    /// </summary>
    Running,
    
    /// <summary>
    /// Agent is paused
    /// </summary>
    Paused,
    
    /// <summary>
    /// Agent has encountered an error
    /// </summary>
    Error,
    
    /// <summary>
    /// Agent is stopped
    /// </summary>
    Stopped
}
