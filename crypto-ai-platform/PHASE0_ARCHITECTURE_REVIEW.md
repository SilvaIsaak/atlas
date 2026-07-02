# 🔍 Revisão de Arquitetura – PHASE 0: QUANT FOUNDATION (v1 Final)
**Plataforma**: Crypto AI Platform  
**Versão**: 1.0 (Revisada)  
**Data**: 2026-06-25  
**Status**: Pronto para Aprovação

---

## 1. Resumo das Alterações Principais
Esta revisão ajusta a arquitetura da Phase 0 para simplicidade, clareza e conformidade com requisitos críticos:

| Requisito | Implementação |
|-----------|----------------|
| ✅ 1. TimescaleDB para séries temporais | Definido como único armazenamento para dados de mercado, features, spreads |
| ✅ 2. PostgreSQL para metadados | Definido para entidades de domínio, audit logs, identity |
| ✅ 3. Redis para cache distribuído | Definido para features frequentes, dados em tempo real, rate limiting |
| ✅ 4/5. Apenas RabbitMQ como Event Bus | Kafka REMOVIDO da v1 (simplicidade inicial) |
| ✅ 6. Data Lake Parquet (S3/Azure Blob) | Cold storage para dados históricos, artefatos |
| ✅ 7. Módulo Feature Lineage | Adicionado ao Feature Store |
| ✅ 8. Experiment Tracking Completo | Todas as entidades adicionadas |
| ✅ 9. EventBus Architecture Definida | Tópicos/filas, producers/consumers, abstração |
| ✅ 10. Política Secrets Management Obrigatória | Definida em detalhes |
| ✅ 11. Criptografia para Exchange API Keys | Definida usando AES-256-GCM |
| ✅ 12. Domain Model Atualizado | Link para documento separado |
| ✅ 13. Roadmap Atualizado | Incluído abaixo |
| ✅ 14. Tasks 031-038 Atualizadas | Link para documento separado |

---

## 2. Arquitetura de Dados Definitiva (v1)
```
┌───────────────────────────────────────────────────────────────────────────┐
│                        DADOS DE DOMÍNIO E METADADOS                        │
│                          ───────────────────────                          │
│  POSTGRESQL (Único para Metadata e Entidades Não-SerieTemporal)           │
│  - Identity & Permissões                                                   │
│  - Audit Logs                                                              │
│  - Entidades de Domínio (MarketDataSource, Feature, Experiment, etc.)     │
│  - Row Level Security (RLS) ATIVADO                                       │
└───────────────────────────────────────────────────────────────────────────┘
                                      │
                                      │
┌───────────────────────────────────────────────────────────────────────────┐
│                        DADOS DE SÉRIES TEMPORAIS                           │
│                          ───────────────────────                          │
│  TIMESCALEDB (Extensão do PostgreSQL, Único para Time-Series)             │
│  - OHLCV (1s, 1m, 1h, 1d)                                                 │
│  - Trades                                                                  │
│  - Order Book Snapshots                                                    │
│  - Funding Rates, Open Interest, Liquidations                              │
│  - Features Calculadas                                                     │
│  - Spreads Históricos                                                      │
│  - Hypertables + Compressão ATIVADA                                        │
└───────────────────────────────────────────────────────────────────────────┘
                                      │
                                      │
┌───────────────────────────────────────────────────────────────────────────┐
│                        CACHE DISTRIBUÍDO E TEMPO REAL                      │
│                          ───────────────────────                          │
│  REDIS (Único para Cache e Real-Time)                                     │
│  - Cache de Features Frequentes (TTL: 1h)                                 │
│  - Dados de Mercado em Tempo Real (Pub/Sub)                               │
│  - Rate Limiting                                                           │
│  - Cache de Resultados de Experimentos Pequenos                            │
└───────────────────────────────────────────────────────────────────────────┘
                                      │
                                      │
┌───────────────────────────────────────────────────────────────────────────┐
│                        COLD STORAGE E DATA LAKE                            │
│                          ───────────────────────                          │
│  S3 / AZURE BLOB STORAGE (Único para Cold Storage)                        │
│  - Dados históricos > 2 anos (Parquet, particionados)                     │
│  - Artefatos de Experimentos (Relatórios, Gráficos, Parquets)             │
│  - Pacotes de Reprodução (tar.gz)                                          │
│  - Criptografia Server-Side ATIVADA (SSE-S3/SSE-AES)                      │
└───────────────────────────────────────────────────────────────────────────┘
                                      │
                                      │
┌───────────────────────────────────────────────────────────────────────────┐
│                        COMUNICAÇÃO ASSÍNCRONA                              │
│                          ───────────────────────                          │
│  RABBITMQ (Único Event Bus, Kafka REMOVIDO da v1)                        │
│  - Exchange: `cryptoaiplatform.direct`                                     │
│  - Filas Duráveis, Dead-Letter Queues (DLQ) ATIVADAS                      │
│  - Abstração `IEventBus` para desacoplar implementação                    │
└───────────────────────────────────────────────────────────────────────────┘
```

