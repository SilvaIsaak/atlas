using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using CryptoAIPlatform.Infrastructure.Options;

namespace CryptoAIPlatform.Infrastructure;

public static class HealthCheckExtensions
{
    public static IHealthChecksBuilder AddInfrastructureHealthChecks(
        this IHealthChecksBuilder builder,
        HealthChecksOptions options)
    {
        if (options.EnablePostgreSQL)
        {
            // Note: You'd need to add Npgsql health check package (Npgsql.EntityFrameworkCore.PostgreSQL.HealthChecks)
            // For now, we'll skip, since we have DbContext
            // builder.AddNpgSql();
        }
        
        if (options.EnableRedis)
        {
            // Redis is already registered as distributed cache, we could check later
        }

        if (options.EnableRabbitMQ)
        {
            builder.AddRabbitMQ();
        }

        return builder;
    }
}
