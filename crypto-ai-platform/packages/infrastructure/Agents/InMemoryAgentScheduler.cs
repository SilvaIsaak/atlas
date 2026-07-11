using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Agents;
using System.Collections.Concurrent;

namespace CryptoAIPlatform.Infrastructure.Agents;

public class InMemoryAgentScheduler : IAgentScheduler
{
    private readonly ConcurrentDictionary<string, ScheduledTask> _scheduledTasks = new();
    private readonly ILogger<InMemoryAgentScheduler> _logger;
    private readonly IAgentRegistry _agentRegistry;

    public InMemoryAgentScheduler(ILogger<InMemoryAgentScheduler> logger, IAgentRegistry agentRegistry)
    {
        _logger = logger;
        _agentRegistry = agentRegistry;
    }

    public async Task<string> ScheduleTaskAsync(string agentId, IAgentTask task, DateTime? executeAt = null, CancellationToken cancellationToken = default)
    {
        var taskId = Guid.NewGuid().ToString();
        var scheduledTask = new ScheduledTask
        {
            TaskId = taskId,
            AgentId = agentId,
            Task = task,
            ExecuteAt = executeAt ?? DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _scheduledTasks[taskId] = scheduledTask;
        _logger.LogInformation("Scheduled task {TaskId} for agent {AgentId}", taskId, agentId);

        // If executeAt is in the past or now, execute immediately (placeholder)
        if (scheduledTask.ExecuteAt <= DateTime.UtcNow)
        {
            await ExecuteScheduledTaskAsync(scheduledTask, cancellationToken);
        }

        return await Task.FromResult(taskId);
    }

    public Task CancelTaskAsync(string taskId, CancellationToken cancellationToken = default)
    {
        if (_scheduledTasks.TryRemove(taskId, out _))
        {
            _logger.LogInformation("Cancelled task {TaskId}", taskId);
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<IAgentTask>> GetScheduledTasksAsync(string agentId, CancellationToken cancellationToken = default)
    {
        var tasks = _scheduledTasks.Values
            .Where(t => t.AgentId == agentId)
            .Select(t => t.Task);
        return Task.FromResult(tasks);
    }

    private async Task ExecuteScheduledTaskAsync(ScheduledTask scheduledTask, CancellationToken cancellationToken)
    {
        var agent = await _agentRegistry.GetAgentAsync(scheduledTask.AgentId, cancellationToken);
        if (agent is IAsyncAgent asyncAgent)
        {
            _logger.LogInformation("Executing scheduled task {TaskId} for agent {AgentId}", scheduledTask.TaskId, scheduledTask.AgentId);
            // Note: We can't really execute without knowing the TResult type, this is a placeholder
        }
    }

    private class ScheduledTask
    {
        public string TaskId { get; init; } = string.Empty;
        public string AgentId { get; init; } = string.Empty;
        public IAgentTask Task { get; init; } = null!;
        public DateTime ExecuteAt { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
