namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Base interface for all AI Agents in the system
/// </summary>
public interface IAgent
{
    /// <summary>
    /// Unique identifier for the agent
    /// </summary>
    string Id { get; }
    
    /// <summary>
    /// Name of the agent
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Description of the agent's purpose
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Current status of the agent
    /// </summary>
    AgentStatus Status { get; }
    
    /// <summary>
    /// Initialize the agent
    /// </summary>
    Task InitializeAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Start the agent's execution
    /// </summary>
    Task StartAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Stop the agent's execution
    /// </summary>
    Task StopAsync(CancellationToken cancellationToken = default);
}
