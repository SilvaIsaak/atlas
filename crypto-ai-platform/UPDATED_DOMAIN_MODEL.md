# 🗂️ Modelo de Domínio Atualizado – PHASE 0: QUANT FOUNDATION
**Plataforma**: Crypto AI Platform  
**Versão**: 1.0 (Revisado)  
**Data**: 2026-06-25  
**Status**: Pronto para Aprovação

---

## 1. Regras Básicas do Modelo
1. **Aggregate Roots têm identidade única (`Guid`)
2. **Entities** são imutáveis após criação, exceto por mudanças de status
3. **Value Objects** são `record` e imutáveis
4. **Todas as entidades** herdam de `BaseEntity<Guid>` (com `CreatedAt`, `UpdatedAt`, `CreatedBy`, `UpdatedBy`, `TenantId`)
5. **Todas as entidades** têm `TenantId` para multi-tenant com RLS
6. **Todas as regras de negócio** estão no Domain Layer

---

## 2. Aggregate Roots Completos
| Nome | Módulo | Atributos Principais |
|------|--------|---------------------|
| `MarketDataSource` | Market Data Lake | `Id`, `Name`, `BaseUrl`, `EncryptedApiKey`, `ApiKeyNonce`, `ApiKeyTag`, `IsActive`, `Type` (enum: Binance, Bybit, Okx, CoinGecko, CoinMarketCap) |
| `DataQualityJob` | Data Quality Engine | `Id`, `AssetSymbol`, `PeriodStart`, `PeriodEnd`, `Status` (enum: Pending, Running, Completed, Failed), `TotalChecksCount`, `AnomaliesCount` |
| `Feature` | Feature Store | `Id`, `Name`, `Description`, `Code`, `Maturity` (enum: Experimental, Beta, Production, Deprecated), `OwnerId`, `IsApproved` |
| `FeatureLineage` | Feature Lineage | `Id`, `FeatureId`, `Nodes` (lista de `FeatureLineageNode`), `CreatedAt` |
| `Experiment` | Experiment Tracking | `Id`, `Name`, `Description`, `Type` (enum: Backtest, WalkForward, PaperTrade), `OwnerId`, `CreatedAt` |
| `ExperimentRun` | Experiment Tracking | `Id`, `ExperimentId`, `DatasetVersionId`, `StrategyVersion`, `Status` (enum: Draft, Running, Completed, Failed), `StartedAt`, `CompletedAt` |
| `ResearchDataset` | Research Dataset Registry | `Id`, `Name`, `Description`, `OwnerId`, `Version` (semver), `IsImmutable` (true) |
| `ReproducibilityPackage` | Research Reproducibility | `Id`, `ExperimentRunId`, `EnvironmentInfo`, `Status` (enum: Pending, InProgress, Verified, Failed), `CreatedAt` |
| `MarketMicrostructureModel` | Market Microstructure | `Id`, `Name`, `AssetSymbol`, `CalibratedAt`, `IsActive` |
| `ExecutionSimulation` | Execution Simulator | `Id`, `Name`, `MicrostructureModelId`, `Status` (enum: Draft, Running, Completed, Failed), `CreatedAt` |

---

