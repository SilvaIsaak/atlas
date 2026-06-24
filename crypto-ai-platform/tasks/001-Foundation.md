# Task 001: Foundation (Base do Projeto)

## Objetivo
Criar a base estrutural do projeto, seguindo Clean Architecture, DDD, CQRS e todos os padrões enterprise definidos na documentação. Incluir a configuração do monorepo para backend (.NET) e frontend (Next.js), configuração do Docker Compose para ambiente de desenvolvimento e CI/CD.

## Status
✅ Concluído

## Critérios de Aceite
- [x] Monorepo configurado com pnpm (para frontend) e .NET Solution (para backend)
- [x] Camadas da Clean Architecture criadas em .NET: Domain, Application, Infrastructure, Presentation
- [x] Frontend Next.js 14 configurado (estrutura básica)
- [x] Docker Compose configurado com PostgreSQL+TimescaleDB, Redis, Kafka (KRaft), Confluent Schema Registry, Prometheus, Grafana, Loki, Tempo, MinIO, pgAdmin, RedisInsight
- [x] GitHub Actions CI/CD pipeline criada para build e testes
- [x] OpenTelemetry configurado para observabilidade (logs, métricas, tracing)
- [x] .editorconfig e ferramentas de formatação/configuração centralizada no monorepo (Directory.Build.props, Directory.Packages.props)
- [x] .gitignore atualizado
- [x] Estrutura seguindo todas as regras de ARCHITECTURE_RULES.md

## Passos Implementados
1. **Configuração do Monorepo .NET**:
   - Criados `Directory.Build.props` (configuração geral do projeto) e `Directory.Packages.props` (gerenciamento centralizado de versões de pacotes NuGet)
   - Criados 6 projetos .NET organizados em camadas (Domain, Application, Infrastructure, Presentation, Shared, Api)
   - Adicionados pacotes NuGet necessários (MediatR, FluentValidation, EF Core, Npgsql, Serilog, OpenTelemetry, Polly, etc.)

2. **Camadas da Clean Architecture**:
   - **Domain Layer**:
     - Criadas abstrações base (`BaseEntity<TId>`, `ValueObject`, `DomainEvent`, `IAggregateRoot`)
   - **Application Layer**:
     - Criado `AddApplicationServices` (DI) com MediatR e FluentValidation
     - Criado `ValidationBehavior` (Middleware do MediatR para validação de Commands/Queries)
   - **Infrastructure Layer**:
     - Criado `ApplicationDbContext` (EF Core) com suporte a TimescaleDB e atualização automática de timestamps
     - Criado `AddInfrastructureServices` (DI) com EF Core, Redis, OpenTelemetry
   - **Presentation Layer**:
     - Criado `AddPresentationServices` (DI) com Controllers, API Versioning, Swagger/OpenAPI, CORS
   - **Api Core App**:
     - Criado `Program.cs` como ponto de entrada da API, configurando Serilog, DI, Middleware pipeline

3. **Configuração de Docker Compose**:
   - Atualizado `docker-compose.yml` com:
     - Banco de dados: PostgreSQL + TimescaleDB
     - Cache: Redis + RedisInsight
     - Mensageria: Kafka (KRaft mode, sem ZooKeeper) + Confluent Schema Registry
     - Observabilidade: Prometheus, Grafana, Loki, Tempo
     - Object Storage: MinIO
     - Ferramentas de administração: pgAdmin

4. **Configuração de CI/CD (GitHub Actions)**:
   - Atualizado `.github/workflows/ci.yml` para build e testes (placeholder, a ser expandido)

