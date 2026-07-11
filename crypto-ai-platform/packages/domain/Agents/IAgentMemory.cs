namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Interface for agent memory storage
/// </summary>
public interface IAgentMemory
{
    /// <summary>
    /// Store a value in memory
    /// </summary>
    Task StoreAsync<T>(string key, T value, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieve a value from memory
    /// </summary>
    Task<T?> RetrieveAsync<T>(string key, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Delete a value from memory
    /// </summary>
    Task DeleteAsync(string key, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Clear all memory for this agent
    /// </summary>
    Task ClearAsync(CancellationToken cancellationToken = default);
}
