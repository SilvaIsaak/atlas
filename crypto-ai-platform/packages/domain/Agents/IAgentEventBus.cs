namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Interface for agent event bus
/// </summary>
public interface IAgentEventBus
{
    /// <summary>
    /// Publish an agent event
    /// </summary>
    Task PublishAsync(IAgentEvent agentEvent, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Subscribe to agent events
    /// </summary>
    Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler, CancellationToken cancellationToken = default) 
        where TEvent : IAgentEvent;
}
