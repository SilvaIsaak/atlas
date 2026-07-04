# BUILD_ERRORS_ANALYSIS

Resumo inicial dos erros do build (resultado do último `dotnet build`). Este arquivo será atualizado iterativamente.

Formato: Categoria | Quantidade (aprox.) | Arquivos afetados (exemplos) | Correção necessária

- Missing DbSets / ApplicationDbContext references | ~50 | Admin/GetAdminLogsQueryHandler.cs; Dashboard/GetDashboardQueryHandler.cs; Backtesting/*; Wallets/*; LiveTrading/*; Notifications/*; Learning/*; PaperTrading/*; Strategies/*; Research/* | Reconciliar `ApplicationDbContext`: adicionar/expor DbSets necessários ou adaptar handlers para usar repositórios existentes. Validar EntityConfigurations e relacionamentos.

- Inaccessible BaseEntity setters (Id/CreatedAt) | ~20 | Backtesting/CreateBacktestCommandHandler.cs; Execution/CreateExecutionEngineCommandHandler.cs; Mobile/RegisterMobileDeviceCommandHandler.cs; Research/CreateResearchStudyCommand.cs; Strategies/CreateStrategyCommandHandler.cs; etc. | Ajustar modelos de domínio (construtores / factories) ou adaptar handlers para não setar propriedades com setters inacessíveis. Não alterar sem coordenação com Domain — preferir usar construtores/factories.

- Namespace vs Type ambiguities | ~8 | News/GetNewsQuery.cs ("News" namespace); Deployment/CreateDeploymentCommandHandler.cs; WalkForward/CreateWalkForwardCommandHandler.cs; AIDecision/GenerateAIDecisionCommandHandler.cs | Usar aliases ou `using` corretos; garantir que referências apontem para o tipo esperado. Não renomear módulos.

- MediatR API mismatches | 1 | DependencyInjection.cs | Atualizar registros do MediatR para API da versão instalada (ex.: `RegisterServicesFromAssembly` / `AddOpenBehavior` não disponíveis). Usar `services.AddMediatR(...)` ou API compatível.

- Exchange client / connector API mismatches | ~3 | Wallets/SyncWalletBalancesCommand.cs; MarketScanner/* | Consumidores chamam `GetClient`/`GetTickerAsync`/`GetKlinesAsync` mas infra expõe `CreateClient` e métodos diferentes. Harmonizar chamadas ou adicionar adaptadores de compatibilidade.

- Method signature mismatches / repository APIs | 1-2 | Reproducibility/GetReproducibilityPackageQueryHandler.cs | Ajustar chamadas para usar a assinatura atual do repositório (`GetByExperimentRunIdAsync`).

- DTO / Type mismatches (namespaces) | ~3 | RiskManagement/CreateRiskProfileCommandHandler.cs (RiskProfile em namespace diferente) | Corrigir usings/aliases ou mapear DTOs adequadamente.

- Others (compilation errors que exigem investigação específica) | ~5 | Reports/CreateReportCommandHandler.cs (Report type missing) | Rever referências e imports.

Warnings:
- `CryptoAIPlatform.Infrastructure` compilou com avisos (observações em infra). Manter listagem em paralelo.

Observação: números são estimativas iniciais extraídas do log do build. Irei iterar as correções por categoria, atualizando este arquivo com contagens exatas e arquivos restantes conforme avançarmos.
