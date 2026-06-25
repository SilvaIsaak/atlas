# Task 17: RiskEngine

## Objetivo
Implementar o módulo de Risk Engine para a Crypto AI Platform! Permitir que os usuários criem perfis de risco, regras de gerenciamento de risco e recebam alertas!

## Status
✅ Implementado com sucesso!

## Critérios de Aceite
- [x] Criar entidades RiskProfile, RiskRule, RiskAlert, RiskAlertLevel na camada Domain
- [x] Adicionar DbSet<RiskProfile> no ApplicationDbContext
- [x] Criar queries/commands na camada Application para gerenciar perfis de risco
- [x] Criar endpoints na camada Presentation para o módulo de Risk Engine

## Detalhes da Implementação
### Domain
- Criar enum `RiskAlertLevel`
- Criar records `RiskAlert`, `RiskRule`
- Criar entidade `RiskProfile` como Aggregate Root

### Infrastructure
- Atualizar `ApplicationDbContext` com `DbSet<RiskProfile>`

### Application
- Criar `CreateRiskProfileCommand` e handler
- Criar `GetRiskProfileQuery` e handler
- Criar `GetAllRiskProfilesQuery` e handler

### Presentation
- Criar `RiskController`

## Arquivos Criados/Atualizados
- `packages/domain/RiskManagement/RiskAlertLevel.cs`
- `packages/domain/RiskManagement/RiskAlert.cs`
- `packages/domain/RiskManagement/RiskRule.cs`
- `packages/domain/RiskManagement/RiskProfile.cs`
- `packages/infrastructure/Data/ApplicationDbContext.cs`
- `packages/application/RiskManagement/CreateRiskProfileCommand.cs`
- `packages/application/RiskManagement/CreateRiskProfileCommandHandler.cs`
- `packages/application/RiskManagement/GetRiskProfileQuery.cs`
- `packages/application/RiskManagement/GetRiskProfileQueryHandler.cs`
- `packages/application/RiskManagement/GetAllRiskProfilesQuery.cs`
- `packages/application/RiskManagement/GetAllRiskProfilesQueryHandler.cs`
- `packages/presentation/Controllers/RiskController.cs`
- `tasks/017-Risk-Engine.md`

## Próximas Tarefas
- Task 18: AIDecision