# Crypto AI Platform - Database Guidelines

## Índice
1. [Bancos de Dados Utilizados](#bancos-de-dados-utilizados)
2. [Padrões de Nomenclatura](#padrões-de-nomenclatura)
3. [Modelagem de Dados](#modelagem-de-dados)
4. [EF Core](#ef-core)
5. [TimescaleDB (Dados de Mercado)](#timescaledb-dados-de-mercado)
6. [Migrações](#migrações)
7. [Consultas](#consultas)

---

## Bancos de Dados Utilizados
| Banco | Uso |
|-------|-----|
| PostgreSQL + TimescaleDB | Dados de negócio, dados de mercado (séries temporais) |
| Redis | Cache, tokens revogados, sessões |

---

## Padrões de Nomenclatura
- **Tabelas**: Plural, snake_case (ex: `strategies`, `market_data`, `users`)
- **Colunas**: Snake_case (ex: `id`, `created_at`, `strategy_name`)
- **Chaves Primárias**: `id` (UUID/Guid por padrão; para tabelas TimescaleDB, usar `id` + `timestamp`)
- **Chaves Estrangeiras**: `{tabela_singular}_id` (ex: `strategy_id`, `user_id`)
- **Índices**: `idx_{tabela}_{coluna(s)}` (ex: `idx_strategies_user_id`, `idx_market_data_symbol_timestamp`)
- **Constraints**: `pk_{tabela}` (PK), `fk_{tabela}_{tabela_referenciada}` (FK), `uq_{tabela}_{coluna(s)}` (Unique), `ck_{tabela}_{descricao}` (Check)

---

## Modelagem de Dados
- **Entidades de Negócio**: Armazenadas em tabelas no PostgreSQL padrão
- **Entidades Base**: Todas as entidades devem herdar de `BaseEntity<Guid>` (com `Id`, `CreatedAt`, `UpdatedAt`, e `DomainEvents`)
- **Value Objects**: Mapeados como colunas simples ou owned entities no EF Core
- **Agregados**: Somente acessados via Agregado Raiz

---

## EF Core
- Usar **EF Core 9+** com provedor `Npgsql.EntityFrameworkCore.PostgreSQL`
- Configurar `SnakeCaseNamingConvention` para mapear para snake_case
- Usar `DbContext` separado para Identity (se necessário) ou mesmo `ApplicationDbContext` com tabelas de Identity
- Configurar `UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)` para consultas com Includes
- Usar `AsNoTracking()` para consultas que não alteram dados (melhora performance)
- Implementar **Repository Pattern** para acesso a dados (somente para Agregados Raiz)

---

## TimescaleDB (Dados de Mercado)
- Tabelas de séries temporais (candles, tickers, order book snapshots) devem ser **Hypertables** do TimescaleDB
- Particionar por `timestamp` (intervalo de 1 dia ou 1 semana, dependendo da frequência)
- Usar compressão em dados antigos
- Usar Continuous Aggregates para queries complexas frequentemente usadas
- Exemplo de tabela de candles como hypertable:
  ```sql
  CREATE TABLE market_candles (
      id UUID PRIMARY KEY,
      symbol VARCHAR(20) NOT NULL,
      timeframe VARCHAR(10) NOT NULL,
      timestamp TIMESTAMPTZ NOT NULL,
      open NUMERIC(18,8) NOT NULL,
      high NUMERIC(18,8) NOT NULL,
      low NUMERIC(18,8) NOT NULL,
      close NUMERIC(18,8) NOT NULL,
      volume NUMERIC(18,8) NOT NULL,
      created_at TIMESTAMPTZ DEFAULT NOW()
  );
  SELECT create_hypertable('market_candles', by_range('timestamp'));
  ```

---

## Migrações
- Usar **EF Core Migrations** para gerenciar schema
- Migrações em projeto separado (ou em `Infrastructure`)
- Sempre testar migrações em ambiente de staging antes de aplicar em produção
- Ter plano de rollback para cada migração
- Migrações para TimescaleDB (criar hypertables, etc.) devem ser feitas com SQL raw em migrations

---

## Consultas
- Para consultas de leitura (CQRS Query Side), usar **Dapper** ou EF Core com `AsNoTracking()`
- Para consultas complexas em dados de mercado, usar TimescaleDB functions (time_bucket, first, last, etc.)
- Sempre usar índices para consultas frequentes (verificando `EXPLAIN ANALYZE`)
- Evitar `SELECT *` (especificar apenas colunas necessárias)
- Pagináção para consultas que retornam muitos dados
