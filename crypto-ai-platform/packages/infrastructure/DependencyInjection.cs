using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RabbitMQ.Client;
using CryptoAIPlatform.Infrastructure.Data;
using CryptoAIPlatform.Infrastructure.Data.Repositories;
using CryptoAIPlatform.Infrastructure.EventBus;
using CryptoAIPlatform.Infrastructure.Options;
using CryptoAIPlatform.Infrastructure.Services;
using CryptoAIPlatform.Infrastructure.Services.Storage;
using CryptoAIPlatform.Infrastructure.Exchanges;
using CryptoAIPlatform.Infrastructure.Resilience;
using CryptoAIPlatform.Infrastructure.Observability;
using CryptoAIPlatform.Infrastructure.HealthChecks;
using CryptoAIPlatform.Infrastructure.Workers;
using CryptoAIPlatform.Infrastructure.DataQuality;
using CryptoAIPlatform.Infrastructure.DataQuality.Rules;
using CryptoAIPlatform.Infrastructure.FeatureStore;
using CryptoAIPlatform.Infrastructure.FeatureStore.Calculators;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Domain.Core.Abstractions;
using CryptoAIPlatform.Domain.Core.Abstractions.Events;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure.Repositories;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator.Repositories;
using CryptoAIPlatform.Domain.Trading.Repositories;
using CryptoAIPlatform.Domain.Risk.Repositories;
using CryptoAIPlatform.Domain.Risk.Services;
using CryptoAIPlatform.Infrastructure.Risk;
using CryptoAIPlatform.Domain.Trading.Services;

