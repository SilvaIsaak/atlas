using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Agents;
using System.Collections.Concurrent;

namespace CryptoAIPlatform.Infrastructure.Agents;

public class InMemoryAgentRegistry : IAgentRegistry
{
    private readonly ConcurrentDictionary<string, IAgent> _agents = new();
    private readonly ILogger<InMemoryAgentRegistry> _logger;

    public InMemoryAgentRegistry(ILogger<InMemoryAgentRegistry> logger)
    {
        _logger = logger;
    }

    public Task RegisterAsync(IAgent agent, CancellationToken cancellationToken = default)
    {
        if (_agents.TryAdd(agent.Id, agent))
        {
            _logger.LogInformation("Agent {AgentName} ({AgentId}) registered successfully", agent.Name, agent.Id);
        }
        else
        {
            _logger.LogWarning("Agent {AgentId} is already registered", agent.Id);
        }
        return Task.CompletedTask;
    }

    public Task UnregisterAsync(string agentId, CancellationToken cancellationToken = default)
    {
        if (_agents.TryRemove(agentId, out _))
        {
            _logger.LogInformation("Agent {AgentId} unregistered successfully", agentId);
        }
        return Task.CompletedTask;
    }

    public Task<IAgent?> GetAgentAsync(string agentId, CancellationToken cancellationToken = default)
    {
        _agents.TryGetValue(agentId, out var agent);
        return Task.FromResult(agent);
    }

    public Task<IEnumerable<IAgent>> GetAllAgentsAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_agents.Values.AsEnumerable());
    }

    public Task<IEnumerable<IAgent>> GetAgentsByTypeAsync<TAgent>(CancellationToken cancellationToken = default) where TAgent : IAgent
    {
        var agents = _agents.Values.OfType<TAgent>().Cast<IAgent>();
        return Task.FromResult(agents);
    }
}
