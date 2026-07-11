using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoAIPlatform.Infrastructure.Data;
using CryptoAIPlatform.Domain.Agents;
using CryptoAIPlatform.Infrastructure.Agents;

namespace CryptoAIPlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, npgsql =>
            {
                npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });

        // Add AI Agent infrastructure services
        services.AddSingleton<IAgentRegistry, InMemoryAgentRegistry>();
        services.AddSingleton<IAgentEventBus, InMemoryAgentEventBus>();
        services.AddSingleton<IAgentScheduler, InMemoryAgentScheduler>();

        // Register agent memory factory (since each agent needs its own memory instance)
        services.AddTransient<Func<string, IAgentMemory>>(sp => 
            agentId => ActivatorUtilities.CreateInstance<InMemoryAgentMemory>(sp, agentId));

        // Register all AI Agents as transient
        services.AddTransient<SupervisorAgent>();
        services.AddTransient<TradingAgent>();
        services.AddTransient<RiskAgent>();
        services.AddTransient<PortfolioAgent>();
        services.AddTransient<ResearchAgent>();
        services.AddTransient<FeatureEngineeringAgent>();
        services.AddTransient<DataQualityAgent>();
        services.AddTransient<ExecutionAgent>();
        services.AddTransient<NotificationAgent>();
        services.AddTransient<MarketDataAgent>();

        // Add hosted service to manage agent lifecycle
        services.AddHostedService<AgentHostedService>();

        return services;
    }
}
