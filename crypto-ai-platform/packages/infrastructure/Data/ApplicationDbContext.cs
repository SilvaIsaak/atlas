using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CryptoAIPlatform.Domain.Abstractions;
using CryptoAIPlatform.Domain.IdentityAndAccess;
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
using CryptoAIPlatform.Domain.Risk;
using CryptoAIPlatform.Domain.Trading;
using CryptoAIPlatform.Domain.PortfolioAnalytics;
using CryptoAIPlatform.Domain.Admin;
using CryptoAIPlatform.Domain.AIDecision;
using CryptoAIPlatform.Domain.Backtesting;
using CryptoAIPlatform.Domain.Wallets;
using CryptoAIPlatform.Domain.WalkForward;
using CryptoAIPlatform.Domain.Notifications;
using CryptoAIPlatform.Domain.Reports;
using CryptoAIPlatform.Domain.Monitoring;
using CryptoAIPlatform.Domain.Learning;
using CryptoAIPlatform.Domain.Deployment;
using CryptoAIPlatform.Domain.Execution;
using CryptoAIPlatform.Domain.Exchanges;
using CryptoAIPlatform.Domain.Strategies;
using CryptoAIPlatform.Infrastructure.Data.Extensions;
using CryptoAIPlatform.Infrastructure.Data.Configurations;
using System.Linq.Expressions;

// NOTE: alguns configs são excluídos por fase via csproj; manter referência direta aqui pode falhar
// se a classe não existir/for removida do compilação. A configuração abaixo deve compilar somente
// com as configs disponíveis na fase atual.


