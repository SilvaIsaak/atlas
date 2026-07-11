using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CryptoAIPlatform.Domain.Agents;
using CryptoAIPlatform.Infrastructure.Agents;

namespace CryptoAIPlatform.Infrastructure.Agents;

public class AgentHostedService : IHostedService
{
    private readonly ILogger<AgentHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IAgentRegistry _agentRegistry;

    public AgentHostedService(
        ILogger<AgentHostedService> logger,
        IServiceProvider serviceProvider,
        IAgentRegistry agentRegistry)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _agentRegistry = agentRegistry;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Agent Hosted Service");

        // Register and initialize all agents
        await RegisterAndInitializeAgentAsync<SupervisorAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<TradingAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<RiskAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<PortfolioAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<ResearchAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<FeatureEngineeringAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<DataQualityAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<ExecutionAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<NotificationAgent>(cancellationToken);
        await RegisterAndInitializeAgentAsync<MarketDataAgent>(cancellationToken);

        _logger.LogInformation("All agents registered and initialized successfully");
    }

    private async Task RegisterAndInitializeAgentAsync<TAgent>(CancellationToken cancellationToken) where TAgent : IAgent
    {
        using var scope = _serviceProvider.CreateScope();
        var agent = scope.ServiceProvider.GetRequiredService<TAgent>();
        
        await _agentRegistry.RegisterAsync(agent, cancellationToken);
        await agent.InitializeAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Agent Hosted Service");

        var agents = await _agentRegistry.GetAllAgentsAsync(cancellationToken);
        foreach (var agent in agents)
        {
            await agent.StopAsync(cancellationToken);
        }

        _logger.LogInformation("All agents stopped successfully");
    }
}
