# Task 15: WalkForward

## Objetivo
Implementar o módulo de Walk‑Forward Analysis para a Crypto AI Platform! Permitir que os usuários validem estratégias com análise walk‑forward, um método rigoroso de validação out‑of‑sample!

## Status
✅ Implementado com sucesso!

## Critérios de Aceite
- [x] Criar entidades WalkForward, WalkForwardStatus e WalkForwardWindowResult na camada Domain
- [x] Adicionar DbSet<WalkForward> no ApplicationDbContext
- [x] Criar queries/commands na camada Application para gerenciar walk‑forwards
- [x] Criar endpoints na camada Presentation para o módulo de walk‑forward
- [x] Implementar execução de walk‑forward (simulada)

## Detalhes da Implementação
### Domain
- Criar enum `WalkForwardStatus` (Pending, Running, Completed, Failed)
- Criar record `WalkForwardWindowResult` com métricas de cada janela
- Criar entidade `WalkForward` como Aggregate Root

### Infrastructure
- Atualizar `ApplicationDbContext` com `DbSet<WalkForward>`

### Application
- Criar `CreateWalkForwardCommand` e handler
- Criar `GetWalkForwardQuery` e handler
- Criar `GetAllWalkForwardsQuery` e handler
- Criar `ExecuteWalkForwardCommand` e handler

### Presentation
- Criar `WalkForwardController`

## Arquivos Criados/Atualizados
- `packages/domain/WalkForward/WalkForwardStatus.cs`
- `packages/domain/WalkForward/WalkForwardWindowResult.cs`
- `packages/domain/WalkForward/WalkForward.cs`
- `packages/infrastructure/Data/ApplicationDbContext.cs`
- `packages/application/WalkForward/CreateWalkForwardCommand.cs`
- `packages/application/WalkForward/CreateWalkForwardCommandHandler.cs`
- `packages/application/WalkForward/GetWalkForwardQuery.cs`
- `packages/application/WalkForward/GetWalkForwardQueryHandler.cs`
- `packages/application/WalkForward/GetAllWalkForwardsQuery.cs`
- `packages/application/WalkForward/GetAllWalkForwardsQueryHandler.cs`
- `packages/application/WalkForward/ExecuteWalkForwardCommand.cs`
- `packages/application/WalkForward/ExecuteWalkForwardCommandHandler.cs`
- `packages/presentation/Controllers/WalkForwardController.cs`
- `tasks/015-WalkForward.md`

## Próximas Tarefas
- Task 16: PaperTrading