## Arquivos Criados/Atualizados
### Backend e Configuração .NET
- `Directory.Build.props`: Configuração geral do monorepo .NET
- `Directory.Packages.props`: Gerenciamento centralizado de versões de pacotes NuGet
- `packages/domain/CryptoAIPlatform.Domain.csproj`: Projeto da camada Domain
- `packages/domain/Abstractions/BaseEntity.cs`: Abstração base para entidades
- `packages/domain/Abstractions/ValueObject.cs`: Abstração base para value objects
- `packages/domain/Abstractions/DomainEvent.cs`: Abstração base para eventos de domínio
- `packages/domain/Abstractions/IAggregateRoot.cs`: Interface marcadora para agregados raiz
- `packages/application/CryptoAIPlatform.Application.csproj`: Projeto da camada Application
- `packages/application/DependencyInjection.cs`: Extensão DI para Application
- `packages/application/Behaviors/ValidationBehavior.cs`: Behavior de validação do MediatR
- `packages/infrastructure/CryptoAIPlatform.Infrastructure.csproj`: Projeto da camada Infrastructure
- `packages/infrastructure/DependencyInjection.cs`: Extensão DI para Infrastructure
- `packages/infrastructure/Data/ApplicationDbContext.cs`: DbContext do EF Core
- `packages/presentation/CryptoAIPlatform.Presentation.csproj`: Projeto da camada Presentation
- `packages/presentation/DependencyInjection.cs`: Extensão DI para Presentation
- `packages/shared/CryptoAIPlatform.Shared.csproj`: Projeto de código compartilhado
- `apps/api-core/CryptoAIPlatform.Api.csproj`: Projeto principal da API
- `apps/api-core/Program.cs`: Ponto de entrada da API
- `apps/api-core/appsettings.json`: Configurações da API (produção)
- `apps/api-core/appsettings.Development.json`: Configurações da API (desenvolvimento)
- `tests/unit/CryptoAIPlatform.UnitTests.csproj`: Projeto de testes unitários (xUnit, Moq, FluentAssertions)
- `CryptoAIPlatform.sln`: Solution .NET com todos os projetos

### Frontend (Next.js)
- `apps/web/package.json`: Dependências do frontend
- `apps/web/tsconfig.json`: Configuração TypeScript do frontend
- `apps/web/next.config.ts`: Configuração Next.js
- `apps/web/tailwind.config.ts`: Configuração Tailwind CSS
- `apps/web/postcss.config.mjs`: Configuração PostCSS
- `apps/web/app/layout.tsx`: Layout root do Next.js
- `apps/web/app/page.tsx`: Página inicial do Next.js
- `apps/web/app/globals.css`: Estilos globais do Tailwind com Design Tokens
- `apps/web/lib/utils.ts`: Utilitários (cn para clsx + tailwind-merge)
- `apps/web/components/ui/button.tsx`: Componente Button do shadcn/ui
- `apps/web/components/ui/card.tsx`: Componente Card (e variações) do shadcn/ui
- `apps/web/components/ui/input.tsx`: Componente Input do shadcn/ui
- `apps/web/components/ui/label.tsx`: Componente Label do shadcn/ui

### DevOps e Observabilidade
- `docker-compose.yml`: Arquivo Compose com serviços de desenvolvimento
- `config/prometheus.yml`: Configuração do Prometheus
- `config/loki.yml`: Configuração do Loki (logs)
- `config/tempo.yml`: Configuração do Tempo (tracing)
- `config/grafana/provisioning/datasources/datasources.yml`: Datasources do Grafana
- `config/grafana/provisioning/dashboards/dashboards.yml`: Dashboards do Grafana
- `package.json`: Arquivo package.json da raiz do monorepo
- `.github/workflows/ci.yml`: Workflow CI/CD do GitHub Actions

### Guidelines e Documentação
- `.trae/CODING_STANDARDS.md`: Padrões de codificação (C#, TypeScript, React)
- `.trae/API_GUIDELINES.md`: Padrões de API REST
- `.trae/SECURITY_GUIDELINES.md`: Segurança, autenticação, autorização, segredos
- `.trae/DATABASE_GUIDELINES.md`: Banco de dados, PostgreSQL, TimescaleDB, EF Core
- `.trae/TESTING_GUIDELINES.md`: Testes, pirâmide, ferramentas, cobertura
- `.trae/DESIGN_SYSTEM.md`: Cores, tipografia, componentes shadcn/ui
- `.trae/EVENT_GUIDELINES.md`: Eventos, Kafka, Outbox Pattern, Avro Schemas
- `.trae/DEVELOPMENT_WORKFLOW.md`: Git Flow, Conventional Commits, Pull Request Process
- `.trae/QUALITY_GATE.md`: Pré-requisitos para merge, checklist
- `.trae/DECISION_MATRIX.md`: Decisões de arquitetura e stack
- `.trae/AI_FORBIDDEN.md`: O que as IAs NÃO podem fazer
- `.trae/RELEASE_CHECKLIST.md`: Checklist para releases
- `tasks/001-Foundation.md`: Esta task

### Agentes
- `agents/SECURITY_AGENT.md`: Agente de Segurança
- `agents/DATABASE_AGENT.md`: Agente de Banco de Dados

## Como Testar
1. Resturar pacotes NuGet:
   ```bash
   dotnet restore
   ```
2. Build da solução:
   ```bash
   dotnet build CryptoAIPlatform.sln
   ```

## Próximas Tasks
- Task 002: Authentication
