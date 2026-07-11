using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Agents;
using System.Collections.Concurrent;

namespace CryptoAIPlatform.Infrastructure.Agents;

public class InMemoryAgentEventBus : IAgentEventBus
{
    private readonly ConcurrentDictionary<Type, List<Func<object, Task>>> _handlers = new();
    private readonly ILogger<InMemoryAgentEventBus> _logger;

    public InMemoryAgentEventBus(ILogger<InMemoryAgentEventBus> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync(IAgentEvent agentEvent, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Publishing event {EventType} from agent {AgentId}", agentEvent.Type, agentEvent.AgentId);
        
        var eventType = agentEvent.GetType();
        if (_handlers.TryGetValue(eventType, out var handlers))
        {
            foreach (var handler in handlers)
            {
                _ = handler(agentEvent); // Fire and forget
            }
        }

        return Task.CompletedTask;
    }

    public Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler, CancellationToken cancellationToken = default) where TEvent : IAgentEvent
    {
        var eventType = typeof(TEvent);
        var handlers = _handlers.GetOrAdd(eventType, _ => new List<Func<object, Task>>());
        handlers.Add(async (evt) => await handler((TEvent)evt));
        
        _logger.LogInformation("Subscribed to event type {EventType}", eventType.Name);
        return Task.CompletedTask;
    }
}
