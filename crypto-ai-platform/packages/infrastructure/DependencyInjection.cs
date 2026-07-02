using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using RabbitMQ.Client;
using CryptoAIPlatform.Infrastructure.Data;
using CryptoAIPlatform.Infrastructure.EventBus;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Domain.Core.Abstractions.Events;
using CryptoAIPlatform.Infrastructure.Services;
using CryptoAIPlatform.Infrastructure.Exchanges;

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

        // Identity Configuration
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // JWT Authentication Configuration
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not found.");
        var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found.");
        var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience not found.");
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });

        // Policy-based Authorization
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ViewUsersPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.ViewUsers)));
            options.AddPolicy("CreateUsersPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.CreateUsers)));
            options.AddPolicy("EditUsersPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.EditUsers)));
            options.AddPolicy("DeleteUsersPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.DeleteUsers)));
            options.AddPolicy("ViewRolesPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.ViewRoles)));
            options.AddPolicy("CreateRolesPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.CreateRoles)));
            options.AddPolicy("EditRolesPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.EditRoles)));
            options.AddPolicy("DeleteRolesPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.DeleteRoles)));
            options.AddPolicy("AssignRolesPolicy", policy =>
                policy.RequireClaim("Permission", nameof(Permission.AssignRoles)));
        });

        var redisConnectionString = configuration.GetConnectionString("RedisConnection")
                                   ?? throw new InvalidOperationException("Connection string 'RedisConnection' not found.");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "CryptoAIPlatform_";
        });

        // Event Bus Configuration (RabbitMQ)
        var rabbitMQHost = configuration["RabbitMQ:Host"] ?? "localhost";
        var rabbitMQPort = int.Parse(configuration["RabbitMQ:Port"] ?? "5672");
        var rabbitMQUser = configuration["RabbitMQ:UserName"] ?? "guest";
        var rabbitMQPassword = configuration["RabbitMQ:Password"] ?? "guest";

        var factory = new ConnectionFactory
        {
            HostName = rabbitMQHost,
            Port = rabbitMQPort,
            UserName = rabbitMQUser,
            Password = rabbitMQPassword
        };

        services.AddSingleton<IConnection>(factory.CreateConnection());
        services.AddSingleton<IEventSerializer, JsonEventSerializer>();
        services.AddSingleton<IEventBus, RabbitMQEventBus>();

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IExchangeClientFactory, ExchangeClientFactory>();

        services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService("CryptoAIPlatform.Api", serviceVersion: "1.0.0");
            })
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddHttpClientInstrumentation();
                tracing.AddEntityFrameworkCoreInstrumentation();
                var otlpEndpoint = configuration["Otlp:Endpoint"];
                if (!string.IsNullOrEmpty(otlpEndpoint))
                {
                    tracing.AddOtlpExporter(otlp =>
                    {
                        otlp.Endpoint = new Uri(otlpEndpoint);
                    });
                }
            })
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddHttpClientInstrumentation();
                var otlpEndpoint = configuration["Otlp:Endpoint"];
                if (!string.IsNullOrEmpty(otlpEndpoint))
                {
                    metrics.AddOtlpExporter(otlp =>
                    {
                        otlp.Endpoint = new Uri(otlpEndpoint);
                    });
                }
            });

        return services;
    }
}
