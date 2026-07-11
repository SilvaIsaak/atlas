namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Interface for agent registry
/// </summary>
public interface IAgentRegistry
{
    /// <summary>
    /// Register an agent
    /// </summary>
    Task RegisterAsync(IAgent agent, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Unregister an agent
    /// </summary>
    Task UnregisterAsync(string agentId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get an agent by ID
    /// </summary>
    Task<IAgent?> GetAgentAsync(string agentId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get all registered agents
    /// </summary>
    Task<IEnumerable<IAgent>> GetAllAgentsAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get agents by type
    /// </summary>
    Task<IEnumerable<IAgent>> GetAgentsByTypeAsync<TAgent>(CancellationToken cancellationToken = default) 
        where TAgent : IAgent;
}
