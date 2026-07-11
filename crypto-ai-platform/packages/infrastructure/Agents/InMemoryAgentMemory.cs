using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Agents;
using System.Collections.Concurrent;
using System.Text.Json;

namespace CryptoAIPlatform.Infrastructure.Agents;

public class InMemoryAgentMemory : IAgentMemory
{
    private readonly ConcurrentDictionary<string, string> _storage = new();
    private readonly ILogger<InMemoryAgentMemory> _logger;
    private readonly string _agentId;

    public InMemoryAgentMemory(string agentId, ILogger<InMemoryAgentMemory> logger)
    {
        _agentId = agentId;
        _logger = logger;
    }

    public Task StoreAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        var serialized = JsonSerializer.Serialize(value);
        _storage[key] = serialized;
        _logger.LogDebug("Stored value for key {Key} in agent {AgentId} memory", key, _agentId);
        return Task.CompletedTask;
    }

    public Task<T?> RetrieveAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        if (_storage.TryGetValue(key, out var serialized))
        {
            try
            {
                var value = JsonSerializer.Deserialize<T>(serialized);
                _logger.LogDebug("Retrieved value for key {Key} from agent {AgentId} memory", key, _agentId);
                return Task.FromResult(value);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to deserialize value for key {Key}", key);
            }
        }
        return Task.FromResult(default(T));
    }

    public Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        _storage.TryRemove(key, out _);
        _logger.LogDebug("Deleted key {Key} from agent {AgentId} memory", key, _agentId);
        return Task.CompletedTask;
    }

    public Task ClearAsync(CancellationToken cancellationToken = default)
    {
        _storage.Clear();
        _logger.LogInformation("Cleared all memory for agent {AgentId}", _agentId);
        return Task.CompletedTask;
    }
}
