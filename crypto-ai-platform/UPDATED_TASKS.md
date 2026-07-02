# 📋 Tasks Atualizadas – PHASE 0: QUANT FOUNDATION
**Plataforma**: Crypto AI Platform  
**Versão**: 1.0 (Revisado)  
**Data**: 2026-06-25  
**Status**: Pronto para Aprovação

---

## 1. Regras Básicas das Tasks
1. **Ordem das Tasks**: Executar na sequência abaixo (não pular)
2. **Cada Task**: Deve ter PR aprovado antes de prosseguir para a próxima
3. **Cobertura de Testes**: Mínimo de 70% para todas as Tasks
4. **Observabilidade**: Adicionar logs estruturados, métricas e tracing em todas as Tasks
5. **Segurança**: Adicionar audit logs, RBAC e RLS em todas as Tasks

---

## 2. Tasks 030A a 038 (PHASE 0)

### Task 030A: Architecture Baseline
**Status**: Pendente  
**Esforço Estimado**: 1 semana  
**Objetivo**: Congelar a arquitetura, ADRs, modelo de domínio, contratos de eventos e estratégias antes de implementar.

#### Tarefas Principais:
1. **Architecture Freeze**: Não alterar arquitetura da Phase 0 sem aprovação
2. Atualizar e aprovar todas as ADRs (docs/adr/)
3. Congelar modelo de domínio (UPDATED_DOMAIN_MODEL.md)
4. Congelar contratos de eventos (docs/contracts/events/)
5. Finalizar estratégias de Storage, Segurança e Observabilidade
6. Aprovar arquitetura com Lead Architect e CTO

#### Critérios de Aceite:
- ✅ PHASE0_BASELINE_APPROVAL.md criado e assinado
- ✅ Todas as ADRs aprovadas
- ✅ Modelo de domínio congelado (nenhuma alteração sem processo)
- ✅ Contratos de eventos congelados (nenhuma alteração sem versionamento)
- ✅ Aprovação final recebida para iniciar Task 031

---

### Task 031: Setup de Infraestrutura Base da Phase 0
**Status**: Pendente  
**Esforço Estimado**: 2 semanas  
**Objetivo**: Configurar todas as dependências de infraestrutura e estrutura de pastas.

#### Tarefas Principais:
1. Atualizar estrutura de pastas do projeto (adicionar `QuantFoundation` em todas as camadas)
2. Criar `docker/phase0/docker-compose.yml` com PostgreSQL/TimescaleDB, Redis, RabbitMQ
3. Adicionar pacotes NuGet necessários ao `infrastructure.csproj`
4. Configurar User Secrets (.NET) para desenvolvimento
5. Configurar abstração `IEventBus` no Domain Layer e implementação RabbitMQ no Infrastructure Layer
6. Criar PR e aprovar

#### Critérios de Aceite:
- ✅ Docker Compose sobe sem erros
- ✅ Abstração `IEventBus` implementada
- ✅ Todos os pacotes NuGet adicionados e compilados
- ✅ Estrutura de pastas criada corretamente

---

### Task 032: Domain Layer Completo da Phase 0
**Status**: Pendente  
**Esforço Estimado**: 3 semanas  
**Objetivo**: Implementar todas as entidades, value objects, domain events e interfaces de repositório do Domain Layer.

#### Tarefas Principais:
1. Implementar todas as Aggregate Roots (UPDATED_DOMAIN_MODEL.md)
2. Implementar todas as Child Entities
3. Implementar todos os Value Objects
4. Implementar todos os Domain Events
5. Implementar todas as Interfaces de Repositório
6. Adicionar testes unitários para entidades e value objects (cobertura ≥ 90%)
7. Criar PR e aprovar

#### Critérios de Aceite:
- ✅ Todas as entidades herdam de `BaseEntity<Guid>`
- ✅ Todos os value objects são `record` e imutáveis
- ✅ Todos os testes unitários passam
- ✅ Nenhuma regra de negócio violada

---

### Task 033: Configuração do Banco de Dados (PostgreSQL/TimescaleDB)
**Status**: Pendente  
**Esforço Estimado**: 2 semanas  
**Objetivo**: Configurar ApplicationDbContext, migrations, RLS e TimescaleDB hypertables.

#### Tarefas Principais:
1. Atualizar `ApplicationDbContext.cs` com todos os novos DbSets
2. Configurar Row Level Security (RLS) para todas as tabelas de usuário
3. Configurar TimescaleDB hypertables e índices para todas as tabelas de séries temporais
4. Criar primeira migration: `Phase0_QuantFoundation_Initial`
5. Testar migration em ambiente Docker
6. Criar PR e aprovar

#### Critérios de Aceite:
- ✅ Migration aplica-se sem erros
- ✅ RLS está ativado e funciona corretamente
- ✅ Hypertables e índices são criados
- ✅ `docker-compose up` cria banco com todas as tabelas

---

### Task 034: Infrastructure Layer Completo da Phase 0
**Status**: Pendente  
**Esforço Estimado**: 4 semanas  
**Objetivo**: Implementar repositórios concretos, serviços externos e workers.

#### Tarefas Principais:
1. Implementar todos os repositórios concretos
2. Implementar serviço de criptografia AES-256-GCM para Exchange API Keys
3. Implementar integração com Azure Key Vault/AWS Secrets Manager
4. Implementar `BinanceIngestionWorker` (ingestão de dados OHLCV/trades real-time e histórico)
5. Implementar `DataQualityCheckWorker` (detecção de 8 tipos de anomalias)
6. Implementar `FeatureCalculationWorker` (cálculo de features periodicamente)
7. Adicionar testes de integração (cobertura ≥ 70%)
8. Criar PR e aprovar

