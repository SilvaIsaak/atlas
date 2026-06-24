# Crypto AI Platform - Decision Matrix
## Índice
1. [Decisões Tomadas](#decisões-tomadas)
2. [Por Que Essa Stack?](#por-que-essa-stack)

---

## Decisões Tomadas
| Categoria | Decisão | Razão | Alternativas Consideradas |
|-----------|---------|-------|---------------------------|
| Linguagem Backend | C# 12/.NET 9 LTS | Boa performance, typing forte, ecossistema grande, suporte a EF Core | Node.js, Go, Python |
| Arquitetura Backend | Clean Architecture + DDD + CQRS + Mediator | Separação de responsabilidades, testabilidade, manutenibilidade | Hexagonal, Layered tradicional |
| Banco de Dados Relacional | PostgreSQL + TimescaleDB | Confiável, extensível, TimescaleDB ideal para séries temporais (dados de mercado) | MySQL, SQL Server |
| Cache | Redis | Rápido, popular, boa integração com .NET | Memcached |
| Mensageria | Apache Kafka (KRaft) | Escalável, ideal para streaming de eventos | RabbitMQ |
| Observabilidade | OpenTelemetry + Prometheus + Grafana + Loki + Tempo | Padrão de mercado, open source, integração com .NET | Datadog, New Relic |
| Frontend | Next.js 14 (App Router) + TypeScript + Tailwind CSS + shadcn/ui | Fullstack React, SSR/SSG, performance, componentes prontos | React SPA (Vite), Vue.js, Angular |
| Contêineres | Docker + Docker Compose | Isolamento, fácil de reproduzir ambiente | Podman |
| CI/CD | GitHub Actions | Integração com GitHub, free para repos públicos | GitLab CI, CircleCI, Azure DevOps |

---

## Por Que Essa Stack?
Essas decisões foram tomadas para garantir:
1. **Performance**: Para backtesting, dados de mercado e live trading
2. **Segurança**: Para criptomoedas e finanças
3. **Manutenibilidade**: Para evoluir o projeto ao longo do tempo
4. **Escalabilidade**: Para suportar mais usuários e mais dados
5. **Testabilidade**: Para garantir que o código funciona corretamente