---

## 3. EventBus Architecture Definitiva (v1)
### 3.1 Abstrações Adicionais (Domain Layer)
```csharp
// packages/domain/Abstractions/Events/IEventBus.cs
namespace CryptoAIPlatform.Domain.Abstractions.Events;

public interface IEventBus
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : DomainEvent;
    Task SubscribeAsync<TEvent, THandler>(CancellationToken cancellationToken = default) 
        where TEvent : DomainEvent 
        where THandler : IEventHandler<TEvent>;
}

// packages/domain/Abstractions/Events/IEventHandler.cs
namespace CryptoAIPlatform.Domain.Abstractions.Events;

public interface IEventHandler<TEvent> where TEvent : DomainEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}

// packages/domain/Abstractions/Events/IEventSerializer.cs
namespace CryptoAIPlatform.Domain.Abstractions.Events;

public interface IEventSerializer
{
    string Serialize<TEvent>(TEvent @event) where TEvent : DomainEvent;
    TEvent Deserialize<TEvent>(string serialized) where TEvent : DomainEvent;
}

// packages/domain/Abstractions/Events/IEventPublisher.cs
namespace CryptoAIPlatform.Domain.Abstractions.Events;

public interface IEventPublisher
{
    Task PublishOutboxAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : DomainEvent;
}
```

### 3.2 Idempotency Keys
- **Todas as mensagens/eventos têm um `IdempotencyKey` (Guid) único
- Consumers armazenam chaves processadas no PostgreSQL (tabela `ProcessedEvents` com TTL de 30 dias)
- Se chave já existe, consumer ignora a mensagem

### 3.3 Outbox Pattern
- **Objetivo**: Garantir entrega at-least-once sem transações distribuídas
- **Implementação**:
  1. Salvar evento na tabela `OutboxMessages` (PostgreSQL) na mesma transação que a operação de domínio
  2. Worker `OutboxProcessor` publica eventos do `OutboxMessages` para RabbitMQ
  3. Marca evento como `Processed` após sucesso
- **Tabela Outbox**: `Id`, `EventType`, `Payload`, `CreatedAt`, `ProcessedAt`, `IsProcessed`, `TenantId`

### 3.4 Inbox Pattern
- **Objetivo**: Garantir processamento exactly-once
- **Implementação**:
  1. Consumer salva mensagem na tabela `InboxMessages` (PostgreSQL) com `IdempotencyKey`
  2. Se já existe, ignora
  3. Se não, processa
  4. Marca como `Processed` após sucesso
- **Tabela Inbox**: `Id`, `IdempotencyKey`, `EventType`, `Payload`, `CreatedAt`, `ProcessedAt`, `IsProcessed`, `TenantId`

