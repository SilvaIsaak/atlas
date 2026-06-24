# Crypto AI Platform - Testing Guidelines

## Índice
1. [Pirâmide de Testes](#pirâmide-de-testes)
2. [Ferramentas Utilizadas](#ferramentas-utilizadas)
3. [Testes Unitários](#testes-unitários)
4. [Testes de Integração](#testes-de-integração)
5. [Testes E2E](#testes-e2e)
6. [Testes de Performance](#testes-de-performance)
7. [Testes de Segurança](#testes-de-segurança)

---

## Pirâmide de Testes
| Tipo | Quantidade | Objetivo |
|------|------------|---------|
| Unitários | Muitos | Testar partes individuais (Classes, Methods, Value Objects) |
| Integração | Médio | Testar integração entre componentes (Banco de Dados, Kafka, Redis) |
| E2E | Poucos | Testar fluxo completo (frontend + backend + serviços externos) |

---

## Ferramentas Utilizadas
| Camada | Ferramentas |
|-------|------------|
| Backend .NET | xUnit, Moq, FluentAssertions, TestContainers |
| Frontend React/Next.js | Jest, React Testing Library, Playwright |
| Performance | k6 |
| Segurança | OWASP ZAP, Trivy, Snyk |

---

## Testes Unitários
- **Testar Domain Layer extensivamente** (100% de cobertura para Value Objects e Domain Services)
- **Testar Application Layer** (Handlers, Validators)
- Usar **Moq** para mocar dependências externas
- Usar **FluentAssertions** para assertions (mais legíveis)
- Padrão Arrange-Act-Assert (AAA)
- Cobertura de código mínima **80% para Domain e Application Layers**
- Exemplo:
  ```csharp
  public class InitialCapitalTests
  {
      [Fact]
      public void Create_WithZero_ThrowsException()
      {
          // Arrange
          decimal value = 0;

          // Act
          Action act = () => new InitialCapital(value);

          // Assert
          act.Should().Throw<ArgumentException>();
      }

      [Fact]
      public void Create_WithPositiveValue_Success()
      {
          // Arrange
          decimal value = 1000;

          // Act
          var initialCapital = new InitialCapital(value);

          // Assert
          initialCapital.Value.Should().Be(value);
      }
  }
  ```

---

## Testes de Integração
- Testar integração com:
  - Banco de dados (PostgreSQL/TimescaleDB)
  - Redis
  - Kafka
- Usar **TestContainers** para criar containers temporários para testes (evitar usar banco de desenvolvimento compartilhado)
- Exemplo:
  ```csharp
  public class StrategyRepositoryTests : IAsyncLifetime
  {
      private readonly PostgreSqlContainer _postgresContainer;
      private readonly ApplicationDbContext _dbContext;

      public StrategyRepositoryTests()
      {
          _postgresContainer = new PostgreSqlBuilder()
              .WithImage("timescale/timescaledb:latest-pg16")
              .Build();
      }

      public async Task InitializeAsync()
      {
          await _postgresContainer.StartAsync();
          var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseNpgsql(_postgresContainer.GetConnectionString())
              .Options;
          _dbContext = new ApplicationDbContext(options);
          await _dbContext.Database.MigrateAsync();
      }

      [Fact]
      public async Task AddStrategy_ShouldSucceed()
      {
          // Arrange & Act
          // ...

          // Assert
      }

      public async Task DisposeAsync()
      {
          await _postgresContainer.StopAsync();
          await _postgresContainer.DisposeAsync();
      }
  }
  ```

---

## Testes E2E
- Usar **Playwright** para frontend
- Testar fluxos principais (login, criar estratégia, backtest, etc.)
- Usar ambiente de teste separado
- Sempre limpar dados após testes

---

## Testes de Performance
- Usar **k6** para simular carga em APIs
- Testar endpoints de backtesting (alto uso de recursos)
- Objetivos:
  - Tempo de resposta < 500ms para endpoints comuns
  - Suportar pelo menos 100 requisições/segundo

---

## Testes de Segurança
- **SAST (Static Application Security Testing)**: SonarQube
- **SCA (Software Composition Analysis)**: Snyk/GitHub Dependabot
- **DAST (Dynamic Application Security Testing)**: OWASP ZAP
- Verificar vulnerabilidades em imagens Docker: Trivy
- Testes de penetração em ambiente staging
