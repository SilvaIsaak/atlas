# 🛠️ Especificação Técnica de Implementação – PHASE 0: QUANT FOUNDATION
**Plataforma**: Crypto AI Platform  
**Versão**: 1.0  
**Data**: 2026-06-25  
**Status**: Pronto para Desenvolvimento

---

## 1. Introdução
Esta especificação define **como implementar** a PHASE 0 — QUANT FOUNDATION, seguindo a arquitetura enterprise definida anteriormente. Todas as regras de Clean Architecture, CQRS, DDD e guidelines do projeto são aplicadas.

---

## 2. Pré-requisitos de Ambiente
| Ferramenta | Versão Requerida | Justificativa |
|------------|-------------------|----------------|
| .NET SDK | 9.0 | Framework principal do backend |
| Docker Desktop | 4.30+ | Para containers de PostgreSQL, TimescaleDB, Redis, RabbitMQ/Kafka |
| Node.js | 20+ | Para frontend (se necessário) |
| Git | 2.40+ | Controle de versão |
| Azure CLI / AWS CLI | Última | Para cloud (se aplicável) |

---

## 3. Estrutura de Pastas Atualizada
Adicione as novas pastas ao projeto existente:
```
crypto-ai-platform/
├── docs/
│   └── phase0/                          # Novos docs da Phase 0
├── packages/
│   ├── domain/
│   │   ├── QuantFoundation/            # NOVO: Entidades de domínio da Phase 0
│   │   │   ├── MarketData/
│   │   │   ├── DataQuality/
│   │   │   ├── FeatureStore/
│   │   │   ├── ExperimentTracking/
│   │   │   ├── ResearchDataset/
│   │   │   ├── Reproducibility/
│   │   │   ├── MarketMicrostructure/
│   │   │   └── ExecutionSimulator/
│   ├── application/
│   │   ├── QuantFoundation/            # NOVO: Commands/Queries/Handlers da Phase 0
│   ├── infrastructure/
│   │   ├── QuantFoundation/            # NOVO: Repositórios concretos, serviços externos
│   ├── presentation/
│   │   ├── Controllers/
│   │   │   └── QuantFoundation/        # NOVO: Controllers da Phase 0
├── docker/
│   ├── phase0/                         # NOVO: Docker compose da Phase 0
└── tests/
    ├── unit/
    │   └── QuantFoundation/            # NOVO: Testes unitários da Phase 0
    ├── integration/
    │   └── QuantFoundation/            # NOVO: Testes de integração da Phase 0
```

---

## 4. Pacotes NuGet Requeridos (Infrastructure Layer)
Adicione ao arquivo `packages/infrastructure/infrastructure.csproj`:
```xml
<PackageReference Include="Npgsql" Version="9.0.0" />
<PackageReference Include="Npgsql.TimescaleDB" Version="1.0.0" />
<PackageReference Include="StackExchange.Redis" Version="2.10.0" />
<PackageReference Include="RabbitMQ.Client" Version="7.0.0" />
<PackageReference Include="Confluent.Kafka" Version="2.8.0" />
<PackageReference Include="Parquet.Net" Version="4.16.0" />
<PackageReference Include="Azure.Storage.Blobs" Version="12.22.0" />
<PackageReference Include="AWSSDK.S3" Version="3.7.400" />
```

---

## 5. Passo-a-Passo de Implementação
Implementar na seguinte ordem, respeitando dependências:

### 5.1 Passo 1: Criar Entidades de Domínio e Value Objects (Domain Layer)
| Arquivo | Conteúdo |
|---------|----------|
| `packages/domain/QuantFoundation/MarketData/MarketDataSource.cs` | Aggregate Root `MarketDataSource` |
| `packages/domain/QuantFoundation/MarketData/MarketDataIngestionJob.cs` | Entity `MarketDataIngestionJob` |
| `packages/domain/QuantFoundation/MarketData/MarketDataAsset.cs` | Entity `MarketDataAsset` |
| `packages/domain/QuantFoundation/MarketData/ValueObjects/OhlcvData.cs` | Value Object `OhlcvData` |
| `packages/domain/QuantFoundation/MarketData/ValueObjects/TradeData.cs` | Value Object `TradeData` |
| `packages/domain/QuantFoundation/DataQuality/Anomaly.cs` | Entity `Anomaly` |
| `packages/domain/QuantFoundation/FeatureStore/Feature.cs` | Aggregate Root `Feature` |
| `packages/domain/QuantFoundation/ExperimentTracking/Experiment.cs` | Aggregate Root `Experiment` |
| `packages/domain/QuantFoundation/ResearchDataset/ResearchDataset.cs` | Aggregate Root `ResearchDataset` |
| `packages/domain/QuantFoundation/Reproducibility/ReproducibilityPackage.cs` | Aggregate Root `ReproducibilityPackage` |
| `packages/domain/QuantFoundation/MarketMicrostructure/MarketMicrostructureModel.cs` | Aggregate Root `MarketMicrostructureModel` |
| `packages/domain/QuantFoundation/ExecutionSimulator/ExecutionSimulation.cs` | Aggregate Root `ExecutionSimulation` |

