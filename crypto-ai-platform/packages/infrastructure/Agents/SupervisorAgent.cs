using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Agents;

namespace CryptoAIPlatform.Infrastructure.Agents;

public class SupervisorAgent : BaseAgent
{
    private readonly IAgentRegistry _agentRegistry;

    public SupervisorAgent(
        ILogger<SupervisorAgent> logger,
        Func<string, IAgentMemory> memoryFactory,
        IAgentEventBus eventBus,
        IAgentRegistry agentRegistry)
        : base("supervisor-agent", "Supervisor Agent", "Oversees all other agents and coordinates their activities", logger, memoryFactory("supervisor-agent"), eventBus)
    {
        _agentRegistry = agentRegistry;
    }

    public override async Task<TResult> ExecuteAsync<TTask, TResult>(TTask task, CancellationToken cancellationToken = default)
    {
        Logger.LogInformation("SupervisorAgent executing task {TaskId}", task.TaskId);
        await PublishAgentEventAsync("supervisor.task_executed", new { TaskId = task.TaskId }, cancellationToken);
        
        // Placeholder implementation - will be filled with actual AI logic later
        return await Task.FromResult(default(TResult) ?? throw new InvalidOperationException("Result cannot be null"));
    }

    public async Task<IEnumerable<IAgent>> GetAllRegisteredAgentsAsync(CancellationToken cancellationToken = default)
    {
        return await _agentRegistry.GetAllAgentsAsync(cancellationToken);
    }
}
