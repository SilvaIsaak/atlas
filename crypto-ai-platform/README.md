# Crypto AI Platform
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 9](https://img.shields.io/badge/.NET-9-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Next.js 14](https://img.shields.io/badge/Next.js-14-000000?logo=next.js)](https://nextjs.org/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker)](https://www.docker.com/)
[![Kafka](https://img.shields.io/badge/Apache%20Kafka-231F20?logo=apachekafka)](https://kafka.apache.org/)

## Overview
The **Crypto AI Platform** is an enterprise-grade, scalable platform for developing, backtesting, paper-trading, and live-trading quantitative trading strategies for cryptocurrencies, with AI/ML support. The platform is built following Clean Architecture, Domain-Driven Design (DDD), CQRS, and SOLID principles, ensuring high maintainability, scalability, and security.

## Key Features
- **Strategy Development**: Support for writing strategies in C#, Python, and JavaScript/TypeScript.
- **Advanced Backtesting**: High-performance backtesting with slippage, commission, and latency simulation.
- **Paper Trading**: Real-time simulated trading using actual market data and virtual money.
- **Live Trading**: Real trading with multiple exchanges (Binance, Coinbase Pro, Kraken).
- **Automated Risk Management**: Customizable risk rules with automatic enforcement.
- **Security**: JWT authentication, 2FA, API key encryption, rate limiting.
- **Observability**: OpenTelemetry, Prometheus, Grafana, Loki, Jaeger.
- **Scalability**: Microservices, Kafka, Kubernetes, Redis.

## Architecture
For a detailed description of the architecture, see the [`.trae/` directory](./.trae), especially:
- [`PROJECT_CONTEXT.md`](./.trae/PROJECT_CONTEXT.md) — Project context, objectives, requirements.
- [`ARCHITECTURE_RULES.md`](./.trae/ARCHITECTURE_RULES.md) — Mandatory architecture rules.
- [`PROJECT_MASTER_PROMPT.md`](./.trae/PROJECT_MASTER_PROMPT.md) — The "brain" of the project for AI-assisted development.

### Architecture Overview (Clean Architecture & C4 Model)
The platform uses a modular monolith approach that can evolve into microservices, with a clear separation of concerns:
1. **Domain Layer**: Core business logic, entities, value objects, aggregates, domain services, domain events.
2. **Application Layer**: Use cases, CQRS commands/queries, MediatR handlers, DTOs.
3. **Infrastructure Layer**: Database access (EF Core + PostgreSQL + TimescaleDB), external integrations (exchanges), messaging (Kafka), cache (Redis), secrets management.
4. **Presentation Layer**: REST API (ASP.NET Core Web API), Frontend (Next.js 14 + React + TypeScript + Tailwind CSS + shadcn/ui).

## Tech Stack
### Backend
- **Framework**: .NET 9 LTS
- **Web API**: ASP.NET Core 9
- **ORM**: Entity Framework Core 9
- **CQRS/MediatR**: MediatR
- **Validation**: FluentValidation
- **Logging**: Serilog
- **Observability**: OpenTelemetry
- **Messaging**: Apache Kafka + Confluent.Kafka

### Databases
- **Relational**: PostgreSQL 16 + TimescaleDB (for time-series data like market prices)
- **Cache/Queues**: Redis 7

### Frontend
- **Framework**: Next.js 14 + App Router
- **UI Library**: React 18 + TypeScript
- **Styling**: Tailwind CSS
- **Components**: shadcn/ui

### Infrastructure & DevOps
- **Containerization**: Docker
- **Orchestration**: Kubernetes
- **Infrastructure as Code**: Terraform
- **CI/CD**: GitHub Actions
- **Monitoring**: Prometheus + Grafana + Loki + Jaeger

## Getting Started
### Prerequisites
To run the platform locally, you need:
- Docker & Docker Compose
- .NET 9 SDK (for backend development)
- Node.js 20+ & pnpm (for frontend development)

### Local Setup (Docker Compose)
The easiest way to run the platform locally is using Docker Compose:
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/crypto-ai-platform.git
   cd crypto-ai-platform
   ```
2. Copy the example environment variables file and configure it:
   ```bash
   cp .env.example .env
   # Edit .env with your configuration
   ```
3. Start the services (PostgreSQL, Redis, Kafka, etc.):
   ```bash
   docker-compose up -d
   ```
4. Run database migrations (for .NET backend):
   ```bash
   cd apps/api-core
   dotnet ef database update --connection "Host=localhost;Database=crypto_ai_platform;Username=postgres;Password=yourpassword"
   ```
5. Access the frontend at `http://localhost:3000` and the API at `http://localhost:5000`.

## Contributing
We welcome contributions! Please read our [Contributing Guide](./docs/CONTRIBUTING.md) and [Git Workflow](./.trae/GIT_WORKFLOW.md) before starting.

### Development Workflow
1. Create a new branch from `develop`: `git checkout -b feature/your-feature-name`.
2. Implement your changes, following our coding standards.
3. Write tests for your changes.
4. Commit using [Conventional Commits](./.trae/GIT_WORKFLOW.md#2-convenções-de-commits-semânticos).
5. Open a Pull Request to `develop`.

## License
This project is licensed under the MIT License — see the [LICENSE](./LICENSE) file for details.

## Contact
- Project Lead: [Your Name]
- GitHub: [Your GitHub Organization/User]