**Regras**:
- Todas as entidades herdam de `BaseEntity<Guid>`
- Todos os value objects são `record` e imutáveis
- Todas as regras de negócio estão no Domain Layer

---

### 5.2 Passo 2: Criar Domain Events (Domain Layer)
| Arquivo | Conteúdo |
|---------|----------|
| `packages/domain/QuantFoundation/MarketData/Events/MarketDataIngestedEvent.cs` | Evento `MarketDataIngestedEvent` |
| `packages/domain/QuantFoundation/DataQuality/Events/AnomalyDetectedEvent.cs` | Evento `AnomalyDetectedEvent` |
| `packages/domain/QuantFoundation/FeatureStore/Events/FeatureCreatedEvent.cs` | Evento `FeatureCreatedEvent` |
| `packages/domain/QuantFoundation/ExperimentTracking/Events/ExperimentStartedEvent.cs` | Evento `ExperimentStartedEvent` |

---

### 5.3 Passo 3: Criar Interfaces de Repositório (Domain Layer)
| Arquivo | Conteúdo |
|---------|----------|
| `packages/domain/QuantFoundation/MarketData/Repositories/IMarketDataSourceRepository.cs` | Interface `IMarketDataSourceRepository` |
| `packages/domain/QuantFoundation/DataQuality/Repositories/IAnomalyRepository.cs` | Interface `IAnomalyRepository` |
| `packages/domain/QuantFoundation/FeatureStore/Repositories/IFeatureRepository.cs` | Interface `IFeatureRepository` |
| `packages/domain/QuantFoundation/ExperimentTracking/Repositories/IExperimentRepository.cs` | Interface `IExperimentRepository` |

---

### 5.4 Passo 4: Configurar Banco de Dados (Infrastructure Layer)
1. **Atualizar `ApplicationDbContext.cs`**:
   - Adicionar DbSets para todas as novas entidades
   - Configurar RLS (Row Level Security)
   - Configurar TimescaleDB hypertables

2. **Criar Migrations**:
   ```powershell
   cd packages/infrastructure
   dotnet ef migrations add Phase0_QuantFoundation --startup-project ../apps/api-core
   ```

3. **Criar Docker Compose para Phase 0**:
   Arquivo: `docker/phase0/docker-compose.yml`
   ```yaml
   version: '3.8'
   services:
     postgres:
       image: timescale/timescaledb:latest-pg16
       environment:
         POSTGRES_USER: postgres
         POSTGRES_PASSWORD: postgres
         POSTGRES_DB: cryptoaiplatform_phase0
       ports:
         - "5432:5432"
       volumes:
         - postgres_data:/var/lib/postgresql/data
     redis:
       image: redis:alpine
       ports:
         - "6379:6379"
     rabbitmq:
       image: rabbitmq:3-management-alpine
       ports:
         - "5672:5672"
         - "15672:15672"
       environment:
         RABBITMQ_DEFAULT_USER: guest
         RABBITMQ_DEFAULT_PASS: guest
   volumes:
     postgres_data:
   ```

---

### 5.5 Passo 5: Implementar Repositórios Concretos (Infrastructure Layer)
| Arquivo | Conteúdo |
|---------|----------|
| `packages/infrastructure/QuantFoundation/MarketData/Repositories/MarketDataSourceRepository.cs` | Implementação `IMarketDataSourceRepository` |
| `packages/infrastructure/QuantFoundation/DataQuality/Repositories/AnomalyRepository.cs` | Implementação `IAnomalyRepository` |

---

### 5.6 Passo 6: Implementar Commands/Queries/Handlers (Application Layer)
Exemplo para `CreateMarketDataSourceCommand`:
```csharp
// packages/application/QuantFoundation/MarketData/Commands/CreateMarketDataSourceCommand.cs
using MediatR;

namespace CryptoAIPlatform.Application.QuantFoundation.MarketData.Commands;

public record CreateMarketDataSourceCommand : IRequest<CreateMarketDataSourceResponse>
{
    public string Name { get; init; } = string.Empty;
    public string BaseUrl { get; init; } = string.Empty;
    public string ApiKey { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}

public record CreateMarketDataSourceResponse(Guid DataSourceId);

// Handler
// packages/application/QuantFoundation/MarketData/Commands/CreateMarketDataSourceCommandHandler.cs
using MediatR;

namespace CryptoAIPlatform.Application.QuantFoundation.MarketData.Commands;

public class CreateMarketDataSourceCommandHandler : IRequestHandler<CreateMarketDataSourceCommand, CreateMarketDataSourceResponse>
{
    private readonly IMarketDataSourceRepository _repository;

    public CreateMarketDataSourceCommandHandler(IMarketDataSourceRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateMarketDataSourceResponse> Handle(CreateMarketDataSourceCommand request, CancellationToken cancellationToken)
    {
        var dataSource = new MarketDataSource
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            BaseUrl = request.BaseUrl,
            ApiKey = request.ApiKey, // NÃO FAÇA ISSO EM PROD! Use Vault!
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(dataSource, cancellationToken);

        return new CreateMarketDataSourceResponse(dataSource.Id);
    }
}
```

