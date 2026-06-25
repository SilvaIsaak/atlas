# Task 013: Strategies

## Objetivo
Implementar o módulo de estratégias para a Crypto AI Platform! Permitir que os usuários criem, editem, gerenciem e vejam suas estratégias de trading quantitativas!

## Status
✅ Implementado com sucesso!

## Critérios de Aceite
- [x] Criar entidade Strategy na camada Domain
- [x] Criar queries/commands na camada Application para gerenciar estratégias
- [x] Criar endpoints na camada Presentation para o módulo de estratégias
- [x] Integrar com o Research Engine (Task 012) para converter estudos em estratégias
- [x] Adicionar DbSet<Strategy> no ApplicationDbContext

## Detalhes da Implementação
### Domain
- Criar entidade `Strategy` como Aggregate Root
- Criar enum `StrategyStatus` para controlar o estado da estratégia (Draft, Active, Paused, Archived)

### Infrastructure
- Atualizar `ApplicationDbContext` com `DbSet<Strategy>`

### Application
- Criar `CreateStrategyCommand` e handler para criar uma nova estratégia
- Criar `UpdateStrategyCommand` e handler para editar uma estratégia existente
- Criar `GetStrategyQuery` e handler para buscar uma estratégia por ID
- Criar `GetAllStrategiesQuery` e handler para listar todas as estratégias de um usuário
- Criar `ChangeStrategyStatusCommand` e handler para alterar o status da estratégia

### Presentation
- Criar `StrategiesController` com endpoints para gerenciar estratégias

## Arquivos Criados/Atualizados
- `packages/domain/Strategies/Strategy.cs`
- `packages/domain/Strategies/StrategyStatus.cs`
- `packages/infrastructure/Data/ApplicationDbContext.cs`
- `packages/application/Strategies/CreateStrategyCommand.cs`
- `packages/application/Strategies/CreateStrategyCommandHandler.cs`
- `packages/application/Strategies/UpdateStrategyCommand.cs`
- `packages/application/Strategies/UpdateStrategyCommandHandler.cs`
- `packages/application/Strategies/GetStrategyQuery.cs`
- `packages/application/Strategies/GetStrategyQueryHandler.cs`
- `packages/application/Strategies/GetAllStrategiesQuery.cs`
- `packages/application/Strategies/GetAllStrategiesQueryHandler.cs`
- `packages/application/Strategies/ChangeStrategyStatusCommand.cs`
- `packages/application/Strategies/ChangeStrategyStatusCommandHandler.cs`
- `packages/presentation/Controllers/StrategiesController.cs`
- `tasks/013-Strategies.md`

## Próximas Tarefas
- Task 014: Backtesting