### 3.5 Política de Versionamento dos Eventos
- **SemVer para eventos (Major.Minor.Patch
- **Campos obrigatórios em TODOS os eventos**: `Version`, `CorrelationId`, `CausationId`, `OccurredAt`, `TenantId`
- **Nomes de eventos: `{Module}.{Action}.V{Major}` (ex: `MarketData.Ingested.V1`)

### 3.6 Estratégia de Compatibilidade Entre Versões de Eventos
- **Backward Compatibility Obrigatória**:
  - Não remover campos existentes
  - Não alterar tipo de campos existentes
  - Novos campos devem ser opcionais (com valor padrão)
- **Forward Compatibility Recomendada**:
  - Consumers ignoram campos desconhecidos
- **Quando incrementar Major**:
  - Alterações breaking (ex: remover campo obrigatório, alterar tipo de campo)
- **Quando incrementar Minor**:
  - Adicionar campos opcionais
- **Quando incrementar Patch**:
  - Correções de bugs no payload (não alteram contrato)

### 3.7 Implementação (Infrastructure Layer – RabbitMQ)
- Usa `RabbitMQ.Client` (versão 7.0.0+)
- Filas duráveis, mensagens persistentes
- DLQ para mensagens que falham (3 tentativas com backoff exponencial)
- Usa Polly para Retry + Circuit Breaker
  - Retry: 3 tentativas com backoff exponencial (1s, 2s, 4s)
  - Circuit Breaker: Abre após 5 falhas consecutivas, fecha após 30s

### 3.8 Tópicos/Filas Definitivos
| Exchange | Routing Key | Fila | Producer | Consumer |
|----------|--------------|------|----------|----------|
| `cryptoaiplatform.direct` | `marketdata.ingested.v1` | `queue.marketdata.ingested.v1` | BinanceIngestionWorker | FeatureCalculationWorker, DataQualityCheckWorker |
| `cryptoaiplatform.direct` | `dataquality.anomaly.critical.v1` | `queue.dataquality.anomaly.critical.v1` | DataQualityCheckWorker | AlertingService |
| `cryptoaiplatform.direct` | `feature.created.v1` | `queue.feature.created.v1` | FeatureService | NotificationsService |
| `cryptoaiplatform.direct` | `experiment.completed.v1` | `queue.experiment.completed.v1` | ExperimentService | ReportingService, NotificationsService |

---

## 4. Novas Abstrações de Infraestrutura (Domain Layer)
### 4.1 IUnitOfWork
```csharp
// packages/domain/Abstractions/IUnitOfWork.cs
namespace CryptoAIPlatform.Domain.Abstractions;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
```

### 4.2 IColdStorageService
```csharp
// packages/domain/Abstractions/IColdStorageService.cs
namespace CryptoAIPlatform.Domain.Abstractions;

public interface IColdStorageService
{
    Task UploadAsync(string path, byte[] data, CancellationToken cancellationToken = default);
    Task<byte[]> DownloadAsync(string path, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string path, CancellationToken cancellationToken = default);
    Task DeleteAsync(string path, CancellationToken cancellationToken = default);
}
```

---

## 5. Estratégia de Redis Atualizada
### 5.1 Prefixo Obrigatório para Chaves do Redis
- **Todas as chaves do Redis**: `tenant:{TenantId}:{Module}:{Key}` (ex: `tenant:123e4567-e89b-12d3-a456-426614174000:features:feature-123`)
- **Isolamento**: Em produção, usar database Redis separada por Tenant

### 5.2 Estratégia de Migração de Banco
- **Ferramenta**: Entity Framework Core Migrations
- **Regras**:
  1. Toda migração tem nome: `V{Major}.{Minor}.{Patch}_{Description}` (ex: `V1.0.0_Phase0Initial`)
  2. Migrações são idempotentes (usar `IF NOT EXISTS`)
  3. Migrações são testadas em staging antes de produção
  4. Backup automático do banco antes de aplicar migração
  5. Rollback manual se necessário (apenas em staging/testes, produção usa backup)
- **Fluxo**:
  1. Criar migration local
  2. Testar em dev
  3. Testar em staging
  4. Aplicar em produção (com backup prévio)

---

## 6. Política de Secrets Management Atualizada
### 6.1 Ambientes Clarificados
- **Desenvolvimento**: Apenas Docker Secrets / User Secrets (.NET) (NÃO USAR VAULT EM DEV)
- **Staging/Produção**: Azure Key Vault / AWS Secrets Manager (OBRIGATÓRIO)
- **NENHUM secret em appsettings.json ou docker-compose.yml**

### 6.2 Polly para Retry + Circuit Breaker (Integração Externa
- **Biblioteca**: Polly (versão 8+)
- **Configuração**:
  - Retry: 3 tentativas com backoff exponencial (1s, 2s, 4s)
  - Circuit Breaker: Abre após 5 falhas consecutivas, fecha após 30s
  - Aplicar em todas as chamadas a exchanges e serviços externos

---

## 4. Política Obrigatória de Secrets Management
### 4.1 Regras Absolutas
1. **NENHUM secret hardcoded** em código, appsettings.json, docker-compose.yml, etc.
2. **Todos os secrets** devem ser armazenados em um gerenciador de segredos:
   - **Desenvolvimento**: Docker Secrets / User Secrets (.NET)
   - **Staging/Produção**: Azure Key Vault / AWS Secrets Manager
3. **Rotatividade de secrets**:
   - Chaves API de exchanges: a cada 90 dias
   - Senhas de banco: a cada 60 dias
   - JWT Secret Key: a cada 30 dias
4. **Auditoria**: Todos os acessos a secrets são logados (retido eternamente)

### 4.2 Criptografia de Exchange API Keys
Para chaves API de exchanges (Binance, Bybit, etc.):
1. **Algoritmo**: AES-256-GCM (autenticado, seguro)
2. **Armazenamento**:
   - Chave criptografada armazenada no PostgreSQL (campo `EncryptedApiKey`)
   - Nonce (12 bytes) e Tag (16 bytes) armazenados junto
3. **Chave de Criptografia**:
   - Armazenada **apenas** no gerenciador de segredos (nunca no banco)
   - Nome: `CryptoAIPlatform-ExchangeApiEncryptionKey`

---

## 5. Módulos Adicionados/Atualizados
### 5.1 Módulo Feature Lineage (Integrado ao Feature Store)
#### Objetivo
Rastrear a origem de cada feature até os dados brutos do Market Data Lake.
#### Entidades Adicionadas
| Nome | Tipo | Descrição |
|------|------|-----------|
| `FeatureLineage` | Entity | Lineage completo da feature (dependências de dados brutos, transformações, versões) |
| `FeatureLineageNode` | Value Object | Nó do lineage (tipo: `RawData`, `Feature`, `Transformation`) |

#### APIs Adicionadas
- `GET /api/v1/features/{id}/lineage`: Obter lineage de uma feature (formato JSON + Graphviz DOT para visualização)

---

## 6. Roadmap Atualizado (v1)
| Fase | Nome | Esforço Estimado | Riscos | Dependências | Definição de Pronto |
|------|------|-------------------|--------|---------------|----------------------|
| **PHASE 0** | QUANT FOUNDATION | 24 semanas (~6 meses) | Simplicidade RabbitMQ vs escalabilidade futura | Nenhuma | Ver detalhes em UPDATED_TASKS.md |
| FASE 1 | SEGURANÇA CRÍTICA | 4 semanas | - | PHASE 0 | - |
| FASE 2 | QUALIDADE | 6 semanas | - | FASE 1 | - |
| FASE 3 | TRADING FOUNDATION | 8 semanas | - | FASE 2 | - |
| FASE 4 | PAPER TRADING | 9 semanas | - | FASE 3 | - |
| FASE 5 | LIVE TRADING READINESS | 10 semanas | - | FASE 4 | - |

---

## 7. Links para Documentos Anexos
1. **UPDATED_DOMAIN_MODEL.md**: Modelo de domínio completo e atualizado
2. **UPDATED_TASKS.md**: Tasks 031-038 da Phase 0, detalhadas

---

## 8. Aprovação Requerida
Esta arquitetura está pronta para revisão. Aguardamos aprovação para iniciar a implementação.

| Cargo | Nome | Assinatura | Data |
|-------|------|------------|------|
| Lead Architect | [Pendente] | [Pendente] | [Pendente] |
| CTO | [Pendente] | [Pendente] | [Pendente] |
