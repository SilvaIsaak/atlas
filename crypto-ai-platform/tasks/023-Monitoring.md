# Task 23: Monitoring

## Objetivo
Implementar o módulo de Monitoring para a Crypto AI Platform! Permitir o monitoramento de métricas do sistema (CPU, memória, disco), desempenho de API, banco de dados) e alertas do sistema!

## Status
✅ Implementado com sucesso!

## Critérios de Aceite
- [x] Criar entidades SystemMetric, SystemAlert, MetricType na camada Domain
- [x] Adicionar DbSet<SystemMetric> e DbSet<SystemAlert> no ApplicationDbContext
- [x] Criar queries na camada Application para obter métricas e alertas
- [x] Criar comando na camada Application para reconhecer alertas
- [x] Criar endpoints na camada Presentation para o módulo de Monitoring

## Detalhes da Implementação
### Domain
- Criar enum `MetricType` (SystemCpu, SystemMemory, SystemDisk, ApiLatency, ApiRequests, ApiErrors, DatabaseQueries, DatabaseLatency
- Criar entidade `SystemMetric` como Aggregate Root
- Criar entidade `SystemAlert` como Aggregate Root

### Infrastructure
- Atualizar `ApplicationDbContext` com `DbSet<SystemMetric>` e `DbSet<SystemAlert>`

### Application
- Criar `GetSystemMetricsQuery` e handler
- Criar `GetSystemAlertsQuery` e handler
- Criar `AcknowledgeSystemAlertCommand` e handler

### Presentation
- Criar `MonitoringController`

## Arquivos Criados/Atualizados
- `packages/domain/Monitoring/MetricType.cs`
- `packages/domain/Monitoring/SystemMetric.cs`
- `packages/domain/Monitoring/SystemAlert.cs`
- `packages/infrastructure/Data/ApplicationDbContext.cs`
- `packages/application/Monitoring/GetSystemMetricsQuery.cs`
- `packages/application/Monitoring/GetSystemMetricsQueryHandler.cs`
- `packages/application/Monitoring/GetSystemAlertsQuery.cs`
- `packages/application/Monitoring/GetSystemAlertsQueryHandler.cs`
- `packages/application/Monitoring/AcknowledgeSystemAlertCommand.cs`
- `packages/application/Monitoring/AcknowledgeSystemAlertCommandHandler.cs`
- `packages/presentation/Controllers/MonitoringController.cs`
- `tasks/023-Monitoring.md`

## Próximas Tarefas
- Task 24: Learning