## 3. Entidades Filhas (Child Entities)
| Nome | Aggregate Pai | Atributos Principais |
|------|----------------|---------------------|
| `MarketDataIngestionJob` | `MarketDataSource` | `Id`, `DataSourceId`, `AssetSymbol`, `DataType` (enum: Ohlcv, Trades, OrderBook), `Status` (enum: Pending, Running, Completed, Failed), `StartedAt`, `CompletedAt` |
| `MarketDataAsset` | `MarketDataSource` | `Id`, `DataSourceId`, `Symbol`, `Name`, `IsActive` |
| `Anomaly` | `DataQualityJob` | `Id`, `JobId`, `AssetSymbol`, `Type` (enum: MissingCandle, Gap, Spike, Outlier, Duplicate, Corrupt, InvalidTimestamp, VolumeInconsistent), `Severity` (enum: Info, Warning, Critical), `IsResolved`, `ResolvedAt` |
| `FeatureVersion` | `Feature` | `Id`, `FeatureId`, `Version` (semver), `Code`, `CreatedAt` |
| `FeatureCatalogEntry` | `Feature` | `Id`, `FeatureId`, `Description`, `UsageExamples`, `PerformanceMetrics`, `CreatedAt` |
| `ExperimentParameter` | `Experiment` | `Id`, `ExperimentId`, `Key`, `Value`, `CreatedAt` |
| `ExperimentMetric` | `ExperimentRun` | `Id`, `ExperimentRunId`, `Key`, `Name`, `Value`, `CreatedAt` |
| `ExperimentArtifact` | `ExperimentRun` | `Id`, `ExperimentRunId`, `Name`, `Type` (enum: Report, Graph, Dataset), `StoragePath`, `CreatedAt` |
| `DatasetVersion` | `ResearchDataset` | `Id`, `DatasetId`, `Version` (semver), `PeriodStart`, `PeriodEnd`, `AssetSymbols` (lista), `CreatedAt` |
| `DatasetTransformation` | `ResearchDataset` | `Id`, `DatasetVersionId`, `Type`, `Code`, `CreatedAt` |
| `SpreadData` | `MarketMicrostructureModel` | `Id`, `ModelId`, `Timestamp`, `BidPrice`, `AskPrice`, `SpreadBps`, `AssetSymbol` |
| `SlippageModel` | `MarketMicrostructureModel` | `Id`, `ModelId`, `AssetSymbol`, `Parameters` (JSON), `CreatedAt` |
| `LiquidityProfile` | `MarketMicrostructureModel` | `Id`, `ModelId`, `AssetSymbol`, `OrderBookLevels` (JSON), `CreatedAt` |
| `SimulatedOrder` | `ExecutionSimulation` | `Id`, `SimulationId`, `Type` (enum: Market, Limit, Twap, Vwap), `Symbol`, `Side` (enum: Buy, Sell), `Quantity`, `Status` (enum: Pending, PartiallyFilled, Filled, Cancelled, Failed), `CreatedAt` |
| `SimulatedFill` | `ExecutionSimulation` | `Id`, `OrderId`, `Price`, `Quantity`, `Fee`, `Timestamp`, `CreatedAt` |

---

## 4. Value Objects Completos
| Nome | Módulo | Atributos |
|------|--------|-----------|
| `OhlcvData` | Market Data Lake | `Timestamp`, `Open`, `High`, `Low`, `Close`, `Volume` |
| `TradeData` | Market Data Lake | `TradeId`, `Timestamp`, `Price`, `Quantity`, `Side` (Buy/Sell) |
| `OrderBookSnapshot` | Market Data Lake | `Timestamp`, `Bids` (lista de `OrderBookLevel`), `Asks` (lista de `OrderBookLevel`) |
| `FundingRateData` | Market Data Lake | `Timestamp`, `FundingRate`, `NextFundingTime` |
| `OpenInterestData` | Market Data Lake | `Timestamp`, `OpenInterest` |
| `LiquidationData` | Market Data Lake | `Timestamp`, `Price`, `Quantity`, `Side`, `LiquidationType` |
| `OnChainMetricData` | Market Data Lake | `Timestamp`, `MetricName`, `MetricValue` |
| `OrderBookLevel` | Market Data Lake / Market Microstructure | `Price`, `Quantity` |
| `FeatureLineageNode` | Feature Lineage | `NodeId`, `Type` (RawData, Feature, Transformation), `ParentNodeIds`, `Metadata` (JSON) |
| `EnvironmentInfo` | Research Reproducibility | `DotNetVersion`, `PackageVersions` (JSON), `OsVersion`, `DockerImageTag` |
| `ExchangeLatencyModel` | Market Microstructure | `MeanLatencyMs`, `P99LatencyMs`, `P999LatencyMs` |
| `TenantId` | Core | `Value` (Guid) |
| `CorrelationId` | Core | `Value` (Guid) |
| `CausationId` | Core | `Value` (Guid) |
| `EventVersion` | Core | `Major` (int), `Minor` (int), `Patch` (int) |
| `IdempotencyKey` | Core | `Value` (Guid) |

---

## 5. Domain Event Base Class
Todos os eventos herdam de `DomainEvent`, que tem os campos OBRIGATÓRIOS:
```csharp
// packages/domain/Abstractions/Events/DomainEvent.cs
namespace CryptoAIPlatform.Domain.Abstractions.Events;

public abstract record DomainEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public EventVersion Version { get; init; } = new(1, 0, 0);
    public CorrelationId CorrelationId { get; init; } = new(Guid.NewGuid());
    public CausationId? CausationId { get; init; }
    public DateTime OccurredAt { get; init; } = DateTime.UtcNow;
    public TenantId TenantId { get; init; } = null!;
    public IdempotencyKey IdempotencyKey { get; init; } = new(Guid.NewGuid());
}
```

---