namespace CryptoAIPlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Options
        services.Configure<RabbitMQOptions>(configuration.GetSection(RabbitMQOptions.SectionName));
        services.Configure<RedisOptions>(configuration.GetSection(RedisOptions.SectionName));
        services.Configure<StorageOptions>(configuration.GetSection(StorageOptions.SectionName));
        services.Configure<PollyOptions>(configuration.GetSection(PollyOptions.SectionName));
        services.Configure<OpenTelemetryOptions>(configuration.GetSection(OpenTelemetryOptions.SectionName));
        services.Configure<HealthChecksOptions>(configuration.GetSection(HealthChecksOptions.SectionName));
        services.Configure<BinanceOptions>(configuration.GetSection(BinanceOptions.SectionName));
        services.Configure<ExchangeOptions>(configuration.GetSection(ExchangeOptions.SectionName));
        services.Configure<RateLimitOptions>(configuration.GetSection(RateLimitOptions.SectionName));
        services.Configure<ReconnectOptions>(configuration.GetSection(ReconnectOptions.SectionName));

        // Register options validators
        services.AddSingleton<IValidateOptions<BinanceOptions>, BinanceOptionsValidator>();
        services.AddSingleton<IValidateOptions<ExchangeOptions>, ExchangeOptionsValidator>();
        services.AddSingleton<IValidateOptions<RateLimitOptions>, RateLimitOptionsValidator>();
        services.AddSingleton<IValidateOptions<ReconnectOptions>, ReconnectOptionsValidator>();

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

        // Redis Cache
        var redisOptions = configuration.GetSection(RedisOptions.SectionName).Get<RedisOptions>() ?? new RedisOptions();
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisOptions.ConnectionString;
            options.InstanceName = redisOptions.InstanceName;
        });

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        services.AddScoped<IMarketDataSourceRepository, MarketDataSourceRepository>();
        services.AddScoped<IMarketDataIngestionJobRepository, MarketDataIngestionJobRepository>();
        services.AddScoped<IMarketDataRepository, MarketDataRepository>();
        services.AddScoped<IDataQualityJobRepository, DataQualityJobRepository>();
        services.AddScoped<IAnomalyRepository, AnomalyRepository>();
        services.AddScoped<IFeatureRepository, FeatureRepository>();
        services.AddScoped<IFeatureLineageRepository, FeatureLineageRepository>();
        services.AddScoped<IExperimentRepository, ExperimentRepository>();
        services.AddScoped<IExperimentRunRepository, ExperimentRunRepository>();
        services.AddScoped<IResearchDatasetRepository, ResearchDatasetRepository>();
        services.AddScoped<IDatasetVersionRepository, DatasetVersionRepository>();
        services.AddScoped<IReproducibilityPackageRepository, ReproducibilityPackageRepository>();
        services.AddScoped<IExecutionManifestRepository, ExecutionManifestRepository>();
        services.AddScoped<IMarketMicrostructureModelRepository, MarketMicrostructureModelRepository>();
        services.AddScoped<ILiquidityRepository, LiquidityRepository>();
        services.AddScoped<IExecutionSimulationRepository, ExecutionSimulationRepository>();
        services.AddScoped<IExecutionStatisticsRepository, ExecutionStatisticsRepository>();

        // Trading Repositories
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<IRiskRepository, RiskRepository>();
        services.AddScoped<ITradeExecutionRepository, TradeExecutionRepository>();
        services.AddScoped<IRiskAssessmentRepository, RiskAssessmentRepository>();
        services.AddScoped<IRiskLimitRepository, RiskLimitRepository>();
        services.AddScoped<IRiskRuleRepository, RiskRuleRepository>();
        services.AddScoped<IExposureRepository, ExposureRepository>();
        services.AddScoped<IPortfolioRiskRepository, PortfolioRiskRepository>();

        // Services
        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IExchangeClientFactory, ExchangeClientFactory>();
        services.AddScoped<BinanceSpotConnector>();
        services.AddScoped<IExchangeConnector, BinanceSpotConnector>();
        services.AddScoped<IExchangeStreamingService, BinanceStreamingService>();
        services.AddScoped<ISecretProvider, ConfigurationSecretProvider>();
        services.AddSingleton<ExchangeResiliencePolicies>();
        services.AddScoped<IRiskAssessmentService, RiskAssessmentService>();
        services.AddScoped<IRiskValidationService, RiskValidationService>();
        services.AddScoped<IExposureService, ExposureService>();
        services.AddScoped<IMarginService, MarginService>();
        services.AddScoped<ILiquidationService, LiquidationService>();
        
        // Data Quality Services
        services.AddScoped<IDataQualityService, DataQualityService>();
        services.AddScoped<ICandleDataQualityRule, MissingCandleRule>();
        services.AddScoped<ICandleDataQualityRule, NegativeVolumeRule>();
        
        // Feature Store Services
        services.AddScoped<IFeatureStoreService, FeatureStoreService>();
        services.AddScoped<IFeatureCalculator, SmaCalculator>();
        services.AddScoped<IFeatureCalculator, EmaCalculator>();
        
        // Experiment Tracking Services
        services.AddScoped<IExperimentTrackingService, QuantFoundation.ExperimentTrackingService>();
        
        // Research Dataset Services
        services.AddScoped<IResearchDatasetService, QuantFoundation.ResearchDatasetService>();

        // Reproducibility Services
        services.AddScoped<IReproducibilityService, QuantFoundation.ReproducibilityService>();
        
        // Market Microstructure Services
        services.AddScoped<IMarketMicrostructureService, QuantFoundation.MarketMicrostructureService>();
        
        // Execution Simulator Services
        services.AddScoped<IExecutionSimulatorService, QuantFoundation.ExecutionSimulatorService>();

        // Trading Services
        services.AddScoped<ITradingEngine, Trading.TradingEngineService>();
        services.AddScoped<IRiskEngine, Trading.RiskEngineService>();
        services.AddScoped<IOrderExecutionService, Trading.OrderExecutionService>();
        services.AddScoped<IPortfolioService, Trading.PortfolioService>();
        services.AddScoped<IPositionService, Trading.PositionService>();

        // Background Workers
        services.AddHostedService<MarketDataWorker>();
        services.AddHostedService<FeatureCalculationWorker>();
        services.AddHostedService<DataQualityWorker>();
        services.AddHostedService<ExperimentWorker>();

        // Health Checks
        services.AddHealthChecks()
            .AddCheck<ExchangeHealthCheck>("exchange");
        
        // Storage Services
        var storageOptions = configuration.GetSection(StorageOptions.SectionName).Get<StorageOptions>() ?? new StorageOptions();
        if (storageOptions.Provider.Equals("AzureBlob", StringComparison.OrdinalIgnoreCase))
        {
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
        }
        else if (storageOptions.Provider.Equals("AwsS3", StringComparison.OrdinalIgnoreCase))
        {
            services.AddScoped<IAwsS3StorageService, AwsS3StorageService>();
        }
        services.AddScoped<IColdStorageService, ColdStorageService>();

        // Event Bus Configuration (RabbitMQ)
        var rabbitMQOptions = configuration.GetSection(RabbitMQOptions.SectionName).Get<RabbitMQOptions>() ?? new RabbitMQOptions();
        var factory = new ConnectionFactory
        {
            HostName = rabbitMQOptions.Host,
            Port = rabbitMQOptions.Port,
            UserName = rabbitMQOptions.UserName,
            Password = rabbitMQOptions.Password,
            VirtualHost = rabbitMQOptions.VirtualHost
        };

        services.AddSingleton<IConnection>(factory.CreateConnection());
        services.AddSingleton<IEventSerializer, JsonEventSerializer>();
        services.AddSingleton<IEventBus, RabbitMQEventBus>();

        // OpenTelemetry Configuration
        var otelOptions = configuration.GetSection(OpenTelemetryOptions.SectionName).Get<OpenTelemetryOptions>() ?? new OpenTelemetryOptions();
        services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService(otelOptions.ServiceName, serviceVersion: otelOptions.ServiceVersion);
            })
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation();
                tracing.AddHttpClientInstrumentation();
                tracing.AddEntityFrameworkCoreInstrumentation();
                if (!string.IsNullOrEmpty(otelOptions.OtlpEndpoint))
                {
                    tracing.AddOtlpExporter(otlp =>
                    {
                        otlp.Endpoint = new Uri(otelOptions.OtlpEndpoint);
                    });
                }
            })
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddHttpClientInstrumentation();
                if (!string.IsNullOrEmpty(otelOptions.OtlpEndpoint))
                {
                    metrics.AddOtlpExporter(otlp =>
                    {
                        otlp.Endpoint = new Uri(otelOptions.OtlpEndpoint);
                    });
                }
            });

        return services;
    }
}