---

### 5.7 Passo 7: Implementar Controllers (Presentation Layer)
Exemplo para `MarketDataSourcesController`:
```csharp
// packages/presentation/Controllers/QuantFoundation/MarketDataSourcesController.cs
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAIPlatform.Presentation.Controllers.QuantFoundation;

[ApiController]
[Route("api/v{version:apiVersion}/market-data/sources")]
[ApiVersion("1.0")]
[Authorize]
public class MarketDataSourcesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MarketDataSourcesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateMarketDataSourceResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateMarketDataSourceCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.DataSourceId }, result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MarketDataSource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetMarketDataSourceByIdQuery(id));
        return result != null ? Ok(result) : NotFound();
    }
}
```

---

### 5.8 Passo 8: Implementar Workers (Infrastructure Layer)
Implementar workers background para ingestão de dados:
| Worker | Objetivo |
|--------|----------|
| `BinanceIngestionWorker` | Ingerir dados da Binance (Spot/Futures) |
| `DataQualityCheckWorker` | Verificar qualidade de dados periodicamente |
| `FeatureCalculationWorker` | Calcular features periodicamente |

---

### 5.9 Passo 9: Implementar Testes (Tests Layer)
1. **Testes Unitários**: Testar entidades, value objects, handlers
2. **Testes de Integração**: Testar controllers, repositórios, workers
3. **Cobertura de Testes**: Alvo inicial de 70%

---

## 6. Exemplo de Pull Request (PR) Template para Phase 0
Crie um arquivo `.github/PULL_REQUEST_TEMPLATE/phase0.md`:
```markdown
## Descrição
Breve descrição da tarefa implementada.

## Tarefa Relacionada
Link para a tarefa do board (se aplicável).

## Tipo de Mudança
- [ ] Nova Feature
- [ ] Correção de Bug
- [ ] Refatoração
- [ ] Documentação

## Checklist
- [ ] Eu segui as regras de Clean Architecture e DDD
- [ ] Eu adicionei testes unitários e de integração
- [ ] Todos os testes passam
- [ ] Eu atualizei a documentação
- [ ] Eu adicionei logs e métricas de observabilidade
- [ ] Eu segui as regras de segurança (audit logs, RBAC, RLS)

## Screenshots (se aplicável)
```

---

## 7. Critérios de Aceite para Cada Tarefa
1. ✅ Código segue todas as regras de Clean Architecture, CQRS e DDD
2. ✅ Testes unitários e de integração passam
3. ✅ Observabilidade está implementada (logs estruturados, métricas, tracing)
4. ✅ Segurança está implementada (audit logs, RBAC, RLS, secrets em vault)
5. ✅ Documentação está atualizada
6. ✅ PR está aprovado por pelo menos um colega

---

## 8. Planejamento de Tarefas (24 Semanas)
| Semana | Módulo | Tarefas Principais |
|--------|--------|---------------------|
| 1-2 | Setup | Configurar ambiente, criar estrutura de pastas, adicionar pacotes NuGet |
| 3-4 | Market Data Lake - Domain | Criar entidades, value objects, domain events, interfaces de repositório |
| 5-6 | Market Data Lake - Infra/Application/Tests | Implementar repositórios, commands/queries/handlers, controllers, testes |
| 7-8 | Market Data Lake - Workers | Implementar workers de ingestão, integração com Binance |
| 9-10 | Data Quality Engine | Implementar tudo (domain, infra, application, tests, workers) |
| 11-14 | Feature Store (Expandido) | Implementar tudo |
| 15-16 | Experiment Tracking | Implementar tudo |
| 17-18 | Research Dataset Registry | Implementar tudo |
| 19-20 | Research Reproducibility | Implementar tudo |
| 21-22 | Market Microstructure | Implementar tudo |
| 23-24 | Execution Simulator | Implementar tudo, integração final, testes end-to-end |

---

## 9. Recursos Úteis
- [Documentação do TimescaleDB](https://docs.timescale.com/)
- [Documentação do StackExchange.Redis](https://stackexchange.github.io/StackExchange.Redis/)
- [Documentação do RabbitMQ.Client](https://www.rabbitmq.com/dotnet.html)
- [Documentação do Confluent.Kafka](https://docs.confluent.io/kafka-clients/dotnet/current/overview.html)