## 6. Domain Events Completos
Todos os eventos herdam de `DomainEvent` (campos comuns não são listados abaixo):
| Nome | Módulo | Propriedades Específicas |
|------|--------|---------------------------|
| `MarketData.Ingested.V1` | Market Data Lake | `DataSourceId`, `AssetSymbol`, `DataType`, `IngestionJobId` |
| `MarketData.IngestionFailed.V1` | Market Data Lake | `DataSourceId`, `AssetSymbol`, `ErrorMessage` |
| `DataQuality.AnomalyDetected.V1` | Data Quality Engine | `AnomalyId`, `AssetSymbol`, `AnomalyType`, `Severity` |
| `DataQuality.AnomalyResolved.V1` | Data Quality Engine | `AnomalyId`, `ResolvedBy` |
| `Feature.Created.V1` | Feature Store | `FeatureId`, `Name`, `OwnerId` |
| `Feature.Versioned.V1` | Feature Store | `FeatureId`, `Version` |
| `Feature.Approved.V1` | Feature Store | `FeatureId`, `ApprovedBy` |
| `Experiment.Started.V1` | Experiment Tracking | `ExperimentId`, `RunId`, `StartedBy` |
| `Experiment.Completed.V1` | Experiment Tracking | `ExperimentId`, `RunId`, `Metrics` (lista de `ExperimentMetric`) |
| `Experiment.Failed.V1` | Experiment Tracking | `ExperimentId`, `RunId`, `ErrorMessage` |
| `Dataset.Created.V1` | Research Dataset Registry | `DatasetId`, `Name`, `OwnerId` |
| `Reproducibility.PackageCreated.V1` | Research Reproducibility | `PackageId`, `ExperimentRunId` |
| `Reproducibility.Verified.V1` | Research Reproducibility | `PackageId`, `IsVerified` |
| `MarketMicrostructure.ModelCalibrated.V1` | Market Microstructure | `ModelId`, `AssetSymbol` |
| `ExecutionSimulation.Completed.V1` | Execution Simulator | `SimulationId`, `Status` |

---

## 7. Interfaces de Infraestrutura e Repositório Completas
### 7.1 Interfaces de Infraestrutura
| Nome | Módulo | Métodos Obrigatórios |
|------|--------|-----------------------|
| `IUnitOfWork` | Core | `SaveChangesAsync`, `BeginTransactionAsync`, `CommitTransactionAsync`, `RollbackTransactionAsync` |
| `IColdStorageService` | Cold Storage | `UploadAsync`, `DownloadAsync`, `ExistsAsync`, `DeleteAsync` |
| `IEventSerializer` | Events | `Serialize`, `Deserialize` |
| `IEventPublisher` | Events | `PublishOutboxAsync` |

### 7.2 Interfaces de Repositório
| Nome | Módulo | Métodos Obrigatórios |
|------|--------|-----------------------|
| `IMarketDataSourceRepository` | Market Data Lake | `GetByIdAsync`, `GetAllAsync`, `AddAsync`, `UpdateAsync`, `GetActiveAsync` |
| `IMarketDataIngestionJobRepository` | Market Data Lake | `GetByIdAsync`, `GetByDataSourceIdAsync`, `AddAsync`, `UpdateAsync` |
| `IDataQualityJobRepository` | Data Quality Engine | `GetByIdAsync`, `GetAllAsync`, `AddAsync`, `UpdateAsync` |
| `IAnomalyRepository` | Data Quality Engine | `GetByIdAsync`, `GetByAssetSymbolAndPeriodAsync`, `GetUnresolvedAsync`, `AddAsync`, `UpdateAsync` |
| `IFeatureRepository` | Feature Store | `GetByIdAsync`, `GetAllAsync`, `GetByNameAsync`, `GetApprovedAsync`, `AddAsync`, `UpdateAsync` |
| `IFeatureLineageRepository` | Feature Lineage | `GetByIdAsync`, `GetByFeatureIdAsync`, `AddAsync` |
| `IExperimentRepository` | Experiment Tracking | `GetByIdAsync`, `GetByUserIdAsync`, `AddAsync`, `UpdateAsync` |
| `IExperimentRunRepository` | Experiment Tracking | `GetByIdAsync`, `GetByExperimentIdAsync`, `AddAsync`, `UpdateAsync` |
| `IResearchDatasetRepository` | Research Dataset Registry | `GetByIdAsync`, `GetAllAsync`, `GetByUserIdAsync`, `AddAsync`, `UpdateAsync` |
| `IReproducibilityPackageRepository` | Research Reproducibility | `GetByIdAsync`, `GetByExperimentRunIdAsync`, `AddAsync`, `UpdateAsync` |
| `IMarketMicrostructureModelRepository` | Market Microstructure | `GetByIdAsync`, `GetAllAsync`, `GetActiveAsync`, `AddAsync`, `UpdateAsync` |
| `IExecutionSimulationRepository` | Execution Simulator | `GetByIdAsync`, `GetAllAsync`, `AddAsync`, `UpdateAsync` |
