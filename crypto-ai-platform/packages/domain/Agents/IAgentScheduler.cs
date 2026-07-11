namespace CryptoAIPlatform.Domain.Agents;

/// <summary>
/// Interface for agent task scheduler
/// </summary>
public interface IAgentScheduler
{
    /// <summary>
    /// Schedule a task for an agent
    /// </summary>
    Task<string> ScheduleTaskAsync(string agentId, IAgentTask task, DateTime? executeAt = null, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Cancel a scheduled task
    /// </summary>
    Task CancelTaskAsync(string taskId, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get scheduled tasks for an agent
    /// </summary>
    Task<IEnumerable<IAgentTask>> GetScheduledTasksAsync(string agentId, CancellationToken cancellationToken = default);
}