namespace CryptoAIPlatform.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public static TenantId CurrentTenantId { get; set; } = null!;
    public static Guid? CurrentUserId { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Tenant> Tenants { get; set; }



    // QuantFoundation - MarketData
    public DbSet<MarketDataSource> MarketDataSources { get; set; }

    public DbSet<MarketDataIngestionJob> MarketDataIngestionJobs { get; set; }
    public DbSet<MarketDataAsset> MarketDataAssets { get; set; }
    public DbSet<Candle> Candles { get; set; }
    public DbSet<Trade> Trades { get; set; }
    public DbSet<OrderBook> OrderBooks { get; set; }
    public DbSet<FundingRate> FundingRates { get; set; }
    public DbSet<OpenInterest> OpenInterests { get; set; }

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
    public DbSet<DatasetSnapshot> DatasetSnapshots { get; set; }
    public DbSet<DatasetPartition> DatasetPartitions { get; set; }
    public DbSet<DatasetMetadata> DatasetMetadata { get; set; }
    public DbSet<DatasetSchema> DatasetSchemas { get; set; }
    public DbSet<DatasetTag> DatasetTags { get; set; }

    // QuantFoundation - Reproducibility
    public DbSet<ReproducibilityPackage> ReproducibilityPackages { get; set; }
    public DbSet<EnvironmentSnapshot> EnvironmentSnapshots { get; set; }
    public DbSet<GitSnapshot> GitSnapshots { get; set; }
    public DbSet<DependencySnapshot> DependencySnapshots { get; set; }
    public DbSet<DatasetReference> DatasetReferences { get; set; }
    public DbSet<FeatureReference> FeatureReferences { get; set; }
    public DbSet<ExperimentReference> ExperimentReferences { get; set; }
    public DbSet<ArtifactReference> ArtifactReferences { get; set; }
    public DbSet<ExecutionManifest> ExecutionManifests { get; set; }

    // QuantFoundation - MarketMicrostructure
    public DbSet<MarketMicrostructureModel> MarketMicrostructureModels { get; set; }
    public DbSet<SpreadData> SpreadData { get; set; }
    public DbSet<SlippageModel> SlippageModels { get; set; }
    public DbSet<LiquidityProfile> LiquidityProfiles { get; set; }
    public DbSet<OrderBookLevel> OrderBookLevels { get; set; }
    public DbSet<BidAskSpread> BidAskSpreads { get; set; }
    public DbSet<LiquiditySnapshot> LiquiditySnapshots { get; set; }
    public DbSet<MarketImpact> MarketImpacts { get; set; }
    public DbSet<TradeFlow> TradeFlows { get; set; }
    public DbSet<OrderImbalance> OrderImbalances { get; set; }
    public DbSet<VolumeProfile> VolumeProfiles { get; set; }
    public DbSet<VWAPSnapshot> VWAPSnapshots { get; set; }
    public DbSet<TWAPSnapshot> TWAPSnapshots { get; set; }
    public DbSet<MarketDepthSnapshot> MarketDepthSnapshots { get; set; }

    // QuantFoundation - ExecutionSimulator
    public DbSet<ExecutionSimulation> ExecutionSimulations { get; set; }
    public DbSet<SimulatedOrder> SimulatedOrders { get; set; }
    public DbSet<SimulatedFill> SimulatedFills { get; set; }
    public DbSet<ExecutionFee> ExecutionFees { get; set; }
    public DbSet<ExecutionLatency> ExecutionLatencies { get; set; }
    public DbSet<ExecutionSlippage> ExecutionSlippages { get; set; }
    public DbSet<ExecutionStatistics> ExecutionStatistics { get; set; }
    public DbSet<ExecutionTimeline> ExecutionTimelines { get; set; }

    // Trading
    public DbSet<Order> Orders { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Portfolio> Portfolios { get; set; }
    public DbSet<PortfolioSnapshot> PortfolioSnapshots { get; set; }
    public DbSet<RiskProfile> RiskProfiles { get; set; }
    public DbSet<TradeExecution> TradeExecutions { get; set; }
    public DbSet<OrderFill> OrderFills { get; set; }
    public DbSet<OrderFee> OrderFees { get; set; }
    public DbSet<OrderStatusHistory> OrderStatusHistory { get; set; }
    public DbSet<PositionLeg> PositionLegs { get; set; }
    public DbSet<PositionPnL> PositionPnLs { get; set; }
    public DbSet<PortfolioAsset> PortfolioAssets { get; set; }
    public DbSet<PortfolioBalance> PortfolioBalances { get; set; }

    // Risk Engine
    public DbSet<RiskAssessment> RiskAssessments { get; set; }
    public DbSet<RiskViolation> RiskViolations { get; set; }
    public DbSet<RiskMetric> RiskMetrics { get; set; }
    public DbSet<RiskLimit> RiskLimits { get; set; }
    public DbSet<RiskRule> RiskRules { get; set; }
    public DbSet<ExposureProfile> ExposureProfiles { get; set; }
    public DbSet<ExposureItem> ExposureItems { get; set; }
    public DbSet<PortfolioRiskSnapshot> PortfolioRiskSnapshots { get; set; }
    public DbSet<VaRSnapshot> VaRSnapshots { get; set; }
    public DbSet<StressScenarioResult> StressScenarioResults { get; set; }
    public DbSet<DrawdownSnapshot> DrawdownSnapshots { get; set; }
    public DbSet<MarginRequirement> MarginRequirements { get; set; }
    public DbSet<LiquidationLevel> LiquidationLevels { get; set; }

    // Portfolio Analytics
    public DbSet<PortfolioPerformanceReport> PortfolioPerformanceReports { get; set; }
    public DbSet<PerformanceSnapshot> PerformanceSnapshots { get; set; }
    public DbSet<EquityCurvePoint> EquityCurvePoints { get; set; }
    public DbSet<DrawdownPoint> DrawdownPoints { get; set; }
    public DbSet<BenchmarkComparison> BenchmarkComparisons { get; set; }
    
    // AI Strategy Engine
    public DbSet<Strategy> Strategies { get; set; }
    public DbSet<StrategyVersion> StrategyVersions { get; set; }
    public DbSet<StrategyExecution> StrategyExecutions { get; set; }
    public DbSet<StrategyResult> StrategyResults { get; set; }
    public DbSet<StrategySignal> StrategySignals { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure TenantId value conversion
        modelBuilder.Entity<Tenant>().Ignore(x => x.DomainEvents);
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity<Guid>).IsAssignableFrom(entityType.ClrType))
            {
                // Ignore DomainEvents
                modelBuilder.Entity(entityType.ClrType)
                    .Ignore(nameof(BaseEntity<Guid>.DomainEvents));
            }
        }
        
        modelBuilder.ConfigureTenantId();
        
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => new { rp.RoleId, rp.Permission });
            entity.HasOne<Role>()
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);
        });
        
        // Apply only the necessary configurations for Phase 0
        modelBuilder.ApplyConfiguration(new MarketDataSourceConfiguration());
        modelBuilder.ApplyConfiguration(new MarketDataIngestionJobConfiguration());
        modelBuilder.ApplyConfiguration(new MarketDataAssetConfiguration());
        modelBuilder.ApplyConfiguration(new CandleConfiguration());
        modelBuilder.ApplyConfiguration(new TradeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderBookConfiguration());
        modelBuilder.ApplyConfiguration(new FundingRateConfiguration());
        modelBuilder.ApplyConfiguration(new OpenInterestConfiguration());
        modelBuilder.ApplyConfiguration(new DataQualityJobConfiguration());
        modelBuilder.ApplyConfiguration(new AnomalyConfiguration());
        modelBuilder.ApplyConfiguration(new FeatureConfiguration());
        modelBuilder.ApplyConfiguration(new FeatureVersionConfiguration());
        modelBuilder.ApplyConfiguration(new FeatureCatalogEntryConfiguration());
        modelBuilder.ApplyConfiguration(new FeatureLineageConfiguration());
        modelBuilder.ApplyConfiguration(new ExperimentConfiguration());
        modelBuilder.ApplyConfiguration(new ExperimentParameterConfiguration());
        modelBuilder.ApplyConfiguration(new ExperimentRunConfiguration());
        modelBuilder.ApplyConfiguration(new ExperimentMetricConfiguration());
        modelBuilder.ApplyConfiguration(new ExperimentArtifactConfiguration());
        modelBuilder.ApplyConfiguration(new ResearchDatasetConfiguration());
        modelBuilder.ApplyConfiguration(new DatasetVersionConfiguration());
        modelBuilder.ApplyConfiguration(new DatasetTransformationConfiguration());
        modelBuilder.ApplyConfiguration(new DatasetSnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new DatasetMetadataConfiguration());
        modelBuilder.ApplyConfiguration(new DatasetSchemaConfiguration());
        modelBuilder.ApplyConfiguration(new DatasetTagConfiguration());
        modelBuilder.ApplyConfiguration(new DatasetPartitionConfiguration());
        modelBuilder.ApplyConfiguration(new ReproducibilityPackageConfiguration());
        modelBuilder.ApplyConfiguration(new ReproducibilityPackageExtendedConfiguration());
        modelBuilder.ApplyConfiguration(new EnvironmentSnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new GitSnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new DependencySnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new DatasetReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new FeatureReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new ExperimentReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new ArtifactReferenceConfiguration());
        modelBuilder.ApplyConfiguration(new ExecutionManifestConfiguration());
        modelBuilder.ApplyConfiguration(new MarketMicrostructureModelConfiguration());
        modelBuilder.ApplyConfiguration(new SpreadDataConfiguration());
        modelBuilder.ApplyConfiguration(new SlippageModelConfiguration());
        modelBuilder.ApplyConfiguration(new LiquidityProfileConfiguration());
        modelBuilder.ApplyConfiguration(new OrderBookLevelConfiguration());
        modelBuilder.ApplyConfiguration(new BidAskSpreadConfiguration());
        modelBuilder.ApplyConfiguration(new LiquiditySnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new MarketImpactConfiguration());
        modelBuilder.ApplyConfiguration(new TradeFlowConfiguration());
        modelBuilder.ApplyConfiguration(new OrderImbalanceConfiguration());
        modelBuilder.ApplyConfiguration(new VolumeProfileConfiguration());
        modelBuilder.ApplyConfiguration(new VWAPSnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new TWAPSnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new MarketDepthSnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new ExecutionSimulationConfiguration());
        modelBuilder.ApplyConfiguration(new SimulatedOrderConfiguration());
        modelBuilder.ApplyConfiguration(new SimulatedFillConfiguration());
        modelBuilder.ApplyConfiguration(new ExecutionFeeConfiguration());
        modelBuilder.ApplyConfiguration(new ExecutionLatencyConfiguration());
        modelBuilder.ApplyConfiguration(new ExecutionSlippageConfiguration());
        modelBuilder.ApplyConfiguration(new ExecutionStatisticsConfiguration());
        modelBuilder.ApplyConfiguration(new ExecutionTimelineConfiguration());

        // Trading Configurations
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new PositionConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioSnapshotConfiguration());
        // RiskProfileConfiguration é aplicado somente se disponível na fase atual.
        // (evita falhas de build quando o arquivo/config é excluído via csproj por fase)
        // applied only when the configuration type is available at compile time.
        // If the config file is excluded via csproj for the current phase, this must not reference it.
        // (Because we target Phase 0 baseline stability.)
        // modelBuilder.ApplyConfiguration(new RiskProfileConfiguration());
        modelBuilder.ApplyConfiguration(new TradeExecutionConfiguration());
        modelBuilder.ApplyConfiguration(new OrderFillConfiguration());
        modelBuilder.ApplyConfiguration(new OrderFeeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderStatusHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new PositionLegConfiguration());
        modelBuilder.ApplyConfiguration(new PositionPnLConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioAssetConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioBalanceConfiguration());

        // Risk Engine configurations
        modelBuilder.ApplyConfiguration(new RiskAssessmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RiskViolationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RiskMetricEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RiskLimitEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RiskRuleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExposureProfileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ExposureItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioRiskSnapshotEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VaRSnapshotEntityConfiguration());
        modelBuilder.ApplyConfiguration(new StressScenarioResultEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DrawdownSnapshotEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MarginRequirementEntityConfiguration());
        modelBuilder.ApplyConfiguration(new LiquidationLevelEntityConfiguration());

        // Portfolio Analytics configurations
        modelBuilder.ApplyConfiguration(new PortfolioPerformanceReportConfiguration());
        modelBuilder.ApplyConfiguration(new PerformanceSnapshotConfiguration());
        modelBuilder.ApplyConfiguration(new EquityCurvePointConfiguration());
        modelBuilder.ApplyConfiguration(new DrawdownPointConfiguration());
        modelBuilder.ApplyConfiguration(new BenchmarkComparisonConfiguration());
        
        // AI Strategy Engine configurations
        modelBuilder.ApplyConfiguration(new StrategyConfiguration());
        modelBuilder.ApplyConfiguration(new StrategyVersionConfiguration());
        modelBuilder.ApplyConfiguration(new StrategyExecutionConfiguration());
        modelBuilder.ApplyConfiguration(new StrategyResultConfiguration());
        modelBuilder.ApplyConfiguration(new StrategySignalConfiguration());
    }
}
