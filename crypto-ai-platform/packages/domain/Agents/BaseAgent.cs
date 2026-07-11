using Microsoft.Extensions.Logging;

namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Base abstract class for all AI Agents
/// </summary>
public abstract class BaseAgent : IAsyncAgent
{
    protected readonly ILogger Logger;
    protected readonly IAgentMemory Memory;
    protected readonly IAgentEventBus EventBus;

    protected BaseAgent(
        string id,
        string name,
        string description,
        ILogger logger,
        IAgentMemory memory,
        IAgentEventBus eventBus)
    {
        Id = id;
        Name = name;
        Description = description;
        Logger = logger;
        Memory = memory;
        EventBus = eventBus;
        Status = AgentStatus.NotInitialized;
    }

    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public AgentStatus Status { get; protected set; }

    public virtual async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Initializing agent {AgentName} ({AgentId})", Name, Id);
        Status = AgentStatus.Idle;
        await PublishAgentEventAsync("agent.initialized", null, cancellationToken);
        Logger.LogInformation("Agent {AgentName} initialized successfully", Name);
    }

    public virtual async Task StartAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Starting agent {AgentName} ({AgentId})", Name, Id);
        Status = AgentStatus.Running;
        await PublishAgentEventAsync("agent.started", null, cancellationToken);
        Logger.LogInformation("Agent {AgentName} started successfully", Name);
    }

    public virtual async Task StopAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("Stopping agent {AgentName} ({AgentId})", Name, Id);
        Status = AgentStatus.Stopped;
        await PublishAgentEventAsync("agent.stopped", null, cancellationToken);
        Logger.LogInformation("Agent {AgentName} stopped successfully", Name);
    }

    public abstract Task<TResult> ExecuteAsync<TTask, TResult>(TTask task, CancellationToken cancellationToken = default)
        where TTask : IAgentTask;

    protected async Task PublishAgentEventAsync(string eventType, object? payload, CancellationToken cancellationToken = default)
    {
        var agentEvent = new AgentEvent
        {
            EventId = Guid.NewGuid().ToString(),
            AgentId = Id,
            Type = eventType,
            OccurredAt = DateTime.UtcNow,
            Payload = payload
        };

        await EventBus.PublishAsync(agentEvent, cancellationToken);
    }
}

/// <summary>
/// Default implementation of IAgentEvent
/// </summary>
public class AgentEvent : IAgentEvent
{
    public string EventId { get; init; } = string.Empty;
    public string AgentId { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public DateTime OccurredAt { get; init; }
    public object? Payload { get; init; }
}
