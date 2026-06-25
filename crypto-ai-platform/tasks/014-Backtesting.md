# Task 014: Backtesting

## Objetivo
Implementar o módulo de backtesting para a Crypto AI Platform! Permitir que os usuários executem backtests de suas estratégias de trading em dados históricos, com métricas de performance detalhadas!

## Status
✅ Implementado com sucesso!

## Critérios de Aceite
- [x] Criar entidades Backtest, BacktestStatus e BacktestResult na camada Domain
- [x] Adicionar DbSet<Backtest> no ApplicationDbContext
- [x] Criar queries/commands na camada Application para gerenciar backtests
- [x] Criar endpoints na camada Presentation para o módulo de backtesting
- [x] Implementar execução de backtest (simulada por enquanto)

## Detalhes da Implementação
### Domain
- Criar enum `BacktestStatus` (Pending, Running, Completed, Failed)
- Criar record `BacktestResult` com métricas de performance (TotalReturn, SharpeRatio, SortinoRatio, MaxDrawdown, etc.)
- Criar entidade `Backtest` como Aggregate Root

### Infrastructure
- Atualizar `ApplicationDbContext` com `DbSet<Backtest>`

### Application
- Criar `CreateBacktestCommand` e handler para criar um novo backtest
- Criar `GetBacktestQuery` e handler para buscar um backtest por ID
- Criar `GetAllBacktestsQuery` e handler para listar todos os backtests de um usuário
- Criar `ExecuteBacktestCommand` e handler para executar um backtest

### Presentation
- Criar `BacktestingController` com endpoints para gerenciar e executar backtests

## Arquivos Criados/Atualizados
- `packages/domain/Backtesting/BacktestStatus.cs`
- `packages/domain/Backtesting/BacktestResult.cs`
- `packages/domain/Backtesting/Backtest.cs`
- `packages/infrastructure/Data/ApplicationDbContext.cs`
- `packages/application/Backtesting/CreateBacktestCommand.cs`
- `packages/application/Backtesting/CreateBacktestCommandHandler.cs`
- `packages/application/Backtesting/GetBacktestQuery.cs`
- `packages/application/Backtesting/GetBacktestQueryHandler.cs`
- `packages/application/Backtesting/GetAllBacktestsQuery.cs`
- `packages/application/Backtesting/GetAllBacktestsQueryHandler.cs`
- `packages/application/Backtesting/ExecuteBacktestCommand.cs`
- `packages/application/Backtesting/ExecuteBacktestCommandHandler.cs`
- `packages/presentation/Controllers/BacktestingController.cs`
- `tasks/014-Backtesting.md`

## Próximas Tarefas
- Task 015: WalkForward