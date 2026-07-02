using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.IdentityAndAccess;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Domain.Wallets;
using CryptoAIPlatform.Domain.Strategies;
using CryptoAIPlatform.Domain.Backtesting;
using CryptoAIPlatform.Domain.WalkForward;
using CryptoAIPlatform.Domain.PaperTrading;
using CryptoAIPlatform.Domain.RiskManagement;
using CryptoAIPlatform.Domain.AIDecision;
using CryptoAIPlatform.Domain.Execution;
using CryptoAIPlatform.Domain.LiveTrading;
using CryptoAIPlatform.Domain.Notifications;
using CryptoAIPlatform.Domain.Monitoring;
using CryptoAIPlatform.Domain.Learning;
using CryptoAIPlatform.Domain.Deployment;
using CryptoAIPlatform.Domain.Mobile;
using CryptoAIPlatform.Domain.Reports;
using CryptoAIPlatform.Domain.Admin;
using CryptoAIPlatform.Domain.MultiTenant;
using CryptoAIPlatform.Domain.Core.ValueObjects;
using CryptoAIPlatform.Domain.QuantFoundation.MarketData;
using CryptoAIPlatform.Domain.QuantFoundation.DataQuality;
using CryptoAIPlatform.Domain.QuantFoundation.FeatureStore;
using CryptoAIPlatform.Domain.QuantFoundation.ExperimentTracking;
using CryptoAIPlatform.Domain.QuantFoundation.ResearchDataset;
using CryptoAIPlatform.Domain.QuantFoundation.Reproducibility;
using CryptoAIPlatform.Domain.QuantFoundation.MarketMicrostructure;
using CryptoAIPlatform.Domain.QuantFoundation.ExecutionSimulator;
using CryptoAIPlatform.Infrastructure.Data.Extensions;

namespace CryptoAIPlatform.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public static TenantId CurrentTenantId { get; set; } = null!;
    public static Guid? CurrentUserId { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<ExchangeIntegration> ExchangeIntegrations { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletBalance> WalletBalances { get; set; }
    public DbSet<Strategy> Strategies { get; set; }
    public DbSet<Backtest> Backtests { get; set; }
    public DbSet<WalkForward> WalkForwards { get; set; }
    public DbSet<PaperTrade> PaperTrades { get; set; }
    public DbSet<RiskProfile> RiskProfiles { get; set; }
    public DbSet<AIDecision> AIDecisions { get; set; }
    public DbSet<ExecutionEngine> ExecutionEngines { get; set; }
    public DbSet<LiveTrade> LiveTrades { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<SystemMetric> SystemMetrics { get; set; }
    public DbSet<SystemAlert> SystemAlerts { get; set; }
    public DbSet<LearningContent> LearningContents { get; set; }
    public DbSet<UserLearningProgress> UserLearningProgresses { get; set; }
    public DbSet<Deployment> Deployments { get; set; }
    public DbSet<MobileDevice> MobileDevices { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<AdminLog> AdminLogs { get; set; }
    public DbSet<Tenant> Tenants { get; set; }

    // QuantFoundation - MarketData
    public DbSet<MarketDataSource> MarketDataSources { get; set; }
    public DbSet<MarketDataIngestionJob> MarketDataIngestionJobs { get; set; }
    public DbSet<MarketDataAsset> MarketDataAssets { get; set; }

    // QuantFoundation - DataQuality
    public DbSet<DataQualityJob> DataQualityJobs { get; set; }
    public DbSet<Anomaly> Anomalies { get; set; }

    // QuantFoundation - FeatureStore
    public DbSet<Feature> Features { get; set; }
    public DbSet<FeatureVersion> FeatureVersions { get; set; }
    public DbSet<FeatureCatalogEntry> FeatureCatalogEntries { get; set; }
    public DbSet<FeatureLineage> FeatureLineages { get; set; }

    // QuantFoundation - ExperimentTracking
    public DbSet<Experiment> Experiments { get; set; }
    public DbSet<ExperimentParameter> ExperimentParameters { get; set; }
    public DbSet<ExperimentRun> ExperimentRuns { get; set; }
    public DbSet<ExperimentMetric> ExperimentMetrics { get; set; }
    public DbSet<ExperimentArtifact> ExperimentArtifacts { get; set; }

    // QuantFoundation - ResearchDataset
    public DbSet<ResearchDataset> ResearchDatasets { get; set; }
    public DbSet<DatasetVersion> DatasetVersions { get; set; }
    public DbSet<DatasetTransformation> DatasetTransformations { get; set; }

    // QuantFoundation - Reproducibility
    public DbSet<ReproducibilityPackage> ReproducibilityPackages { get; set; }

    // QuantFoundation - MarketMicrostructure
    public DbSet<MarketMicrostructureModel> MarketMicrostructureModels { get; set; }
    public DbSet<SpreadData> SpreadData { get; set; }
    public DbSet<SlippageModel> SlippageModels { get; set; }
    public DbSet<LiquidityProfile> LiquidityProfiles { get; set; }

    // QuantFoundation - ExecutionSimulator
    public DbSet<ExecutionSimulation> ExecutionSimulations { get; set; }
    public DbSet<SimulatedOrder> SimulatedOrders { get; set; }
    public DbSet<SimulatedFill> SimulatedFills { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => new { rp.RoleId, rp.Permission });
            entity.HasOne<Role>()
                  .WithMany(r => r.RolePermissions)
                  .HasForeignKey(rp => rp.RoleId);
        });
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity<Guid>);

        foreach (var entry in entries)
        {
            var entity = (BaseEntity<Guid>)entry.Entity;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.CreatedBy = CurrentUserId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = CurrentUserId;
            }
        }
    }
}