#### Critérios de Aceite:
- ✅ Todos os repositórios implementados e testados
- ✅ Criptografia de API Keys funciona corretamente
- ✅ Workers consomem e publicam eventos via RabbitMQ
- ✅ Todos os testes de integração passam

---

### Task 035: Application Layer Completo da Phase 0
**Status**: Pendente  
**Esforço Estimado**: 4 semanas  
**Objetivo**: Implementar todos os commands, queries e handlers CQRS.

#### Tarefas Principais:
1. Implementar commands/queries/handlers para Market Data Lake
2. Implementar commands/queries/handlers para Data Quality Engine
3. Implementar commands/queries/handlers para Feature Store + Feature Lineage
4. Implementar commands/queries/handlers para Experiment Tracking Completo
5. Implementar commands/queries/handlers para Research Dataset Registry
6. Implementar commands/queries/handlers para Research Reproducibility
7. Implementar commands/queries/handlers para Market Microstructure
8. Implementar commands/queries/handlers para Execution Simulator
9. Adicionar validadores FluentValidation para todos os commands
10. Adicionar testes unitários para handlers (cobertura ≥ 80%)
11. Criar PR e aprovar

#### Critérios de Aceite:
- ✅ Todos os commands/queries/handlers implementados
- ✅ Todos os commands têm validação FluentValidation
- ✅ Todos os testes unitários passam
- ✅ Pipeline MediatR funciona corretamente

---

### Task 036: Presentation Layer Completo da Phase 0
**Status**: Pendente  
**Esforço Estimado**: 3 semanas  
**Objetivo**: Implementar todos os controllers REST com versionamento.

#### Tarefas Principais:
1. Implementar `MarketDataSourcesController`, `MarketDataAssetsController`, `OhlcvController`, `TradesController`
2. Implementar `DataQualityJobsController`, `AnomaliesController`
3. Implementar `FeaturesController`, `FeatureLineageController`, `FeatureCatalogController`
4. Implementar `ExperimentsController`, `ExperimentRunsController`, `ExperimentMetricsController`, `ExperimentArtifactsController`
5. Implementar `ResearchDatasetsController`, `DatasetVersionsController`
6. Implementar `ReproducibilityPackagesController`
7. Implementar `MarketMicrostructureModelsController`
8. Implementar `ExecutionSimulationsController`, `SimulatedOrdersController`
9. Adicionar documentação OpenAPI/Swagger para todos os endpoints
10. Adicionar testes de integração para controllers (cobertura ≥ 70%)
11. Criar PR e aprovar

#### Critérios de Aceite:
- ✅ Todos os controllers implementados
- ✅ Todos os endpoints estão documentados no Swagger
- ✅ Todos os endpoints exigem autenticação
- ✅ Todos os testes de integração passam

---

### Task 037: Observabilidade e Segurança da Phase 0
**Status**: Pendente  
**Esforço Estimado**: 3 semanas  
**Objetivo**: Implementar observabilidade completa e segurança obrigatória.

#### Tarefas Principais:
1. Configurar Serilog para logs estruturados (console + arquivo + OpenTelemetry)
2. Configurar OpenTelemetry para métricas (Prometheus) e tracing (Jaeger)
3. Criar dashboards Grafana para todos os módulos
4. Configurar alertas Prometheus AlertManager
5. Implementar audit logs para todos os endpoints de escrita
6. Implementar RBAC com novas permissões da Phase 0
7. Testar todas as políticas de segurança
8. Criar PR e aprovar

#### Critérios de Aceite:
- ✅ Logs estruturados são gerados para todas as ações
- ✅ Métricas são exportadas para Prometheus
- ✅ Tracing distribuído funciona corretamente
- ✅ Todos os alertas estão configurados
- ✅ Audit logs são salvos e não podem ser alterados/excluídos

---

### Task 038: Integração Final e Testes End-to-End da Phase 0
**Status**: Pendente  
**Esforço Estimado**: 3 semanas  
**Objetivo**: Realizar integração final, testes E2E e validação completa.

#### Tarefas Principais:
1. Testar fluxo completo: Ingestão de dados → Detecção de anomalias → Cálculo de features → Criação de experimento → Reprodução de experimento
2. Implementar testes E2E com Playwright/Selenium (ou SpecFlow para BDD)
3. Criar 100+ experimentos de exemplo e reproduzir todos com sucesso
4. Ingerir 1 ano de dados históricos para 100+ símbolos (BTCUSDT, ETHUSDT, etc.)
5. Validar taxa de qualidade de dados ≥ 99.9%
6. Validadar latência de ingestão real-time < 1s
7. Atualizar toda a documentação técnica
8. Criar PR final e aprovar

#### Critérios de Aceite (Definição de Pronto da Phase 0):
- ✅ Todos os testes E2E passam
- ✅ Fluxo completo funciona sem erros
- ✅ 100+ experimentos são reproduzidos com sucesso
- ✅ 1 ano de dados históricos é ingerido e validado
- ✅ Taxa de qualidade de dados ≥ 99.9%
- ✅ Latência de ingestão real-time < 1s
- ✅ Toda a documentação está atualizada
- ✅ Todas as tarefas anteriores estão aprovadas

---

## 3. Resumo da Phase 0
- **Total de Tasks**: 9 (030A, 031-038)
- **Esforço Total Estimado**: 25 semanas (~6 meses)
- **Cobertura de Testes Alvo**: ≥ 70%
- **Definição de Pronto**: Task 038 concluída e aprovada
