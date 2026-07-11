namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Interface for tools that agents can use
/// </summary>
public interface IAgentTool
{
    /// <summary>
    /// Unique tool identifier
    /// </summary>
    string Id { get; }
    
    /// <summary>
    /// Tool name
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Tool description
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Execute the tool
    /// </summary>
    Task<TResult> ExecuteAsync<TInput, TResult>(TInput input, CancellationToken cancellationToken = default);
}
