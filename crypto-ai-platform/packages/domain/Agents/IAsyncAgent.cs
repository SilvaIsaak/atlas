namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Interface for agents that process tasks asynchronously
/// </summary>
public interface IAsyncAgent : IAgent
{
    /// <summary>
    /// Execute an async task
    /// </summary>
    Task<TResult> ExecuteAsync<TTask, TResult>(TTask task, CancellationToken cancellationToken = default) 
        where TTask : IAgentTask;
}
