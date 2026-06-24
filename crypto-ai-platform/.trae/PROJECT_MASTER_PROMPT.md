# Crypto AI Platform - Project Master Prompt

## Objetivo

Este documento é o guia definitivo para qualquer desenvolvedor (humano ou IA) que trabalhe no projeto Crypto AI Platform. Ele define a arquitetura, os padrões, as regras, as restrições e todas as práticas necessárias para desenvolver esta plataforma enterprise de trading quantitativo de criptomoedas de forma profissional, segura e escalável.

## Índice

1. [Visão Geral do Projeto](#1-visão-geral-do-projeto)
2. [Arquitetura do Sistema](#2-arquitetura-do-sistema)
3. [Domain-Driven Design (DDD)](#3-domain-driven-design-ddd)
4. [Clean Architecture](#4-clean-architecture)
5. [Princípios SOLID](#5-princípios-solid)
6. [CQRS (Command Query Responsibility Segregation)](#6-cqrs-command-query-responsibility-segregation)
7. [Event-Driven Architecture](#7-event-driven-architecture)
8. [Banco de Dados](#8-banco-de-dados)
9. [Backend (.NET 9)](#9-backend-net-9)
10. [Frontend (Next.js + React + TypeScript)](#10-frontend-nextjs--react--typescript)
11. [Testes](#11-testes)
12. [Logs e Observabilidade](#12-logs-e-observabilidade)
13. [Segurança](#13-segurança)
14. [Performance e Escalabilidade](#14-performance-e-escalabilidade)
15. [Deployment e DevOps](#15-deployment-e-devops)
16. [Revisão de Código e Qualidade](#16-revisão-de-código-e-qualidade)
17. [Regras e Restrições](#17-regras-e-restrições)
18. [Fluxo de Desenvolvimento](#18-fluxo-de-desenvolvimento)

---

## 1. Visão Geral do Projeto

### 1.1. Objetivo da Plataforma
A Crypto AI Platform é uma plataforma enterprise de pesquisa quantitativa e negociação automatizada de criptomoedas. Ela deve permitir:
- Pesquisa e desenvolvimento de estratégias de trading quantitativas utilizando linguagens como Python, C# e JavaScript/TypeScript
- Backtesting rigoroso de estratégias em dados históricos de alta qualidade, com suporte a múltiplas fontes de dados e métricas de performance detalhadas
- Paper trading para validação em ambiente simulado em tempo real, sem risco financeiro
- Live trading em exchanges reais (Binance, Coinbase Pro, Kraken, KuCoin, etc.), com suporte a ordens de mercado, limite, stop loss e take profit
- Gerenciamento de risco automatizado (limites de exposição, drawdown máximo, stop loss global, trailing stop
- Monitoramento em tempo real de performance das estratégias e da plataforma em dashboards interativos e alertas por e-mail/SMS/Push
- Análise de dados de mercado em larga escala, com suporte a indicadores técnicos, fundamentalistas e on-chain
- Integração com múltiplas exchanges de criptomoedas via APIs REST e WebSocket
- Sistema de backtesting de estratégias com walk-forward analysis (WFA) e monte carlo para validação robusta
- Sistema de pesquisa quantitativa com suporte a machine learning (regressão, classificação, reinforcement learning) para previsão de preços e otimização de estratégias
- Sistema de auditoria completa de todas as operações para compliance
- Sistema de relatórios detalhados de performance, incluindo métricas como Sharpe Ratio, Sortino Ratio, Maximum Drawdown, Win Rate, Profit Factor, etc.
- Sistema de multi-tenancy para suportar múltiplos usuários e organizações
- API pública para integração com sistemas externos
- Aplicativo móvel (iOS e Android) para acesso em qualquer lugar

### 1.2. Stack Tecnológica
#### 1.2.1 Frontend
- **Framework**: Next.js 14+ com App Router
- **Linguagem**: TypeScript 5+ com strict mode habilitado e todas as regras de tipo rigorosas
- **UI Components**: React 18+ com Suspense e Server Components/Client Components separados adequadamente
- **Styling**: Tailwind CSS 3.x com shadcn/ui para componentes de interface do usuário acessíveis e modernos
- **Gerenciamento de Estado**: React Query 5+ para dados do servidor, Zustand 4+ para estado global do cliente
- **Formulários**: React Hook Form 7+ com Zod 3+ para validação de esquemas
- **Visualização de Dados**: Recharts 2.x para gráficos, Chart.js 4.x para gráficos simples
- **Testes**: Jest 29+ para testes unitários, React Testing Library 14+ para testes de componentes, Playwright 1.x para testes end-to-end
- **Análise de Código**: ESLint 8+ com regras de @typescript-eslint e eslint-plugin-react, Prettier 3+ para formatação

#### 1.2.2 Backend
- **Framework**: .NET 9 LTS (Long Term Support) com ASP.NET Core Web API 9.0
- **Linguagem**: C# 12 com nullable reference types habilitadas, record types, pattern matching e outras funcionalidades modernas
- **ORM**: Entity Framework Core 9.0 com suporte a PostgreSQL
- **CQRS/MediatR**: MediatR 12+ para implementar Command Query Responsibility Segregation
- **Validação**: FluentValidation 11+ para validação de Commands, Queries e DTOs
- **Resiliência**: Polly 8+ para retentativas, circuit breakers, fallbacks, timeouts e bulkhead policies
- **Autenticação/Autorização**: ASP.NET Core Identity 9+ com JWT (JSON Web Tokens) com chaves assimétricas RS256, OpenID Connect (OIDC) e OAuth 2.0 para integração com provedores de identidade externos (Google, GitHub, Azure AD)
- **Mensageria**: Apache Kafka 3.7+ com Confluent.Kafka 2.3+ para produção e consumo de eventos
- **Cache**: StackExchange.Redis 2.7+ para cache de dados, rate limiting, sessões, distributed locks
- **Logging**: Serilog 3.0+ para logging estruturado em JSON, sinks para Elasticsearch, Grafana Loki, Console e File
- **Tracing/Métricas**: OpenTelemetry 1.8+ para instrumentação, export para Prometheus 2.52+ para métricas, Grafana 11.x para visualização, Jaeger 1.58+ para tracing distribuído
- **Testes**: xUnit 2.6+ para testes unitários, Moq 4.20+ para mocks, FluentAssertions 6.12+ para assertions, Testcontainers 3.9+ para testes de integração com infraestrutura em containers
- **Análise de Código**: SonarQube 10.x, Roslyn Analyzers, dotnet format para linting e formatação

#### 1.2.3 Banco de Dados
- **Relacional**: PostgreSQL 16+ com TimescaleDB para dados de séries temporais (market data)
- **Cache/Chave-Valor**: Redis 7.2+ com Redis Stack para caching, rate limiting, distributed locks, pub/sub
- **Mensageria**: Apache Kafka 3.7+ com KRaft (sem ZooKeeper) para streaming de eventos
- **Object Storage**: MinIO ou AWS S3 para armazenamento de dados históricos grandes, logs, backups

#### 1.2.4 Infraestrutura e DevOps
- **Containerização**: Docker 25+ com Docker Compose 2.24+ para desenvolvimento local
- **Orquestração de Containers**: Kubernetes 1.29+ com Helm 3.14+ para deployment em produção
- **Infraestrutura como Código**: Terraform 1.7+ para provisionamento de recursos na nuvem (AWS, Azure, GCP)
- **CI/CD**: GitHub Actions 2024+ para pipeline de build, testes, security scans, build de imagens Docker e deployment
- **Registry de Imagens**: Docker Hub, AWS ECR, Azure Container Registry ou GitHub Container Registry
- **Secrets Management**: HashiCorp Vault 1.15+ ou AWS Secrets Manager ou Azure Key Vault para gerenciamento seguro de segredos
- **Monitoring/Observability**: Prometheus 2.52+, Grafana 11.x, Grafana Loki 3.0+, Grafana Tempo 2.5+, OpenTelemetry 1.8+
- **Service Mesh**: Istio 1.21+ ou Linkerd 2.14+ para gerenciamento de tráfego, mTLS, observabilidade entre serviços
- **CDN**: Cloudflare ou AWS CloudFront para distribuição de assets frontend e proteção contra DDoS

#### 1.2.5 Outras Ferramentas e Bibliotecas
- **Monorepo**: pnpm 9.x workspaces para Node.js, .NET Solution Files com Directory.Build.props e Directory.Packages.props para gerenciamento de dependências compartilhadas
- **IDE/Editor**: Visual Studio 2022, Visual Studio Code 1.89+, Rider 2024.x
- **Controle de Versão**: Git 2.45+ com GitHub como plataforma de hospedagem de código
- **Gerenciamento de Projetos**: GitHub Projects, Jira ou Asana para gerenciamento de tarefas e sprints
- **Comunicação**: Slack ou Microsoft Teams para comunicação da equipe
- **Documentação**: MkDocs com Material for MkDocs para documentação técnica, GitHub Wiki para documentação de processo

### 1.3. Princípios Fundamentais
Os princípios a seguir guiam todas as decisões arquiteturais, de design e de implementação na Crypto AI Platform. Eles são não-negotiáveis e devem ser seguidos rigorosamente por todos os membros da equipe.

#### 1.3.1 Qualidade em Primeiro Lugar
- Nenhum código é aprovado e mergeado na branch principal sem testes adequados (unitários, integração, e2e quando aplicável)
- Nenhum código é mergeado sem revisão de pelo menos um outro desenvolvedor
- Nenhum código é deployado em produção sem passar por todos os estágios do pipeline de CI/CD
- Qualidade de código é medida por ferramentas de análise de código (SonarQube, ESLint, Roslyn Analyzers)
- Meta de cobertura de código é de no mínimo 80% para camadas de domínio e aplicação
- Todo bug fix deve ter um teste de regressão associado para evitar recorrência

#### 1.3.2 Escalabilidade Horizontal
- Todos os componentes da plataforma devem ser projetados para escalar horizontalmente
- Nenhum componente deve ser um ponto único de falha (SPOF - Single Point of Failure)
- Banco de dados PostgreSQL deve usar read replicas para consultas de leitura
- Kafka deve ter múltiplos brokers e tópicos particionados para paralelismo
- Redis deve ser clusterizado para alta disponibilidade e escalabilidade
- Workers devem ser stateless para permitir scaling in/out automático baseado em carga (HPA - Horizontal Pod Autoscaler no Kubernetes)
- CDN para assets frontend para distribuir carga globalmente

#### 1.3.3 Resiliência
- O sistema deve lidar graciosamente com falhas em serviços externos (exchanges, bancos de dados, mensageria)
- Implementar retentativas com backoff exponencial usando Polly para chamadas a serviços externos
- Implementar circuit breakers para evitar chamadas repetidas a serviços que estão falhando
- Implementar fallbacks para fornecer funcionalidade degradada em caso de falhas
- Todos os workers devem ser idempotentes, ou seja, processar a mesma mensagem/evento múltiplas vezes sem causar efeitos colaterais indesejados
- Implementar graceful shutdown para que serviços podem ser parados sem perda de dados ou inconsistências
- Backup regular de dados críticos (PostgreSQL, Kafka, Redis)
- Disaster Recovery (DR) com RTO (Recovery Time Objective) e RPO (Recovery Point Objective) definidos e documentados

#### 1.3.4 Segurança
- Segurança é a prioridade número um, especialmente em uma plataforma de trading de criptomoedas, onde qualquer vulnerabilidade pode resultar em perdas financeiras significativas para os usuários
- Princípio do mínimo privilégio (Least Privilege) em todos os níveis (acesso a banco de dados, APIs, serviços, etc.)
- Autenticação forte para todos os usuários (JWT com chaves assimétricas, 2FA/MFA obrigatória para acesso a funcionalidades sensíveis como live trading e gerenciamento de chaves de API)
- Autorização granular para cada recurso e operação (role-based, policy-based, resource-based authorization)
- Criptografia de dados sensíveis em repouso (AES-256 para dados em banco de dados)
- Criptografia de dados em trânsito (TLS 1.2+ para todas as comunicações, mTLS para comunicação entre serviços)
- Gerenciamento seguro de segredos (HashiCorp Vault, AWS Secrets Manager, Azure Key Vault) — NUNCA commit segredos em código-fonte ou arquivos de configuração
- Validação rigorosa de todas as entradas de usuário para prevenir injeções (SQL Injection, XSS, NoSQL Injection, Command Injection)
- Cabeçalhos de segurança em todas as respostas HTTP (HSTS, X-Content-Type-Options, X-Frame-Options, Content-Security-Policy, X-XSS-Protection, Referrer-Policy)
- Rate limiting em todas as APIs públicas e privadas para prevenir abusos e ataques DDoS
- WAF (Web Application Firewall) para proteger a API e o frontend
- Segurança de containers e imagens Docker (usar imagens base oficiais e minimizadas, scan de vulnerabilidades em imagens com Trivy ou Clair)
- Compliance com regulamentações aplicáveis (se houver)
- Auditoria completa de todas as operações sensíveis (login, logout, criação de estratégias, execução de ordens, alteração de configurações, etc.)
- Nenhum dado sensível (senhas, chaves de API, cartões de crédito, dados pessoais) deve ser logado em logs
- Rotatividade regular de chaves e segredos (chaves de API, chaves de criptografia, certificados TLS)

#### 1.3.5 Observabilidade
- Todo componente da plataforma deve ser 100% observável por logs, métricas e tracing
- Logs estruturados em JSON, contendo pelo menos timestamp, service name, trace ID, span ID, user ID, correlation ID, level, message e outros metadados relevantes
- Logs em níveis apropriados (Debug, Information, Warning, Error, Fatal)
- Tracing distribuído usando OpenTelemetry e W3C Trace Context para propagação de contexto entre serviços
- Métricas obrigatórias para todos os serviços (taxa de erro, latência, taxa de sucesso, uso de CPU, memória, disco, rede, etc.)
- Alertas configurados em métricas críticas (taxa de erro > 5%, latência > 1s, uso de memória > 80%, etc.)
- Dashboards em Grafana para monitoramento em tempo real
- Logs centralizados em Elasticsearch ou Grafana Loki
- Tracing distribuído em Jaeger ou Grafana Tempo

#### 1.3.6 Simplicidade Complexa
- A arquitetura da plataforma é complexa por natureza (monorepo, microsserviços, event-driven, etc.)
- No entanto, cada componente individual deve ser simples, focado em uma única responsabilidade (Single Responsibility Principle)
- Código deve ser legível, manutenível e bem documentado
- Evitar over-engineering — não adicionar complexidade desnecessária
- KISS (Keep It Simple, Stupid) e YAGNI (You Aren’t Gonna Need It) são princípios importantes
- Code smells devem ser evitados e refatorados regularmente

#### 1.3.7 Domain-Driven Design (DDD)
- Todo o design do software é orientado ao domínio do negócio (trading quantitativo de criptomoedas)
- Linguagem Ubíqua (Ubiquitous Language) é usada por todos os membros da equipe (desenvolvedores, designers, product managers, stakeholders)
- Domínios e subdomínios são claramente definidos e mapeados
- Agregados, entidades, value objects, repositórios, serviços de domínio e eventos de domínio são usados adequadamente
- Bounded Contexts são claramente definidos e comunicam-se entre si via APIs ou eventos
- Context Maps são documentados para mostrar os relacionamentos entre Bounded Contexts

#### 1.3.8 Clean Architecture
- Clean Architecture é seguida rigorosamente, com dependências unidirecionais para dentro
- Camadas são: Domain → Application → Infrastructure → Presentation
- As camadas internas não dependem de camadas externas
- Interfaces são definidas nas camadas internas e implementadas nas camadas externas (Infrastructure e Presentation)
- Injeção de Dependência (DI) é usada para resolver dependências
- Isso garante testabilidade, manutenibilidade e flexibilidade para mudanças de tecnologia

#### 1.3.9 SOLID
- Os 5 princípios SOLID são seguidos rigorosamente em todo o código:
  1. Single Responsibility Principle (SRP)
  2. Open/Closed Principle (OCP)
  3. Liskov Substitution Principle (LSP)
  4. Interface Segregation Principle (ISP)
  5. Dependency Inversion Principle (DIP)

#### 1.3.10 Event-Driven Architecture (EDA)
- A plataforma é altamente orientada a eventos para permitir desacoplamento entre serviços, escalabilidade e resiliência
- Eventos de domínio e eventos de integração são usados para comunicação entre componentes
- Apache Kafka é usado como broker de eventos
- Outbox Pattern é usado para garantir entrega confiável de eventos
- Consumers são idempotentes e idempotent keys são usados quando necessário
- Schema Registry (Confluent Schema Registry) é usado para validação de schemas de eventos (Avro ou Protobuf)

### 1.4. Roadmap de Versões
#### Versão 1.0 (MVP - Minimum Viable Product)
- Autenticação e autorização básicas (JWT, roles básicas)
- Integração com Binance Spot (dados de mercado e ordens)
- Criação e gerenciamento de estratégias básicas
- Backtesting básico com dados históricos de Binance
- Paper trading com dados em tempo real
- Dashboard básico de performance
- Alertas básicos por e-mail
- Relatórios básicos de performance

#### Versão 1.5
- Integração com mais exchanges (Coinbase Pro, Kraken)
- Integração com mais fontes de dados históricas
- Indicadores técnicos mais avançados
- Otimização de estratégias (grid search, bayesian optimization)
- Walk-forward analysis (WFA)
- API pública básica
- Aplicativo móvel básico (iOS e Android)
- Alertas por SMS e Push Notifications
- Melhorias no dashboard

#### Versão 2.0
- Machine Learning para previsão de preços e otimização de estratégias
- Gerenciamento de risco mais avançado
- Análise de dados on-chain
- Integração com provedores de identidade externos (Google, GitHub, Azure AD)
- Multi-tenancy completo
- Compliance e auditoria avançadas
- Melhorias de desempenho e escalabilidade
- Disaster Recovery e alta disponibilidade (HA - High Availability)
- Service Mesh (Istio/Linkerd)
- Melhorias na observabilidade e monitoramento
- Melhorias no CI/CD pipeline
- Melhorias na segurança
- E muitas outras funcionalidades

---

## 2. Arquitetura do Sistema

### 2.1. Visão Geral da Arquitetura
A Crypto AI Platform segue uma arquitetura de microsserviços distribuídos, organizada em um monorepo para facilitar o gerenciamento de dependências, compartilhamento de código e coordenação entre equipes. A arquitetura é dividida em várias camadas e componentes, cada um com responsabilidades claras e separadas.

Os principais componentes e camadas da plataforma são:

#### 2.1.1 Camada de Cliente
- **Frontend Web**: Aplicação Next.js com App Router para a interface web do usuário
- **Aplicativo Móvel**: Aplicativos nativos iOS e Android (ou React Native) para acesso em dispositivos móveis
- **API Clients**: Bibliotecas/clientes SDK em várias linguagens (C#, Python, JavaScript/TypeScript, Go) para integração com a API pública da plataforma

#### 2.1.2 Camada de Gateway
- **API Gateway**: Ponto de entrada único para todas as requisições externas (frontend, aplicativo móvel, clientes API). Responsabilidades:
  - Roteamento de requisições para serviços apropriados
  - Autenticação e autorização inicial
  - Rate limiting
  - Caching
  - Compressão de dados
  - SSL/TLS termination
  - Transformação de requisições/respostas
  - Ferramenta recomendada: YARP (Yet Another Reverse Proxy - da Microsoft) para .NET, ou Kong, ou Nginx Plus

#### 2.1.3 Camada de Serviços Aplicacionais
- **API Principal (Identity & Access + Core)**:
  - Autenticação e autorização
  - Gerenciamento de usuários, roles e permissões
  - Gerenciamento de perfis de usuário
  - Gerenciamento de estratégias de trading
  - Gerenciamento de backtests
  - Gerenciamento de paper trading
  - Gerenciamento de relatórios e dashboards
  - API REST para frontend e clientes externos
  - Tecnologia: ASP.NET Core Web API 9.0

- **API de Market Data**:
  - Coleta e distribuição de dados de mercado em tempo real (preços, volume, order books)
  - Cache de dados de mercado em Redis
  - Armazenamento de dados históricos em PostgreSQL com TimescaleDB
  - API REST e WebSocket para dados de mercado
  - Tecnologia: ASP.NET Core Web API 9.0 + WebSockets

- **API de Trading Execution**:
  - Integração com APIs de exchanges para execução de ordens
  - Gerenciamento de ordens (criação, cancelamento, consulta)
  - Gerenciamento de contas de exchange (chaves de API)
  - Tecnologia: ASP.NET Core Web API 9.0

- **API de Risk Management**:
  - Implementação de regras de gerenciamento de risco
  - Monitoramento de exposição, drawdown, etc.
  - Ações de mitigação de risco automáticas (fechamento de posições, etc.)
  - Tecnologia: ASP.NET Core Web API 9.0

- **API de Analytics**:
  - Cálculo de métricas de performance (Sharpe Ratio, Sortino Ratio, Maximum Drawdown, etc.)
  - Geração de relatórios detalhados de performance
  - Análise de dados históricos
  - Tecnologia: ASP.NET Core Web API 9.0

- **Workers (Background Services)**:
  - **Market Data Worker**: Coleta dados de mercado em tempo real das exchanges via WebSocket e REST
  - **Backtesting Worker**: Executa backtests de estratégias em dados históricos
  - **Paper Trading Worker**: Simula execução de estratégias em ambiente de paper trading
  - **Live Trading Worker**: Executa estratégias em ambiente de live trading, integra com API de Trading Execution
  - **Risk Worker**: Monitora risco em tempo real, executa ações de mitigação
  - **Outbox Worker**: Processa outbox messages e publica eventos no Kafka
  - **Audit Worker**: Processa eventos de auditoria e armazena no banco de dados
  - **Notification Worker**: Envia notificações por e-mail, SMS, Push Notifications
  - Tecnologia: .NET Worker Service 9.0

#### 2.1.4 Camada de Mensageria e Eventos
- **Event Broker**: Apache Kafka 3.7+ com KRaft (sem ZooKeeper) para streaming de eventos
- **Schema Registry**: Confluent Schema Registry para validação de schemas de eventos (Avro ou Protobuf)
- **Tipos de Eventos**:
  - Eventos de Domínio (Domain Events): Representam algo significativo que aconteceu no domínio (ex: StrategyCreated, OrderExecuted, RiskLimitReached)
  - Eventos de Integração (Integration Events): Representam algo que aconteceu em outro serviço e que este serviço precisa saber (ex: UserCreatedIntegrationEvent, StrategyBacktestedIntegrationEvent)

#### 2.1.5 Camada de Persistência e Cache
- **PostgreSQL 16+**: Banco de dados relacional principal para armazenamento de dados estruturados (usuários, estratégias, ordens, backtests, auditoria, etc.)
- **TimescaleDB (extensão para PostgreSQL)**: Para otimização de armazenamento e consulta de dados de séries temporais (market data históricos)
- **Redis 7.2+ com Redis Stack**:
  - Cache de dados frequentes (dados de mercado, sessões de usuário)
  - Rate limiting
  - Distributed locks para prevenção de condições de corrida
  - Pub/Sub para comunicação em tempo real simples
  - Gerenciamento de sessões de usuário
- **MinIO ou AWS S3**: Armazenamento de objetos para dados históricos grandes, backups, logs, arquivos de relatórios, etc.

#### 2.1.6 Camada de Observabilidade
- **OpenTelemetry 1.8+**: Instrumentação unified para logs, métricas e tracing
- **Serilog 3.0+**: Logging estruturado
- **Grafana Loki 3.0+**: Agregação e consulta de logs
- **Prometheus 2.52+**: Coleta de métricas
- **Grafana 11.x**: Visualização de métricas e logs em dashboards interativos
- **Jaeger 1.58+ ou Grafana Tempo 2.5+**: Armazenamento e visualização de tracing distribuído

#### 2.1.7 Camada de Infraestrutura
- **Docker 25+**: Containerização de todos os serviços
- **Kubernetes 1.29+**: Orquestração de containers em produção
- **Helm 3.14+**: Gerenciamento de charts do Kubernetes
- **Terraform 1.7+**: Infraestrutura como Código (IaC) para provisionamento de recursos na nuvem
- **Istio 1.21+ ou Linkerd 2.14+**: Service Mesh para gerenciamento de tráfego, mTLS, observabilidade entre serviços
- **HashiCorp Vault 1.15+ ou AWS Secrets Manager ou Azure Key Vault**: Gerenciamento seguro de segredos
- **Cloudflare ou AWS CloudFront**: CDN para distribuição de assets frontend e proteção contra DDoS
- **WAF (Web Application Firewall)**: Proteção contra ataques web (SQLi, XSS, etc.)

### 2.2. C4 Model Architecture
O C4 Model é uma abordagem para representar a arquitetura de software em diferentes níveis de abstração, desde uma visão geral até detalhes de implementação. Abaixo estão os 4 níveis para a Crypto AI Platform.

#### 2.2.1 Nível 1: Contexto (System Context Diagram)
Este diagrama mostra a Crypto AI Platform como um sistema único, interagindo com usuários e sistemas externos.
- **Usuários (Actors)**:
  - Traders (usuários que usam a plataforma para trading)
  - Administradores (usuários que gerenciam a plataforma)
  - Pesquisadores Quantitativos (usuários que desenvolvem e backtestam estratégias)
  - Sistemas Externos:
    - Exchanges de Criptomoedas (Binance, Coinbase Pro, Kraken, KuCoin, etc.)
    - Provedores de Dados Históricos (se aplicável)
    - Provedores de E-mail (SendGrid, Mailgun, etc.)
    - Provedores de SMS (Twilio, etc.)
    - Provedores de Notificações Push (Firebase Cloud Messaging, Apple Push Notification Service)
- **Interações**:
  - Usuários interagem com a Crypto AI Platform via frontend web, aplicativo móvel ou clientes API
  - A Crypto AI Platform se integra com exchanges para obter dados de mercado e executar ordens
  - A Crypto AI Platform se integra com provedores de comunicação para enviar notificações
  - A Crypto AI Platform pode se integrar com provedores de dados históricos para complementar seus próprios dados

#### 2.2.2 Nível 2: Container (Container Diagram)
Este diagrama divide a Crypto AI Platform em containers (componentes implantáveis separados).
Containers principais:
1. **Frontend Web**: Container com Next.js 14+ app
2. **API Gateway**: Container com YARP/Kong/Nginx
3. **API Principal**: Container com ASP.NET Core Web API 9.0
4. **API Market Data**: Container com ASP.NET Core Web API 9.0
5. **API Trading Execution**: Container com ASP.NET Core Web API 9.0
6. **API Risk Management**: Container com ASP.NET Core Web API 9.0
7. **API Analytics**: Container com ASP.NET Core Web API 9.0
8. **Market Data Worker**: Container com .NET Worker Service 9.0
9. **Backtesting Worker**: Container com .NET Worker Service 9.0
10. **Paper Trading Worker**: Container com .NET Worker Service 9.0
11. **Live Trading Worker**: Container com .NET Worker Service 9.0
12. **Risk Worker**: Container com .NET Worker Service 9.0
13. **Outbox Worker**: Container com .NET Worker Service 9.0
14. **Audit Worker**: Container com .NET Worker Service 9.0
15. **Notification Worker**: Container com .NET Worker Service 9.0
16. **Apache Kafka**: Cluster de brokers Kafka
17. **Confluent Schema Registry**: Container com Schema Registry
18. **PostgreSQL**: Cluster de bancos de dados PostgreSQL
19. **Redis**: Cluster de Redis
20. **MinIO/AWS S3**: Storage de objetos
21. **Prometheus**: Container com Prometheus
22. **Grafana**: Container com Grafana
23. **Grafana Loki**: Container com Grafana Loki
24. **Jaeger/Grafana Tempo**: Container com tracing
25. **HashiCorp Vault**: Container com Vault (ou serviço gerenciado)
- **Interações entre Containers**:
  - Frontend Web se comunica com API Gateway via HTTPS
  - API Gateway roteia requisições para APIs apropriadas (API Principal, API Market Data, API Trading Execution, API Risk Management, API Analytics)
  - Todas as APIs e Workers se comunicam com Kafka para produzir e consumir eventos
  - APIs e Workers se comunicam com PostgreSQL e Redis para dados e cache
  - Workers se comunicam com S3/MinIO para armazenamento de arquivos grandes
  - APIs e Workers se comunicam com Vault para obter segredos
  - Todos os containers enviam logs, métricas e tracing para Prometheus, Grafana, Loki e Tempo

#### 2.2.3 Nível 3: Component (Component Diagram)
Este diagrama aprofunda em um container específico e mostra seus componentes internos. Por exemplo, o Container API Principal pode ser dividido em:
- Presentation Layer (Controllers API)
- Application Layer (Commands, Queries, Command Handlers, Query Handlers, DTOs, Validators)
- Domain Layer (Entities, Value Objects, Aggregates, Domain Events, Domain Services, Repository Interfaces)
- Infrastructure Layer (Repository Implementations, External Service Integrations, Database Contexts, etc.)
Os detalhes do Nível 3 são documentados em um documento separado: `docs/architecture/c4-component-diagram.md`

#### 2.2.4 Nível 4: Código (Code Diagram)
Este diagrama mostra detalhes de implementação de um componente específico (como uma classe ou função), usando diagramas UML de classe, diagramas de sequência, etc. Os detalhes do Nível 4 são documentados em arquivos específicos para cada componente ou serviço.

### 2.3. Monorepo Structure
A Crypto AI Platform usa um monorepo para gerenciar todo o código da plataforma em um único repositório Git. Isso facilita o compartilhamento de código, gerenciamento de dependências, refatorações cross-service e coordenação entre equipes. A estrutura do monorepo é a seguinte (a partir da raiz do repositório):
```
crypto-ai-platform/
├── .github/                # Arquivos do GitHub (workflows, templates de issue/PR)
├── .trae/                  # Arquivos de configuração do Trae AI
├── agents/                 # Arquivos de prompt para agentes AI
├── apps/                   # Aplicativos e serviços que podem ser implantados separadamente
│   ├── api-gateway/        # API Gateway (YARP/Kong/Nginx)
│   ├── api-core/           # API Principal
│   ├── api-market-data/    # API de Market Data
│   ├── api-trading/        # API de Trading Execution
│   ├── api-risk/           # API de Risk Management
│   ├── api-analytics/      # API de Analytics
│   ├── worker-market-data/ # Market Data Worker
│   ├── worker-backtest/    # Backtesting Worker
│   ├── worker-paper-trading/# Paper Trading Worker
│   ├── worker-live-trading/# Live Trading Worker
│   ├── worker-risk/        # Risk Worker
│   ├── worker-outbox/      # Outbox Worker
│   ├── worker-audit/       # Audit Worker
│   ├── worker-notification/# Notification Worker
│   └── web/                # Frontend Next.js
├── packages/               # Pacotes compartilhados reutilizáveis
│   ├── domain/             # Camada de Domínio (Clean Architecture)
│   ├── application/        # Camada de Aplicação (Clean Architecture)
│   ├── infrastructure/     # Camada de Infraestrutura (Clean Architecture)
│   ├── presentation/       # Camada de Apresentação (Clean Architecture)
│   ├── shared/             # Utilitários, extensões, helpers compartilhados
│   ├── contracts/          # Interfaces, DTOs, schemas de eventos compartilhados
│   ├── sdk/                # SDKs/clientes para integração com a API pública
│   └── ui/                 # Componentes de UI compartilhados para frontend (shadcn/ui components)
├── scripts/                # Scripts úteis (build, deploy, migration, etc.)
├── docker/                 # Dockerfiles, docker-compose.yml
├── infra/                  # Arquivos de IaC (Terraform, Kubernetes Helm charts)
├── docs/                   # Documentação do projeto
├── tests/                  # Testes
│   ├── unit/               # Testes unitários
│   ├── integration/        # Testes de integração
│   ├── e2e/                # Testes end-to-end
│   └── performance/        # Testes de performance
├── checklists/             # Checklists para desenvolvimento, deploy, revisão de código, etc.
├── examples/               # Exemplos de uso da plataforma
├── templates/              # Templates para arquivos novos (componentes, APIs, etc.)
├── knowledge/              # Base de conhecimento (documentos de aprendizado, notas, etc.)
├── playbooks/              # Playbooks de incidentes, runbooks para operações
├── tasks/                  # Lista de tarefas a serem implementadas
├── .editorconfig           # Configurações de editor
├── .gitignore              # Arquivos e pastas ignorados pelo Git
├── README.md               # README principal do projeto
├── CONTRIBUTING.md         # Guia de contribuição
├── CHANGELOG.md            # Changelog do projeto
├── LICENSE                 # Licença do projeto
├── package.json            # Arquivo de configuração do pnpm (monorepo Node.js)
├── pnpm-workspace.yaml     # Configuração do pnpm workspaces
├── CryptoAIPlatform.sln    # Arquivo de solução do .NET (monorepo .NET)
└── docker-compose.yml      # Arquivo Docker Compose para desenvolvimento local
```
Mais detalhes sobre a estrutura do monorepo estão em `docs/architecture/monorepo-structure.md`

### 2.4. Clean Architecture Implementation Details
Como mencionado anteriormente, a plataforma segue rigorosamente a Clean Architecture, que é implementada nos pacotes `packages/domain`, `packages/application`, `packages/infrastructure` e `packages/presentation`, e usadas por todos os apps em `apps/`. Mais detalhes sobre a implementação da Clean Architecture na Crypto AI Platform estão em `docs/architecture/clean-architecture.md`

---

## 3. Domain-Driven Design (DDD)

Domain-Driven Design é uma abordagem para o desenvolvimento de software complexos, onde a modelagem do domínio do negócio é o foco principal. A Crypto AI Platform usa DDD extensivamente para garantir que o código represente fielmente o negócio de trading quantitativo de criptomoedas, e que a lógica de negócio seja isolada, testável e fácil de manter.

### 3.1. Domínios, Subdomínios e Bounded Contexts
Um dos primeiros passos em DDD é dividir o domínio do negócio em domínios e subdomínios, e definir os Bounded Contexts (contextos delimitados).

#### 3.1.1 Domínios Principais (Core Domain)
O Core Domain é o coração do negócio, o que diferencia a Crypto AI Platform de outras plataformas.
- **Trading Engine**: Gerenciamento de estratégias de trading, backtesting, paper trading e live trading.
- **Risk Management**: Gerenciamento de risco automatizado.
- **Quantitative Research**: Ferramentas para pesquisa quantitativa, análise de dados e desenvolvimento de estratégias.

#### 3.1.2 Subdomínios de Apoio (Supporting Subdomains)
Subdomínios de apoio suportam o Core Domain, mas não são a principal diferenciadora da plataforma.
- **Identity & Access**: Autenticação, autorização, gerenciamento de usuários, roles e permissões.
- **Audit & Compliance**: Registro de auditoria de todas as operações, compliance com regulamentações.
- **Notifications**: Envio de notificações por e-mail, SMS, Push Notifications.
- **Analytics & Reporting**: Cálculo de métricas de performance, geração de relatórios, dashboards.

#### 3.1.3 Subdomínios Genéricos (Generic Subdomains)
Subdomínios genéricos são necessários para o funcionamento da plataforma, mas são bem compreendidos e podem ser substituídos por soluções prontas (ou até SaaS) se necessário.
- **Market Data**: Coleta, armazenamento e distribuição de dados de mercado.
- **Infrastructure & DevOps**: Infraestrutura, CI/CD, observabilidade, etc.
- **User Interface**: Interface do usuário (frontend).

#### 3.1.4 Bounded Contexts
Cada subdomínio é mapeado para um ou mais Bounded Contexts. Um Bounded Context é uma fronteira explícita onde um modelo de domínio específico se aplica, com sua própria Linguagem Ubíqua.
Para a Crypto AI Platform, os Bounded Contexts principais são:
1. **Identity & Access Bounded Context**:
   - Responsabilidades: Autenticação, autorização, usuários, roles, permissões, tokens.
   - Tecnologias: ASP.NET Core Identity, JWT.
   - Localização no monorepo: `apps/api-core/` (parte da API Principal) + `packages/domain/` (Domain Model), `packages/application/` (Application Layer), `packages/infrastructure/` (Infrastructure Layer).
2. **Market Data Bounded Context**:
   - Responsabilidades: Coleta de dados de mercado, cache, armazenamento histórico, distribuição de dados em tempo real.
   - Tecnologias: PostgreSQL + TimescaleDB, Redis, WebSockets.
   - Localização no monorepo: `apps/api-market-data/`, `apps/worker-market-data/` + `packages/domain/`, `packages/application/`, `packages/infrastructure/`.
3. **Trading Engine Bounded Context**:
   - Responsabilidades: Criação de estratégias, backtesting, paper trading, live trading, execução de ordens.
   - Tecnologias: ASP.NET Core Web API, .NET Worker Services, Kafka.
   - Localização no monorepo: `apps/api-core/`, `apps/api-trading/`, `apps/worker-backtest/`, `apps/worker-paper-trading/`, `apps/worker-live-trading/` + `packages/domain/`, `packages/application/`, `packages/infrastructure/`.
4. **Risk Management Bounded Context**:
   - Responsabilidades: Definição de regras de risco, monitoramento de risco em tempo real, ações de mitigação de risco.
   - Localização no monorepo: `apps/api-risk/`, `apps/worker-risk/` + `packages/domain/`, `packages/application/`, `packages/infrastructure/`.
5. **Analytics & Reporting Bounded Context**:
   - Responsabilidades: Cálculo de métricas de performance, geração de relatórios, dashboards.
   - Localização no monorepo: `apps/api-analytics/` + `packages/domain/`, `packages/application/`, `packages/infrastructure/`.
6. **Audit & Compliance Bounded Context**:
   - Responsabilidades: Registro de auditoria de todas as operações, compliance.
   - Localização no monorepo: `apps/worker-audit/` + `packages/domain/`, `packages/application/`, `packages/infrastructure/`.
7. **Notifications Bounded Context**:
   - Responsabilidades: Envio de notificações por vários canais.
   - Localização no monorepo: `apps/worker-notification/` + `packages/domain/`, `packages/application/`, `packages/infrastructure/`.

#### 3.1.5 Context Maps
Um Context Map é um documento que descreve os relacionamentos entre Bounded Contexts. Para a Crypto AI Platform, o Context Map está em `docs/architecture/context-map.md`

### 3.2. Linguagem Ubíqua (Ubiquitous Language)
Um dos conceitos mais importantes de DDD é a Linguagem Ubíqua: uma linguagem comum, compartilhada por todos os membros da equipe (desenvolvedores, designers, product managers, stakeholders, especialistas do domínio), que descreve o domínio do negócio usando termos do negócio, sem jargões técnicos sempre que possível.

A Linguagem Ubíqua deve ser usada em todos os locais: código, documentação, conversas, tarefas, etc.

Exemplos de termos da Linguagem Ubíqua para a Crypto AI Platform:
- **Agente AI**: Assistente AI que ajuda a criar estratégias de trading.
- **Ativo**: Uma criptomoeda (ex: BTC, ETH, SOL).
- **Atualização de preço**: Evento que indica que o preço de um ativo mudou.
- **Backtest**: Execução de uma estratégia em dados históricos para avaliar sua performance.
- **Book de ordens (Order Book)**: Lista de ordens de compra e venda de um ativo em uma exchange.
- **Drawdown**: Redução percentual do valor do portfólio desde o pico máximo.
- **Exposição**: Quantidade total de capital investido em um ativo ou estratégia.
- **Fator de lucro (Profit Factor)**: Razão entre lucros brutos e perdas brutas.
- **Indivíduo (Tick)**: Uma atualização individual de preço ou volume.
- **Ordem**: Instrução para comprar ou vender um ativo.
  - Ordem de mercado (Market Order): Ordem que é executada imediatamente ao preço atual de mercado.
  - Ordem de limite (Limit Order): Ordem que é executada apenas se o preço do ativo atingir ou ficar melhor que o preço limite especificado.
  - Ordem de stop loss (Stop Loss Order): Ordem que se torna uma ordem de mercado quando o preço do ativo atinge o preço de stop.
  - Ordem de take profit (Take Profit Order): Ordem que se torna uma ordem de mercado quando o preço do ativo atinge o preço de take profit.
- **Papel trading (Paper Trading)**: Simulação de trading usando dinheiro virtual, para testar estratégias sem risco financeiro.
- **Performance**: Medição de quão bem uma estratégia está ou esteve performando (usando métricas como Sharpe Ratio, Sortino Ratio, Drawdown, etc.).
- **Portfólio**: Conjunto de ativos e posições de um usuário.
- **Posição**: Quantidade de um ativo que um usuário ou estratégia detém.
  - Posição comprada (Long Position): Detentor do ativo, esperando que o preço suba.
  - Posição vendida (Short Position): Vendedor do ativo que não detém, esperando que o preço caia para recomprar mais barato.
- **Ratio de Sharpe**: Mede o retorno ajustado ao risco de uma estratégia, comparando o retorno excessivo com a volatilidade (desvio padrão).
- **Ratio de Sortino**: Semelhante ao Sharpe Ratio, mas usa apenas a volatilidade negativa (desvio padrão de perdas), ao invés de toda a volatilidade.
- **Risco**: Probabilidade de perda financeira.
- **Slippage**: Diferença entre o preço esperado de uma ordem e o preço real de execução.
- **Stop loss**: Limite de perda máximo aceitável para uma posição ou estratégia.
- **Estratégia**: Conjunto de regras para tomar decisões de trading (comprar, vender, manter posições).
- **Take profit**: Preço alvo para fechar uma posição com lucro.
- **Trading ao vivo (Live Trading)**: Trading com dinheiro real em uma exchange.
- **Walk-Forward Analysis (WFA)**: Método para validar estratégias, onde a estratégia é otimizada em um período de dados históricos e depois testada em um período seguinte, repetindo o processo.

A Linguagem Ubíqua completa da Crypto AI Platform está em `docs/domain/ubiquitous-language.md`

### 3.3. Blocos de Construção Táticos (Tactical Design)
Uma vez que os Bounded Contexts e a Linguagem Ubíqua estão definidos, é hora de modelar o domínio usando os blocos de construção táticos de DDD: Entidades, Value Objects, Agregados, Serviços de Domínio, Eventos de Domínio, Repositórios e Factories.

#### 3.3.1 Entidades (Entities)
Uma Entidade é um objeto do domínio que é definido por sua identidade única, não por seus atributos. Duas entidades são iguais se tiverem o mesmo ID, mesmo que todos os outros atributos sejam diferentes.
- **Características**:
  - Possuem uma identidade única (Id).
  - São mutáveis (seus atributos podem mudar ao longo do tempo).
  - Possuem vida útil contínua (persistem ao longo do tempo).
- **Exemplos de Entidades na Crypto AI Platform**:
  - `User`: Representa um usuário da plataforma (identificado por `UserId`).
  - `Strategy`: Representa uma estratégia de trading (identificado por `StrategyId`).
  - `Order`: Representa uma ordem de trading (identificado por `OrderId`).
  - `Backtest`: Representa um backtest de uma estratégia (identificado por `BacktestId`).
  - `Position`: Representa uma posição de trading (identificado por `PositionId`).
- **Implementação no Código**:
  - Todas as entidades herdam de uma classe base `Entity` no pacote `packages/domain/`.
  - A classe base `Entity` tem uma propriedade `Id` do tipo `Guid` (ou `string`, ou outro tipo apropriado).
  - A igualdade de entidades é determinada pelo `Id`.

#### 3.3.2 Objetos de Valor (Value Objects)
Um Value Object é um objeto do domínio que é definido por seus atributos, não por uma identidade. Dois Value Objects são iguais se todos os seus atributos forem iguais.
- **Características**:
  - Não possuem identidade (sem `Id`).
  - São imutáveis (seus atributos não podem ser alterados após a criação; para modificar, você cria um novo objeto).
  - Representam uma "característica" ou "descrição" de algo no domínio.
  - Podem conter lógica de negócio simples (validação, cálculos, etc.).
- **Exemplos de Value Objects na Crypto AI Platform**:
  - `Price`: Representa um preço (atributos: `Amount` (decimal), `Currency` (string)).
  - `Quantity`: Representa uma quantidade de ativo (atributos: `Amount` (decimal), `Asset` (string)).
  - `RiskParameters`: Representa parâmetros de risco para uma estratégia (atributos: `MaxDrawdownPercent` (decimal), `MaxPositionSizePercent` (decimal), `MaxTotalExposurePercent` (decimal)).
  - `BacktestMetrics`: Representa métricas de performance de um backtest (atributos: `TotalReturnPercent` (decimal?), `SharpeRatio` (decimal?), `SortinoRatio` (decimal?), `MaxDrawdownPercent` (decimal?), `WinRatePercent` (decimal?), `ProfitFactor` (decimal?), etc.).
- **Implementação no Código**:
  - Todas as Value Objects herdam de uma classe base `ValueObject` no pacote `packages/domain/`.
  - A igualdade de Value Objects é determinada por todos os seus atributos.
  - São implementados como `record` em C# 12+ (que são imutáveis por padrão e fornecem comparação de igualdade por valores).

#### 3.3.3 Agregados (Aggregates)
Um Agregado é um cluster de objetos relacionados (Entidades e Value Objects) que são tratados como uma única unidade para operações de dados e consistência.
- **Componentes de um Agregado**:
  - **Agregado Raiz (Aggregate Root)**: Uma única entidade que é o ponto de entrada único para o agregado. Todo acesso a objetos dentro do agregado deve ser feito através do Aggregate Root.
  - **Entidades e Value Objects internas**: Outros objetos dentro do agregado, que só podem ser acessados através do Aggregate Root.
- **Características**:
  - Consistência transacional: As invariantes do agregado devem ser garantidas em todas as operações (uma transação no banco de dados modifica apenas um agregado por vez).
  - Agregados são persistidos como um todo: Os Repositórios persistem e recuperam Aggregates completos, não entidades individuais.
  - Relacionamentos entre agregados são feitos por referência a ID, não por referência a objeto (para evitar cargas excessivas de dados e garantir consistência).
- **Invariantes**: Uma invariante é uma condição que deve sempre ser verdadeira para o agregado. O Aggregate Root é responsável por garantir que todas as invariantes sejam mantidas a todo momento.
- **Exemplos de Agregados na Crypto AI Platform**:
  - **Strategy Aggregate**:
    - Aggregate Root: `Strategy` (Entidade).
    - Entidades internas: `StrategyVersion` (representa versões de uma estratégia).
    - Value Objects internas: `RiskParameters`, `StrategyConfiguration`, etc.
    - Invariantes:
      - Uma estratégia deve ter um nome não vazio.
      - Uma estratégia deve pertencer a um usuário (UserId não nulo).
      - Os RiskParameters de uma estratégia devem ser válidos (MaxDrawdownPercent entre 0 e 100, etc.).
  - **Order Aggregate**:
    - Aggregate Root: `Order` (Entidade).
    - Value Objects internas: `Price`, `Quantity`, `OrderSide`, `OrderType`, `OrderStatus`, etc.
    - Invariantes:
      - Uma ordem deve ter um lado válido (Buy ou Sell).
      - Uma ordem deve ter um tipo válido (Market, Limit, StopLoss, TakeProfit).
      - Uma ordem de limite deve ter um preço limite válido.
      - Uma ordem de stop loss ou take profit deve ter um preço de stop/take válido.
  - **User Aggregate**:
    - Aggregate Root: `User` (Entidade).
    - Entidades internas: `UserProfile`, `UserExchangeAccount`.
    - Value Objects internas: `Email`, `PasswordHash`, `ApiKey`, etc.
    - Invariantes:
      - Um usuário deve ter um e-mail válido e único.
      - Um usuário deve ter um password hash válido.

#### 3.3.4 Repositórios (Repositories)
Um Repositório é um objeto que encapsula a lógica necessária para acessar a fonte de dados (banco de dados, arquivo, API externa, etc.) e persistir/recuperar Agregados. Ele abstrai a camada de persistência, de forma que as camadas internas (Domain e Application) não precisem saber detalhes de como os dados são armazenados.
- **Características**:
  - A interface do Repositório é definida na Camada de Domínio (ex: `IStrategyRepository` em `packages/domain/`).
  - A implementação do Repositório é definida na Camada de Infraestrutura (ex: `StrategyRepository` em `packages/infrastructure/`).
  - Cada Agregado tem seu próprio Repositório.
  - Métodos típicos de um Repositório:
    - `Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken)`: Adiciona um novo Aggregate Root ao repositório.
    - `Task UpdateAsync(TAggregate aggregate, CancellationToken cancellationToken)`: Atualiza um Aggregate Root existente no repositório.
    - `Task DeleteAsync(TAggregateId id, CancellationToken cancellationToken)`: Remove um Aggregate Root do repositório pelo ID.
    - `Task<TAggregate?> GetByIdAsync(TAggregateId id, CancellationToken cancellationToken)`: Recupera um Aggregate Root do repositório pelo ID, ou null se não existir.
    - `Task<IEnumerable<TAggregate>> ListAsync(/* parâmetros de paginação, filtro, ordenação */, CancellationToken cancellationToken)`: Recupera uma lista paginada de Aggregates.
- **Exemplo de Interface de Repositório**:
  ```csharp
  // em packages/domain/Repositories/IStrategyRepository.cs
  namespace CryptoAIPlatform.Domain.Repositories;

  public interface IStrategyRepository
  {
      Task AddAsync(Strategy strategy, CancellationToken cancellationToken);
      Task UpdateAsync(Strategy strategy, CancellationToken cancellationToken);
      Task DeleteAsync(Guid strategyId, CancellationToken cancellationToken);
      Task<Strategy?> GetByIdAsync(Guid strategyId, CancellationToken cancellationToken);
      Task<IEnumerable<Strategy>> ListByUserIdAsync(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
      // Outros métodos de consulta, se necessário
  }
  ```

#### 3.3.5 Serviços de Domínio (Domain Services)
Um Serviço de Domínio é uma classe stateless que contém lógica de negócio que não pertence naturalmente a nenhuma Entidade ou Value Object, e que opera em múltiplos Agregados ou objetos do domínio.
- **Características**:
  - São stateless (não mantêm estado entre chamadas).
  - Operam em múltiplos Agregados ou Value Objects.
  - Contêm lógica de negócio essencial do domínio.
  - São definidos na Camada de Domínio.
- **Exemplos de Domain Services na Crypto AI Platform**:
  - `StrategyBacktestService`: Contém a lógica de negócio para executar um backtest de uma estratégia (recebe uma estratégia, dados históricos, executa as regras da estratégia, calcula métricas, retorna o resultado do backtest).
  - `RiskCalculationService`: Contém a lógica de negócio para calcular métricas de risco para uma estratégia ou portfólio.
  - `OrderMatchingService`: Contém a lógica de negócio para simular matching de ordens em paper trading (se não estiver usando uma exchange de paper trading).
- **Quando usar um Domain Service**:
  - Quando a lógica não pertence a nenhuma Entidade ou Value Object específico.
  - Quando a lógica precisa acessar múltiplos Agregados.
  - Quando a lógica é puramente lógica de negócio, sem dependências externas (dependências externas devem ser injetadas via interfaces definidas na Camada de Domínio).

#### 3.3.6 Eventos de Domínio (Domain Events)
Um Evento de Domínio é algo significativo que aconteceu no domínio do negócio, que outros componentes do sistema (mesmo Bounded Context ou outros Bounded Contexts) podem precisar saber e reagir.
- **Características**:
  - Nomeados no pretérito (ex: `StrategyCreated`, `OrderExecuted`, `RiskLimitReached`, `BacktestCompleted`).
  - São imutáveis (seus atributos não podem ser alterados após a criação).
  - Contêm apenas os dados necessários para descrever o evento (não carreguem dados desnecessários).
  - Possuem uma data e hora de ocorrência (`OccurredAt`).
  - São definidos na Camada de Domínio.
- **Exemplos de Domain Events na Crypto AI Platform**:
  - `StrategyCreatedEvent`: Ocorre quando uma nova estratégia é criada.
    - Atributos: `StrategyId`, `UserId`, `StrategyName`, `OccurredAt`.
  - `OrderExecutedEvent`: Ocorre quando uma ordem é executada com sucesso.
    - Atributos: `OrderId`, `StrategyId`, `UserId`, `Exchange`, `Symbol`, `OrderSide`, `OrderType`, `Price`, `Quantity`, `OccurredAt`.
  - `RiskLimitReachedEvent`: Ocorre quando um limite de risco é atingido.
    - Atributos: `StrategyId`, `UserId`, `RiskLimitType`, `CurrentValue`, `LimitValue`, `OccurredAt`.
  - `BacktestCompletedEvent`: Ocorre quando um backtest é finalizado com sucesso.
    - Atributos: `BacktestId`, `StrategyId`, `UserId`, `BacktestMetrics`, `OccurredAt`.
- **Implementação e Manipulação de Domain Events**:
  - Domain Events são levantados (raised) pelo Aggregate Root quando algo significativo acontece.
  - Os eventos são coletados por um Unit of Work (padrão) ou por um Event Dispatcher.
  - Os eventos são armazenados na tabela Outbox do banco de dados (Outbox Pattern) na mesma transação que modifica o Aggregate Root.
  - O Outbox Worker (em `apps/worker-outbox/`) lê os eventos da tabela Outbox e publica no Kafka.
  - Outros serviços podem consumir esses eventos do Kafka e reagir de acordo (ex: o Audit Worker registra o evento de auditoria, o Notification Worker envia uma notificação ao usuário, etc.).

#### 3.3.7 Factórias (Factories)
Uma Factory é um objeto responsável por criar objetos complexos do domínio (como Aggregates com várias entidades e value objects internas), encapsulando a lógica de criação e garantindo que os objetos sejam criados em um estado válido (invariantes satisfeitas).
- **Características**:
  - Podem ser classes estáticas, métodos estáticos em outras classes, ou interfaces/classes específicas.
  - Garantem que os objetos criados já estejam em um estado válido (todas as invariantes satisfeitas).
- **Exemplos de Factories na Crypto AI Platform**:
  - `StrategyFactory`: Cria uma nova estratégia a partir de parâmetros de entrada (nome, usuário, parâmetros de risco, configuração da estratégia, etc.), garantindo que a estratégia esteja válida.
  - `OrderFactory`: Cria uma nova ordem a partir de parâmetros de entrada, garantindo que a ordem esteja válida (lado, tipo, preço, quantidade, etc.).
- **Quando usar uma Factory**:
  - Quando a criação de um objeto é complexa (requer vários passos, validações, criação de objetos internos, etc.).
  - Quando você quer garantir que o objeto criado esteja sempre em um estado válido.
  - Quando a criação de um objeto requer acesso a outros serviços ou dados (embora, em geral, Factories devem ser simples e não ter muitas dependências; se precisar de muitas dependências, considere usar um Application Service com MediatR).

### 3.4. Mais Recursos sobre DDD na Crypto AI Platform
- Documento detalhado de modelagem de domínio: `docs/domain/domain-model.md`
- Diagramas de classes das entidades e agregados: `docs/domain/class-diagrams/`
- Exemplos de código de entidades, value objects, aggregates e domain services em `packages/domain/`

---

## 4. Clean Architecture

Clean Architecture (ou Arquitetura Limpa) é uma arquitetura de software proposta por Robert C. Martin ("Uncle Bob") que visa separar as preocupações e tornar o sistema mais testável, flexível, fácil de manter e adaptável a mudanças. A Crypto AI Platform segue rigorosamente a Clean Architecture, com dependências unidirecionais apontando para as camadas internas.

### 4.1. Princípios Fundamentais da Clean Architecture
1. **Independência de Frameworks**: O sistema não depende de frameworks específicos. Os frameworks são usados apenas como ferramentas, não definem a arquitetura.
2. **Testabilidade**: As regras de negócio (Domain) podem ser testadas sem o UI, banco de dados, servidor web ou qualquer outro elemento externo.
3. **Independência de UI**: A UI pode mudar facilmente sem alterar o resto do sistema (por exemplo, de um app web para um app móvel).
4. **Independência de Banco de Dados**: As regras de negócio não estão ligadas a um banco de dados específico. Você pode trocar PostgreSQL por MongoDB ou qualquer outro banco de dados sem alterar a lógica de negócio.
5. **Independência de Agentes Externos**: As regras de negócio não sabem nada sobre o mundo externo (APIs de exchanges, sistemas de pagamento, etc.). Integrações com serviços externos são feitas na camada de infraestrutura.

### 4.2. As Camadas da Clean Architecture (de dentro para fora)
A Clean Architecture é composta por camadas concêntricas, com dependências unidirecionais para dentro: camadas externas dependem de camadas internas, mas camadas internas não dependem de camadas externas.

#### 4.2.1. Camada de Domínio (Domain Layer)
A Camada de Domínio é a mais interna e a mais importante da arquitetura: ela contém o coração do sistema, a lógica de negócio essencial.
- **Responsabilidades Principais**:
  - Contém as **Entidades** do domínio (agregados raiz e entidades internas).
  - Contém os **Value Objects** do domínio.
  - Contém os **Domain Services** (serviços de domínio com lógica de negócio que não pertence a nenhuma entidade/VO).
  - Contém os **Domain Events** (eventos de domínio).
  - Define as **interfaces** de dependências externas que as camadas internas precisam (como interfaces de Repositórios, interfaces de serviços externos, etc.).
  - Garante a consistência das **invariantes de negócio** (regras que devem ser sempre verdadeiras).
- **Dependências**: NENHUMA dependência externa! A Camada de Domínio não depende de .NET Core, ASP.NET Core, Entity Framework, bibliotecas de terceiros, ou qualquer outra camada da arquitetura. A única exceção são bibliotecas de utilitários básicas e fundamentais (como `System.Private.CoreLib` para tipos básicos em .NET, ou bibliotecas de extension methods puramente funcionais).
- **Proibições na Camada de Domínio**:
  - Não usar `using` para assemblies de frameworks (como `Microsoft.AspNetCore.*`, `Microsoft.EntityFrameworkCore.*`, `StackExchange.Redis.*`, etc.).
  - Não ter lógica de infraestrutura (acesso a banco de dados, chamadas a APIs externas, logging, etc.).
  - Não ter dependências de nenhuma outra camada (Application, Infrastructure, Presentation).
- **Localização no Monorepo**: `packages/domain/`
- **Exemplos de Arquivos na Camada de Domínio**:
  - `Entities/Strategy.cs`: Entidade Agregado Raiz `Strategy`.
  - `ValueObjects/Price.cs`: Value Object `Price`.
  - `ValueObjects/RiskParameters.cs`: Value Object `RiskParameters`.
  - `Services/StrategyBacktestService.cs`: Domain Service para backtesting de estratégias.
  - `Events/StrategyCreatedEvent.cs`: Domain Event `StrategyCreatedEvent`.
  - `Repositories/IStrategyRepository.cs`: Interface do Repositório para `Strategy`.
  - `Abstractions/IExchangeIntegrationService.cs`: Interface para integração com exchanges (implementada na camada Infrastructure).

#### 4.2.2. Camada de Aplicação (Application Layer)
A Camada de Aplicação é a camada que orquestra o fluxo de trabalho da aplicação, definindo os casos de uso (Use Cases) do sistema. Ela não contém lógica de negócio (essa fica na Camada de Domínio), mas sim lógica de orquestração: coordena repositórios, serviços de domínio, serviços externos, etc., para executar uma operação.
- **Responsabilidades Principais**:
  - Definir os **Commands** e **Queries** do sistema (usando o padrão CQRS).
  - Implementar os **Command Handlers** e **Query Handlers** (usando o MediatR).
  - Definir os **DTOs** (Data Transfer Objects) e **View Models** para entrada e saída de dados.
  - Implementar validação de entrada de dados (usando FluentValidation).
  - Definir interfaces para serviços externos (se não tiverem sido definidos na Camada de Domínio).
  - Coordenar transações (usando Unit of Work).
  - Disparar e manipular eventos de integração (Integration Events).
- **Dependências**: Depende APENAS da Camada de Domínio. Não depende de Infrastructure, Presentation, frameworks ou bibliotecas externas, exceto bibliotecas específicas para Application Layer (como MediatR, FluentValidation, linguagens de mapeamento como AutoMapper (se for usado, mas é opcional)).
- **Padrões de Projeto Usados na Camada de Aplicação**:
  - **CQRS**: Command Query Responsibility Segregation (separar operações de escrita (Commands) de operações de leitura (Queries)).
  - **Mediator Pattern**: Usado com o MediatR para desacoplar requisições/queries de seus handlers.
  - **Unit of Work Pattern**: Para gerenciar transações e persistência de múltiplos agregados em uma única transação.
  - **DTO Pattern**: Data Transfer Objects para transferir dados entre camadas e para fora da API.
- **Localização no Monorepo**: `packages/application/`
- **Estrutura de Diretórios na Camada de Aplicação**:
  - `Features/`: Esta pasta é organizada por feature/caso de uso (não por tipo de arquivo!), seguindo o princípio de "screaming architecture" (arquitetura que "grita" qual é o domínio do sistema). Por exemplo:
    - `Features/Strategies/`:
      - `Commands/CreateStrategy/`:
        - `CreateStrategyCommand.cs`: Command para criar uma estratégia.
        - `CreateStrategyCommandHandler.cs`: Handler para o Command.
        - `CreateStrategyCommandValidator.cs`: Validador para o Command.
        - `CreateStrategyRequest.cs`: DTO de entrada para a API (requisição).
        - `CreateStrategyResponse.cs`: DTO de saída para a API (resposta).
      - `Queries/GetStrategyById/`:
        - `GetStrategyByIdQuery.cs`: Query para obter uma estratégia por ID.
        - `GetStrategyByIdQueryHandler.cs`: Handler para a Query.
        - `GetStrategyByIdResponse.cs`: DTO de saída com os dados da estratégia.
      - `Events/`: Handlers para eventos de domínio ou eventos de integração relacionados a estratégias.
  - `Interfaces/`: Interfaces específicas da camada Application (se houver).
  - `Exceptions/`: Exceções específicas da camada Application (como `StrategyNotFoundException`, `ValidationException`, etc.).
  - `Behaviors/`: Pipeline Behaviors do MediatR (para logging, validação, tratamento de exceções, etc., que são executados antes/depois dos handlers).
  - `Mappings/`: Perfis de mapeamento (como AutoMapper Profiles, se for usado AutoMapper).
- **Exemplos de Arquivos na Camada de Aplicação**:
  - `Features/Strategies/Commands/CreateStrategy/CreateStrategyCommand.cs`:
  - `Features/Strategies/Commands/CreateStrategy/CreateStrategyCommandHandler.cs`:
  - `Features/Strategies/Queries/GetStrategyById/GetStrategyByIdQuery.cs`:
  - `Features/Strategies/Queries/GetStrategyById/GetStrategyByIdQueryHandler.cs`:
  - `Behaviors/ValidationBehavior.cs`: Pipeline Behavior para validação de Commands/Queries usando FluentValidation.
  - `Behaviors/LoggingBehavior.cs`: Pipeline Behavior para logging de Commands/Queries.

#### 4.2.3. Camada de Infraestrutura (Infrastructure Layer)
A Camada de Infraestrutura implementa as interfaces definidas nas camadas internas (Domain e Application). Ela contém tudo o que é "externo" ao núcleo do sistema: acesso a bancos de dados, integração com serviços externos (exchanges, Kafka, APIs de terceiros), logging, caching, etc.
- **Responsabilidades Principais**:
  - Implementar os **Repositórios** definidos na Camada de Domínio (ex: `StrategyRepository` que implementa `IStrategyRepository`).
  - Implementar a integração com **bancos de dados** (PostgreSQL usando Entity Framework Core, Redis usando StackExchange.Redis).
  - Implementar a integração com **serviços externos** (APIs de exchanges, APIs de notificações, etc.) via interfaces definidas na Camada de Domínio ou Application.
  - Implementar a integração com **mensageria** (Kafka para publish/subscribe de eventos).
  - Implementar **logging** (usando Serilog).
  - Implementar **caching** (usando Redis).
  - Implementar **autenticação/autorização** externa (se for usar provedores externos como Azure AD, Auth0, etc.).
- **Dependências**: Depende da Camada de Aplicação e da Camada de Domínio, e de bibliotecas externas (como Entity Framework Core, StackExchange.Redis, Confluent.Kafka, Serilog, etc.).
- **Localização no Monorepo**: `packages/infrastructure/`
- **Estrutura de Diretórios na Camada de Infraestrutura**:
  - `Data/`: Tudo relacionado a acesso a dados.
    - `DbContexts/`: `AppDbContext.cs` (EF Core DbContext principal).
    - `EntityConfigurations/`: Configurações de entidades do EF Core (usando `IEntityTypeConfiguration<TEntity>`).
    - `Migrations/`: Migrações do EF Core (geradas pelo comando `dotnet ef migrations add`).
    - `Repositories/`: Implementações dos Repositórios (ex: `StrategyRepository.cs`).
    - `UnitOfWork.cs`: Implementação do padrão Unit of Work.
  - `ExternalServices/`: Integrações com serviços externos.
    - `Exchanges/`: Integrações com Binance, Coinbase Pro, etc.
      - `Binance/`:
        - `BinanceIntegrationService.cs`: Implementa `IExchangeIntegrationService`.
        - `BinanceApiClient.cs`: Cliente para a API da Binance.
    - `Kafka/`: Integração com o Kafka.
      - `KafkaProducer.cs`: Produtor de eventos para o Kafka.
      - `KafkaConsumer.cs`: Consumidor de eventos do Kafka.
  - `Caching/`: Implementação de caching.
    - `RedisCacheService.cs`: Serviço de cache usando Redis.
  - `Logging/`: Configuração de logging (usando Serilog).
    - `SerilogConfigurator.cs`: Classe para configurar o Serilog.
- **Exemplos de Arquivos na Camada de Infraestrutura**:
  - `Data/DbContexts/AppDbContext.cs`:
  - `Data/EntityConfigurations/StrategyEntityConfiguration.cs`:
  - `Data/Repositories/StrategyRepository.cs`: Implementa `IStrategyRepository`.
  - `ExternalServices/Exchanges/Binance/BinanceIntegrationService.cs`: Implementa `IExchangeIntegrationService`.
  - `ExternalServices/Kafka/KafkaProducer.cs`:

#### 4.2.4. Camada de Apresentação (Presentation Layer)
A Camada de Apresentação é a camada mais externa: ela lida com a interação com o mundo exterior (usuários via UI, APIs, etc.). Para a Crypto AI Platform, a Camada de Apresentação inclui a API REST (ASP.NET Core Web API) e o Frontend (Next.js).
- **Responsabilidades Principais**:
  - Configurar a **injeção de dependência (DI)** de todos os serviços das camadas internas e da camada de infraestrutura.
  - Configurar os **middlewares** do ASP.NET Core (tratamento de exceções, autenticação, autorização, CORS, rate limiting, etc.).
  - Implementar os **Controllers API** (ou Minimal APIs, se for escolhida essa abordagem).
  - **Receber requisições HTTP** (do frontend, de apps móveis, de clientes API externos).
  - **Mapear requisições HTTP** para Commands/Queries do MediatR.
  - **Enviar respostas HTTP** de volta ao cliente (DTOs de resposta, status codes adequados).
  - Implementar a interface do usuário (Frontend, em `apps/web/`).
- **Dependências**: Depende da Camada de Aplicação e da Camada de Infraestrutura.
- **Localização no Monorepo**:
  - `packages/presentation/`: Código compartilhado da Camada de Apresentação (se houver).
  - `apps/api-core/`, `apps/api-market-data/`, `apps/api-trading/`, `apps/api-risk/`, `apps/api-analytics/`: Aplicativos ASP.NET Core Web API (que implementam a Camada de Apresentação para cada API).
  - `apps/web/`: Aplicativo Next.js Frontend (Camada de Apresentação para a UI).
- **Estrutura de Diretórios em um App API (ex: `apps/api-core/`)**:
  - `Controllers/`: Controladores API (se for usar Controllers em vez de Minimal APIs).
    - `StrategiesController.cs`: Controlador para endpoints relacionados a estratégias.
  - `Middlewares/`: Middlewares customizados.
    - `ExceptionHandlingMiddleware.cs`: Middleware para tratamento global de exceções.
  - `Program.cs`: Arquivo principal de configuração do aplicativo ASP.NET Core (configura serviços, middlewares, pipeline de requisições).
  - `appsettings.json`, `appsettings.Development.json`, `appsettings.Production.json`: Arquivos de configuração do ambiente.
  - `Properties/launchSettings.json`: Configurações de lançamento do Visual Studio/VS Code.

### 4.3. Fluxo de uma Requisição na Clean Architecture (Exemplo Prático)
Para entender melhor, vamos ver o fluxo completo de uma requisição "Criar Estratégia" na Crypto AI Platform:
1. **Usuário interage com o Frontend**: O usuário preenche o formulário de criação de estratégia no app Next.js e clica em "Criar".
2. **Frontend envia requisição HTTP**: O Frontend (apps/web/) envia uma requisição HTTP POST para o endpoint `/api/v1/strategies` na API Gateway ou API Core.
3. **API Core - Controller**: O Controller (ex: `StrategiesController.cs` em `apps/api-core/Controllers/`) recebe a requisição. Ele valida que a requisição está autenticada e autorizada, mapeia o `CreateStrategyRequest` (DTO de entrada) para um `CreateStrategyCommand`, e envia o Command para o MediatR.
4. **MediatR Pipeline Behaviors**: O MediatR executa os Pipeline Behaviors na ordem configurada:
   a. `LoggingBehavior`: Registra no log que o Command foi recebido.
   b. `ValidationBehavior`: Valida o Command usando o `CreateStrategyCommandValidator.cs` (FluentValidation). Se houver erros de validação, retorna uma resposta 400 Bad Request com os detalhes dos erros.
   c. Outros Behaviors, se houver (ex: para caching, métricas, etc.).
5. **Command Handler**: O MediatR invoca o `CreateStrategyCommandHandler.cs` na Camada de Aplicação.
   a. O Handler usa o `IStrategyRepository` (injetado via DI) para verificar se já existe uma estratégia com o mesmo nome para o usuário.
   b. O Handler usa o `StrategyFactory` (Domain Service ou Factory) para criar um novo Aggregate `Strategy`, garantindo que todas as invariantes sejam satisfeitas.
   c. O Handler adiciona a nova estratégia ao Repositório (`_strategyRepository.AddAsync(strategy)`).
   d. O Handler usa o `IUnitOfWork` para salvar as mudanças no banco de dados (`_unitOfWork.SaveChangesAsync()`). Isso também salva quaisquer Domain Events na tabela Outbox (Outbox Pattern).
   e. O Handler retorna um `CreateStrategyResponse` (DTO de saída) com o ID da estratégia criada.
6. **MediatR retorna para o Controller**: O Controller recebe a resposta do Handler.
7. **Controller retorna resposta HTTP**: O Controller retorna uma resposta HTTP 201 Created com o `CreateStrategyResponse` no corpo.
8. **Frontend exibe resultado**: O Frontend recebe a resposta, exibe uma mensagem de sucesso para o usuário, e atualiza a lista de estratégias.
9. **Outbox Worker**: Em segundo plano, o Outbox Worker (apps/worker-outbox/) lê os Domain Events da tabela Outbox, publica-os no Kafka, e marca os eventos como processados.
10. **Consumidores de Eventos**: Outros workers/serviços consumem os eventos do Kafka e reagem a eles (ex: Audit Worker registra o evento de auditoria, Notification Worker envia uma notificação por e-mail ao usuário, etc.).

### 4.4. Mais Recursos sobre Clean Architecture na Crypto AI Platform
- Documento detalhado com diagramas: `docs/architecture/clean-architecture.md`
- Exemplos de código em `packages/domain/`, `packages/application/`, `packages/infrastructure/`, e `apps/api-core/`

---

## 5. Princípios SOLID

Os princípios SOLID são cinco regras fundamentais para o design de software orientado a objetos que visam criar código mais legível, testável, flexível, fácil de manter e de dar manutenção. Eles foram compilados por Robert C. Martin ("Uncle Bob") e são rigorosamente seguidos na Crypto AI Platform. Todo membro da equipe deve conhecer e aplicar todos os princípios SOLID em todo o código produzido.

### 5.1. S — Single Responsibility Principle (Princípio da Responsabilidade Única)
> Uma classe (ou módulo, função, componente, ou outra entidade de software) deve ter um e apenas um motivo para mudar, ou seja, deve ter apenas uma responsabilidade ou tarefa principal.

#### 5.1.1. Por que esse princípio é importante?
- **Testabilidade**: Classes com uma única responsabilidade são mais fáceis de testar, pois há menos caminhos de código a verificar.
- **Manutenibilidade**: Se precisar mudar algo, você só precisa modificar uma única classe, reduzindo o risco de introduzir bugs em outras partes do sistema.
- **Reutilização**: Classes com responsabilidades bem definidas são mais fáceis de reutilizar em diferentes contextos.

#### 5.1.2. Exemplos
##### Exemplo Ruim (Violando o Princípio)
Vamos imaginar uma classe `TradingService` na Camada de Aplicação que faz várias coisas diferentes:
- Autentica usuários na exchange.
- Busca dados de mercado.
- Executa ordens de trading.
- Envia notificações por e-mail sobre ordens executadas.
- Gera relatórios de performance.
Isso viola o SRP porque a classe tem pelo menos 5 motivos para mudar!
```csharp
// Ruim! Violando SRP
public class TradingService
{
    public void AuthenticateUser(Guid userId, string apiKey, string apiSecret) { /* ... */ }
    public async Task<MarketData> GetMarketDataAsync(string symbol) { /* ... */ }
    public async Task<Order> ExecuteOrderAsync(OrderRequest request) { /* ... */ }
    public void SendOrderExecutedEmail(Order order) { /* ... */ }
    public Report GeneratePerformanceReport(Guid userId, DateRange range) { /* ... */ }
}
```

##### Exemplo Bom (Seguindo o Princípio)
Agora vamos dividir essa classe em várias classes menores e interfaces menores e interfaces separadas, cada uma com uma única responsabilidade:
```csharp
// Bom! Seguindo SRP
// Responsável apenas por autenticação de usuários na exchange
public class ExchangeAuthenticationService
{
    public void AuthenticateUser(Guid userId, string apiKey, string apiSecret) { /* ... */ }
}

// Responsável apenas por buscar dados de mercado
public class MarketDataService
{
    public async Task<MarketData> GetMarketDataAsync(string symbol) { /* ... */ }
}

// Responsável apenas por executar ordens de trading
public class OrderExecutionService
{
    public async Task<Order> ExecuteOrderAsync(OrderRequest request) { /* ... */ }
}

// Responsável apenas por enviar e-mails
public class EmailNotificationService
{
    public void SendOrderExecutedEmail(Order order) { /* ... */ }
}

// Responsável apenas por gerar relatórios
public class PerformanceReportService
{
    public Report GeneratePerformanceReport(Guid userId, DateRange range) { /* ... */ }
}
```
Cada uma dessas classes tem um único motivo para mudar e segue o SRP!

### 5.2. O — Open/Closed Principle (Princípio Aberto/Fechado)
> Entidades de software (classes, módulos, funções, etc.) devem estar Abertas para extensão, mas FECHADAS para modificação.

Isso significa que você deve ser capaz de adicionar nova funcionalidade ou comportamento a um sistema sem modificar o código existente (que já foi testado e está em produção). Isso minimiza o risco de introduzir bugs em código que já funcionava.

#### 5.2.1. Como implementar esse princípio?
- Usar **polimorfismo** (herança ou implementação de interfaces).
- Usar **injeção de dependência** (DI).
- Usar o **padrões de projeto** como Strategy, Observer, etc.

#### 5.2.2. Exemplos
##### Exemplo Ruim (Violando o Princípio)
Vamos imaginar uma classe `OrderExecutor` que executa ordens de diferentes tipos (Market, Limit, Stop Loss, etc. Se precisarmos adicionar um novo tipo de ordem (por exemplo, Trailing Stop), temos que modificar essa classe, violando o OCP.
```csharp
// Ruim! Violando OCP
public class OrderExecutor
{
    public async Task ExecuteAsync(Order order)
    {
        switch (order.Type)
        {
            case OrderType.Market:
                await ExecuteMarketOrderAsync(order);
                break;
            case OrderType.Limit:
                await ExecuteLimitOrderAsync(order);
                break;
            case OrderType.StopLoss:
                await ExecuteStopLossOrderAsync(order);
                break;
            // Se quiser adicionar um novo tipo de ordem, tem que editar essa classe!
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
```

##### Exemplo Bom (Seguindo o Princípio)
Agora vamos usar o **padrão Strategy**, definindo uma interface `IOrderExecutionStrategy` e implementações específicas para cada tipo de ordem. Para adicionar um novo tipo, basta criar uma nova implementação da interface — SEM MODIFICAR O CÓDIGO EXISTENTE!
```csharp
// Bom! Seguindo OCP
// Primeiro, defina a interface
public interface IOrderExecutionStrategy
{
    Task ExecuteAsync(Order order);
    bool CanHandle(OrderType orderType);
}

// Agora, implemente para cada tipo de ordem
public class MarketOrderExecutionStrategy : IOrderExecutionStrategy
{
    public bool CanHandle(OrderType orderType) => orderType == OrderType.Market;
    public Task ExecuteAsync(Order order) { /* ... */ }
}

public class LimitOrderExecutionStrategy : IOrderExecutionStrategy
{
    public bool CanHandle(OrderType orderType) => orderType == OrderType.Limit;
    public Task ExecuteAsync(Order order) { /* ... */ }
}

public class StopLossOrderExecutionStrategy : IOrderExecutionStrategy
{
    public bool CanHandle(OrderType orderType) => orderType == OrderType.StopLoss;
    public Task ExecuteAsync(Order order) { /* ... */ }
}

// E agora, a classe OrderExecutor usa as estratégias via injeção de dependência
public class OrderExecutor
{
    private readonly IEnumerable<IOrderExecutionStrategy> _strategies;

    public OrderExecutor(IEnumerable<IOrderExecutionStrategy> strategies)
    {
        _strategies = strategies;
    }

    public async Task ExecuteAsync(Order order)
    {
        var strategy = _strategies.FirstOrDefault(s => s.CanHandle(order.Type));
        if (strategy == null)
            throw new NotSupportedException($"Order type {order.Type} not supported");

        await strategy.ExecuteAsync(order);
    }
}
```
Perfeito! Agora, para adicionar um novo tipo de ordem `TrailingStop`, basta criar uma classe `TrailingStopOrderExecutionStrategy` que implementa `IOrderExecutionStrategy`, e registrar no container de DI — NÃO PRECISA MODIFICAR NADA NA CLASSE `OrderExecutor`!

### 5.3. L — Liskov Substitution Principle (Princípio da Substituição de Liskov)
> Se `S` é um subtipo de `T`, então os objetos do tipo `T` em um programa devem poder ser substituídos por objetos do tipo `S` sem alterar a correção desse programa.

Em outras palavras: classes derivadas (subclasses) devem ser substituíveis por suas classes base (superclasses) sem quebrar a aplicação. Se você substituir uma instância de uma classe base por uma de uma de suas subclasses, o sistema deve continuar funcionando exatamente da mesma forma.

#### 5.3.1. Regras importantes para seguir o LSP
1. As pré-condições (pré-requisitos para que um método funcione) não podem ser FORTALECIDAS em uma subclass.
2. As pós-condições (resultados esperados de um método) não podem ser ENFRAQUECIDAS em uma subclass.
3. As invariantes da classe base (condições que devem sempre devem ser sempre verdadeiras) devem ser mantidas na subclass.
4. Métodos de uma subclass não devem lançar exceções novas que não são esperadas pelo cliente da classe base.

#### 5.3.2. Exemplos
##### Exemplo Ruim (Violando o Princípio)
Vamos imaginar uma classe base `ExchangeIntegrationService` e uma subclasse `BinanceIntegrationService`. A subclass modifica o comportamento de um método de forma que quebraria o sistema se substituída.
```csharp
// Ruim! Violando LSP
public abstract class ExchangeIntegrationService
{
    public abstract Task<decimal> GetCurrentPriceAsync(string symbol);
}

public class BinanceIntegrationService : ExchangeIntegrationService
{
    public override async Task<decimal> GetCurrentPriceAsync(string symbol)
    {
        // Funciona para BTC/USDT, ETH/USDT, etc.
        // Mas, mas se o símbolo for diferente, lança uma InvalidOperationException
        if (symbol.StartsWith("SOL"))
            throw new InvalidOperationException("Binance doesn't support SOL futures");
    }
}

// Agora, se temos código que usa ExchangeIntegrationService:
public class PriceMonitorService
{
    private readonly ExchangeIntegrationService _exchangeService;

    public PriceMonitorService(ExchangeIntegrationService exchangeService)
    {
        _exchangeService = exchangeService;
    }

    public async Task MonitorPricesAsync(List<string> symbols)
    {
        foreach (var symbol in symbols)
        {
            // Espera que GetCurrentPriceAsync nunca lance InvalidOperationException!
            var price = await _exchangeService.GetCurrentPriceAsync(symbol);
        }
    }
}
```
Se substituirmos `ExchangeIntegrationService` por `BinanceIntegrationService`, o `PriceMonitorService` vai quebrar com símbolos que começam com SOL! Isso viola o LSP.

##### Exemplo Bom (Seguindo o Princípio)
Agora, vamos corrigir: na classe base define que o método pode lançar uma exceção `SymbolNotSupportedException`, e a subclasse lança essa mesma exceção (não uma nova):
```csharp
// Bom! Seguindo LSP
// Primeiro, define a exceção esperada
public class SymbolNotSupportedException : Exception { }

// Agora, classe base define que pode lançar essa exceção
public abstract class ExchangeIntegrationService
{
    public abstract Task<decimal> GetCurrentPriceAsync(string symbol);
}

// Subclasse Binance lança a exceção definida pela classe base
public class BinanceIntegrationService : ExchangeIntegrationService
{
    public override async Task<decimal> GetCurrentPriceAsync(string symbol)
    {
        if (symbol.StartsWith("SOL"))
            throw new SymbolNotSupportedException();
        // ... resto do código
    }
}

// Agora, o PriceMonitorService pode esperar essa exceção
public class PriceMonitorService
{
    private readonly ExchangeIntegrationService _exchangeService;

    public PriceMonitorService(ExchangeIntegrationService exchangeService)
    {
        _exchangeService = exchangeService;
    }

    public async Task MonitorPricesAsync(List<string> symbols)
    {
        foreach (var symbol in symbols)
        {
            try
            {
                var price = await _exchangeService.GetCurrentPriceAsync(symbol);
            }
            catch (SymbolNotSupportedException)
            {
                // Trata o erro corretamente
            }
        }
    }
}
```
Agora, se substituirmos por outra subclasse (como `CoinbaseIntegrationService`), o `PriceMonitorService` vai continuar funcionando corretamente!

### 5.4. I — Interface Segregation Principle (Princípio da Segregação de Interfaces)
> Nenhum cliente deve ser forçado a depender de métodos que não usa.

Em outras palavras: é melhor ter várias interfaces pequenas, específicas e coesas, do que uma única interface grande, geral e com muitos métodos que os clientes não precisam.

#### 5.4.1. Por que esse princípio é importante?
- **Desacoplamento**: As classes dependem apenas das interfaces que realmente usam, reduzindo o acoplamento entre classes.
- **Facilita a manutenção**: Se você precisar modificar uma interface, só afeta apenas as classes que implementam-na, não todas as classes que dependiam daquela interface grande.
- **Facilita os testes**: É mais fácil criar mocks para interfaces pequenas e específicas.

#### 5.4.2. Exemplos
##### Exemplo Ruim (Violando o Princípio)
Vamos imaginar uma interface `IUserManager` com muitos métodos que nem todos os clientes precisam:
```csharp
// Ruim! Violando ISP
public interface IUserManager
{
    // Métodos de autenticação
    void Authenticate(string username, string password);
    void Logout(Guid userId);

    // Métodos de gerenciamento de perfil
    UserProfile GetUserProfile(Guid userId);
    void UpdateUserProfile(UserProfile profile);

    // Métodos de gerenciamento de chaves API
    ExchangeApiKey GetExchangeApiKey(Guid userId, string exchange);
    void SaveExchangeApiKey(Guid userId, ExchangeApiKey apiKey);

    // Métodos de relatórios
    Report GenerateUserReport(Guid userId);
}
```
Se uma classe precisa só precisar de métodos de autenticação, ela ainda tem que implementar todos os métodos da interface! Isso viola o ISP.

##### Exemplo Bom (Seguindo o Princípio)
Agora, vamos dividir essa interface em várias interfaces pequenas e específicas!
```csharp
// Bom! Seguindo ISP
// Interfaces separadas por responsabilidade
public interface IUserAuthenticationService
{
    void Authenticate(string username, string password);
    void Logout(Guid userId);
}

public interface IUserProfileService
{
    UserProfile GetUserProfile(Guid userId);
    void UpdateUserProfile(UserProfile profile);
}

public interface IUserExchangeApiKeyService
{
    ExchangeApiKey GetExchangeApiKey(Guid userId, string exchange);
    void SaveExchangeApiKey(Guid userId, ExchangeApiKey apiKey);
}

public interface IUserReportService
{
    Report GenerateUserReport(Guid userId);
}
```
Agora, classes podem implementar apenas as interfaces que realmente usam!

### 5.5. D — Dependency Inversion Principle (Princípio da Inversão de Dependência)
Esse princípio tem duas partes:
1. Módulos de alto nível (que contêm a lógica de negócio) NÃO devem depender de módulos de baixo nível (que lidam com detalhes de infraestrutura). Ambos devem depender de abstrações (interfaces ou classes abstratas).
2. Abstrações NÃO devem depender de detalhes. Detalhes (implementações concretas) devem depender de abstrações.

#### 5.5.1. Por que esse princípio é importante?
- **Testabilidade**: Como módulos de alto nível (como a Camada de Aplicação e Domínio) podem ser testados sem depender de detalhes de infraestrutura (como banco de dados, APIs de exchanges, etc.), usando mocks de interfaces.
- **Flexibilidade**: Você pode trocar implementações concretas (por exemplo, trocar PostgreSQL por MongoDB, Binance por Coinbase) sem alterar o código dos módulos de alto nível.
- **Clean Architecture**: Esse princípio é a base da Clean Architecture, que usamos na Crypto AI Platform!

#### 5.5.2. Exemplos
##### Exemplo Ruim (Violando o Princípio)
Vamos imaginar um `StrategyBacktestService` (módulo de alto nível) que depende diretamente de `PostgreSqlStrategyRepository` (módulo de baixo nível, detalhe de infraestrutura). Isso viola o DIP!
```csharp
// Ruim! Violando DIP
using CryptoAIPlatform.Infrastructure.Data.Repositories; // Dependência de detalhe!
using CryptoAIPlatform.Application.Services;

// Módulo de alto nível (Application Layer)
public class StrategyBacktestService
{
    // Depende diretamente da implementação concreta!
    private readonly PostgreSqlStrategyRepository _strategyRepository;

    public StrategyBacktestService(PostgreSqlStrategyRepository strategyRepository)
    {
        _strategyRepository = strategyRepository;
    }

    public async Task RunBacktestAsync(Guid strategyId)
    {
        var strategy = await _strategyRepository.GetByIdAsync(strategyId);
        // ... código de backtest
    }
}
```

##### Exemplo Bom (Seguindo o Princípio)
Agora, vamos fazer com que o `StrategyBacktestService` (alto nível) dependa da **interface** `IStrategyRepository` (abstração), definida na Camada de Domínio, e `PostgreSqlStrategyRepository` (baixo nível) implementa essa interface!
```csharp
// Bom! Seguindo DIP
// Interface (abstração) definida na Camada de Domínio (alto nível)
namespace CryptoAIPlatform.Domain.Repositories;
public interface IStrategyRepository
{
    Task<Strategy?> GetByIdAsync(Guid strategyId, CancellationToken cancellationToken);
    // outros métodos
}

// Implementação concreta (detalhe) definida na Camada de Infraestrutura (baixo nível), que depende da abstração
namespace CryptoAIPlatform.Infrastructure.Data.Repositories;
public class PostgreSqlStrategyRepository : IStrategyRepository
{
    public async Task<Strategy?> GetByIdAsync(Guid strategyId, CancellationToken cancellationToken)
    {
        // ... implementação usando EF Core e PostgreSQL
    }
}

// Módulo de alto nível (Application Layer) depende da abstração!
namespace CryptoAIPlatform.Application.Services;
public class StrategyBacktestService
{
    private readonly IStrategyRepository _strategyRepository; // Agora depende da interface!

    public StrategyBacktestService(IStrategyRepository strategyRepository) // Injeção de dependência!
    {
        _strategyRepository = strategyRepository;
    }

    public async Task RunBacktestAsync(Guid strategyId, CancellationToken cancellationToken)
    {
        var strategy = await _strategyRepository.GetByIdAsync(strategyId, cancellationToken);
        // ... código de backtest
    }
}
```
Perfeito! Agora, se quiser trocar PostgreSQL por MongoDB, basta criar uma nova classe `MongoDbStrategyRepository` que implementa `IStrategyRepository` e substituir no container de DI — NÃO PRECISA MODIFICAR NADA NO `StrategyBacktestService`!

### 5.6. Resumo dos Princípios SOLID na Crypto AI Platform
Todos os 5 princípios SOLID são seguidos em TODAS as camadas da Clean Architecture, e todo o código da Crypto AI Platform deve seguir esses princípios! Se você tiver dúvidas sobre se seu código segue um dos princípios, consulte o Arquiteteto Chefe ou o Product Owner!


---

## 6. CQRS (Command Query Responsibility Segregation)

CQRS é um padrão que separa as operações de leitura (Queries) das operações de escrita (Commands).

### 6.1. Commands
- Representam ações que modificam o estado do sistema (ex: `CreateStrategyCommand`, `PlaceOrderCommand`).
- São imutáveis, contêm apenas dados necessários para executar a ação.
- São processados por `CommandHandlers` na Camada de Aplicação.
- Não retornam dados (ou retornam apenas o ID da entidade criada).

### 6.2. Queries
- Representam operações de leitura (ex: `GetStrategyByIdQuery`, `GetTradeHistoryQuery`).
- São imutáveis, contêm apenas parâmetros de consulta.
- São processados por `QueryHandlers` na Camada de Aplicação.
- Retornam DTOs (Data Transfer Objects) ou View Models, não Entidades de Domínio.

### 6.3. Ferramenta
Usamos **MediatR** para implementar CQRS no .NET:
- `IRequest`: Interface para Commands e Queries.
- `IRequestHandler<TRequest, TResponse>`: Interface para Handlers.
- `IPipelineBehavior`: Para cross-cutting concerns (logging, validação, transações, etc.).

---

## 7. Event-Driven Architecture (Arquitetura Orientada a Eventos)

A plataforma é altamente orientada a eventos para permitir comunicação assíncrona entre serviços, desacoplamento e escalabilidade.

### 7.1. Tipos de Eventos
- **Eventos de Domínio**: Representam algo que aconteceu no domínio (ex: `StrategyCreated`, `OrderExecuted`).
- **Eventos de Integração**: Representam algo que aconteceu em outro serviço e que este serviço precisa saber (ex: `UserCreatedIntegrationEvent`).

### 7.2. Event Broker
Usamos **Apache Kafka** como broker de eventos:
- **Tópicos**: Cada tipo de evento tem seu próprio tópico (ex: `cryptoai.strategy.created`).
- **Produtores**: Serviços que publicam eventos.
- **Consumidores**: Serviços que se inscrevem em tópicos e processam eventos.
- **Idempotência**: Consumidores devem ser idempotentes, ou seja, processar o mesmo evento múltiplas vezes sem causar efeitos colaterais indesejados.

### 7.3. Outbox Pattern
Para garantir que eventos sejam publicados de forma confiável (mesmo se o broker estiver temporariamente indisponível), usamos o **Outbox Pattern**:
1. Salva o evento na tabela `Outbox` do banco de dados na mesma transação que as mudanças de estado.
2. Um worker separado lê a tabela `Outbox` e publica os eventos no Kafka.
3. Depois de publicado, o evento é marcado como processado na tabela `Outbox`.

---

## 8. Banco de Dados

### 8.1. PostgreSQL (Banco Relacional)
- **Uso**: Armazenamento de dados estruturados (usuários, estratégias, ordens, etc.).
- **ORM**: Entity Framework Core 9.
- **Padrões**:
  - **Code First**: Definimos o modelo no código e geramos as migrations.
  - **Migrations**: Todas as mudanças de schema são versionadas via migrations EF Core.
  - **Índices**: Criamos índices apropriados para consultas frequentes.
  - **Restrições**: Usamos restrições de banco de dados (FK, NOT NULL, CHECK) para garantir integridade de dados.
  - **Transações**: Usamos transações para garantir atomicidade de operações que modificam múltiplos agregados.

### 8.2. Redis (Cache e Armazenamento de Chave-Valor)
- **Uso**:
  - Cache de dados frequentes (ex: dados de mercado recentes).
  - Gerenciamento de sessões.
  - Rate limiting.
  - Distributed locks.
- **Padrões**:
  - **TTL**: Todas as chaves têm TTL (Time To Live) definido.
  - **Cache-Aside**: Busca no cache primeiro; se não encontrar, busca no banco e popula o cache.

### 8.3. Guidelines de Banco de Dados
- **Separação de Leitura/Escrita**: Para CQRS, podemos usar bancos separados para leitura (otimizados para consultas) e escrita (otimizados para transações).
- **Normalização**: Modelo de banco normalizado até a 3NF para evitar redundância.
- **Desnormalização**: Para casos de leitura frequentes, podemos usar desnormalização ou views materializadas.
- **Segurança**:
  - Acesso ao banco apenas via usuários com privilégios mínimos necessários.
  - Senhas fortes armazenadas em secrets managers, nunca em código.
  - Criptografia de dados sensíveis em repouso (colunas criptografadas).
  - Criptografia em trânsito (SSL/TLS).

---

## 9. Backend (.NET 9)

### 9.1. ASP.NET Core Web API
- **Versão**: .NET 9 LTS.
- **Estrutura do Projeto**:
  - `apps/api/`: Projeto principal da API.
  - `Program.cs`: Configuração de DI, middlewares, pipeline de requisição.
  - `Controllers/`: Controladores API (camada fina, apenas delega para MediatR).
  - `Middlewares/`: Middlewares customizados (ex: exception handling, logging, rate limiting).

### 9.2. Entity Framework Core 9
- **Configuração**:
  - `DbContext`: Definido na Camada de Infraestrutura.
  - `IEntityTypeConfiguration<T>`: Configurações de entidade separadas por arquivos.
  - `Migrations`: Projeto separado ou na Camada de Infraestrutura.
- **Padrões**:
  - No tracking para consultas de leitura (`AsNoTracking()`).
  - Lazy loading desabilitado (usar eager loading ou explicit loading).
  - Pagination para consultas que retornam muitos dados.

### 9.3. Validação
- **FluentValidation**: Usamos FluentValidation para validação de Commands, Queries e DTOs.
- **Validação de Domínio**: Validações intrínsecas do domínio são feitas nas Entidades e Value Objects (ex: `Price` não pode ser negativo).
- **Validação de Aplicação**: Validações de entrada do usuário são feitas no handler ou em pipeline behaviors do MediatR.

### 9.4. Autenticação e Autorização
- **Autenticação**: JWT (JSON Web Tokens) com ASP.NET Core Identity.
- **Autorização**:
  - Role-based authorization (roles: Admin, User, Trader).
  - Policy-based authorization para regras complexas.
  - Resource-based authorization para verificar se o usuário tem acesso a um recurso específico (ex: uma estratégia).

### 9.5. Worker Services
- **Uso**: Processamento em background (ex: backtesting, integração com exchanges, processamento de outbox).
- **Estrutura**: `apps/worker/`.
- **Padrões**:
  - Resilience (Polly) para retentativas em caso de falhas.
  - Circuit Breaker para evitar chamadas repetidas a serviços que estão falhando.

---

## 10. Frontend (Next.js + React + TypeScript)

### 10.1. Stack do Frontend
- **Framework**: Next.js 14+ (App Router).
- **Linguagem**: TypeScript 5+ (strict mode obrigatório).
- **UI**: React 18+, Tailwind CSS, shadcn/ui.
- **Gerenciamento de Estado**: React Query (para dados do servidor), Zustand (para estado global do cliente).
- **Formulários**: React Hook Form + Zod.
- **Testes**: Jest (unitários), Playwright (e2e).

### 10.2. Estrutura do Projeto
`apps/web/`:
- `app/`: Next.js App Router (rotas, layouts, pages).
- `components/`: Componentes React reutilizáveis.
- `lib/`: Funções utilitárias, hooks customizados, configuração de React Query, etc.
- `types/`: Definições de tipos TypeScript.
- `features/`: Features organizadas por módulo (ex: `features/strategies/`, `features/trading/`).

### 10.3. Padrões de Frontend
- **Componentes**:
  - Componentes pequenos, focados em uma responsabilidade.
  - Componentes de apresentação (dumb) e container (smart) separados.
  - Uso de hooks customizados para lógica reutilizável.
- **Gerenciamento de Estado**:
  - React Query para dados do servidor (cache, refetch, stale-while-revalidate).
  - Zustand para estado global do cliente (evitar prop drilling).
- **Validação**: Zod para validação de formulários e dados do servidor.
- **Acessibilidade**: Sempre usar ARIA attributes e garantir que a aplicação seja acessível.
- **Performance**:
  - Lazy loading de componentes.
  - Otimização de imagens.
  - Server Components para conteúdo estático ou com dados que não mudam frequentemente.
  - Client Components apenas para interatividade do usuário.

---

## 11. Testes

Testes são fundamentais para garantir a qualidade e a confiabilidade da plataforma. Todo código novo ou modificado deve ter testes adequados.

### 11.1. Pirâmide de Testes
- **Unit Tests (Unitários)**: ~70% dos testes. Testam unidades individuais (funções, classes, componentes) em isolamento.
  - **Ferramentas**: xUnit (.NET), Moq (mocks .NET), Jest (TypeScript).
  - **Padrões**: AAA (Arrange-Act-Assert), F.I.R.S.T. (Fast, Independent, Repeatable, Self-validating, Timely).
- **Integration Tests (Integração)**: ~20% dos testes. Testam a interação entre múltiplos componentes (ex: API + banco de dados, API + Kafka).
  - **Ferramentas**: xUnit (.NET), Testcontainers (para infraestrutura em testes).
- **E2E Tests (End-to-End)**: ~10% dos testes. Testam a aplicação completa do ponto de vista do usuário.
  - **Ferramentas**: Playwright (TypeScript).
- **Performance Tests**: Testes de carga e stress.
  - **Ferramentas**: k6, Locust.

### 11.2. Regras de Testes
- **Cobertura de Código**: Meta mínima de 80% de cobertura para código de domínio e aplicação.
- **Testes de Regressão**: Todo bug fix deve ter um teste de regressão associado.
- **Testes em CI/CD**: Todos os testes são executados automaticamente no pipeline CI/CD. Nenhum código é mergeado se os testes falharem.

---

## 12. Logs e Observabilidade

Observabilidade é a capacidade de entender o estado interno de um sistema a partir de suas saídas externas. A plataforma deve ser 100% observável.

### 12.1. Três Pilares da Observabilidade
1. **Logs**: Registros de eventos discretos (ex: "Usuário X autenticado com sucesso", "Erro ao conectar à exchange").
2. **Métricas**: Dados numéricos agregados ao longo do tempo (ex: taxa de erro da API, latência das requisições, uso de memória).
3. **Tracing**: Rastreamento de requisições através de múltiplos serviços (ex: uma requisição do frontend → API Gateway → API Principal → Worker → Exchange).

### 12.2. Logs
- **Ferramenta**: Serilog para logging estruturado.
- **Padrões**:
  - Logs estruturados em JSON (facilita indexação e busca).
  - Níveis de log: Debug, Information, Warning, Error, Fatal.
  - Incluir sempre:
    - Timestamp.
    - Service name.
    - Trace ID (para tracing distribuído).
    - User ID (quando aplicável).
    - Correlation ID (para correlacionar logs relacionados).
- **Armazenamento**: Elasticsearch + Kibana ou Grafana Loki.

### 12.3. Métricas
- **Ferramenta**: OpenTelemetry para instrumentação, Prometheus para coleta, Grafana para visualização.
- **Tipos de Métricas**:
  - **Counter**: Contador incremental (ex: número de requisições).
  - **Gauge**: Medidor que pode aumentar ou diminuir (ex: uso de CPU, número de conexões ativas).
  - **Histogram**: Distribuição de valores (ex: latência das requisições).
- **Métricas Obrigatórias**:
  - Taxa de erro da API (4xx e 5xx).
  - Latência das requisições (p50, p95, p99).
  - Uso de recursos (CPU, memória, disco).
  - Número de eventos processados pelo Kafka.
  - Latência de consultas ao banco de dados.

### 12.4. Tracing Distribuído
- **Ferramenta**: OpenTelemetry para instrumentação, Jaeger ou Grafana Tempo para armazenamento e visualização.
- **Padrões**:
  - Incluir Trace ID e Span ID em todos os logs e eventos.
  - Propagar contexto de tracing através de headers HTTP (W3C Trace Context).
  - Instrumentar todas as requisições externas (API, banco de dados, Kafka, exchanges).

---

## 13. Segurança

Segurança é a prioridade número um da plataforma. Qualquer vulnerabilidade pode resultar em perda financeira para os usuários, portanto todos os desenvolvedores devem seguir as regras de segurança rigorosamente.

### 13.1. Autenticação e Autorização
- **Autenticação**:
  - JWT com chaves assimétricas (RS256).
  - Senhas armazenadas com bcrypt (work factor alto).
  - Autenticação de dois fatores (2FA) obrigatória para usuários com acesso a live trading.
  - Refresh tokens rotativos.
- **Autorização**:
  - Princípio do mínimo privilégio (Least Privilege).
  - Verificação de autorização em cada endpoint da API.
  - Validação de ownership de recursos (ex: um usuário só pode modificar suas próprias estratégias).

### 13.2. Dados Sensíveis
- **Chaves de API das Exchanges**:
  - Armazenadas criptografadas em banco de dados (AES-256).
  - Chave de criptografia armazenada em um secrets manager (ex: AWS Secrets Manager, HashiCorp Vault), nunca em código ou config files.
- **Dados de Cartão de Crédito**: Não armazenamos dados de cartão de crédito (usamos gateways de pagamento tokenizados).
- **Logs**: Nunca logar dados sensíveis (senhas, chaves de API, dados pessoais).

### 13.3. Segurança da API
- **Rate Limiting**: Prevenir abusos e ataques DDoS.
- **CORS**: Configurar CORS adequadamente, permitindo apenas origens confiáveis.
- **Cabeçalhos de Segurança**: Incluir cabeçalhos de segurança em todas as respostas da API (HSTS, X-Content-Type-Options, X-Frame-Options, Content-Security-Policy).
- **Validação de Entrada**: Todas as entradas de usuário devem ser validadas e sanitizadas para prevenir injeções (SQLi, XSS, NoSQLi).
- **HTTPS**: Toda comunicação em trânsito deve ser via HTTPS/TLS 1.2+.

### 13.4. Segurança do Código
- **Scan de Vulnerabilidades**:
  - Scan de dependências (NuGet, npm) para vulnerabilidades conhecidas (ex: Snyk, Dependabot).
  - Scan de código estático (SAST) (ex: SonarQube).
- **Revisão de Código**: Todo código deve passar por revisão de pelo menos um outro desenvolvedor, com foco em segurança.

---

## 14. Performance e Escalabilidade

A plataforma deve lidar com alta carga de dados (milhões de dados de mercado por segundo) e muitos usuários simultâneos.

### 14.1. Escalabilidade Horizontal
- **API**: Escalar horizontalmente adicionando mais instâncias da API (load balancing).
- **Worker**: Escalar horizontalmente adicionando mais workers para processar tarefas em paralelo.
- **Kafka**: Escalar adicionando mais brokers e particionando tópicos.
- **PostgreSQL**: Read replicas para consultas de leitura, sharding para dados muito grandes.
- **Redis**: Clusterizar Redis para alta disponibilidade e escalabilidade.

### 14.2. Cache
- **Redis**: Cache de dados frequentes.
- **CDN**: Cache de assets frontend (imagens, CSS, JS).
- **Client-Side Cache**: React Query cacheia dados no cliente.

### 14.3. Otimizações de Performance
- **Backend**:
  - Async/Await em todas as operações de I/O (banco de dados, rede).
  - Pooling de conexões com banco de dados e Redis.
  - Pagination em consultas que retornam muitos dados.
- **Frontend**:
  - Lazy loading de componentes e rotas.
  - Otimização de imagens (WebP, AVIF).
  - Minificação e bundling de código.
  - Compressão (gzip, Brotli).

---

## 15. Deployment e DevOps

### 15.1. Infraestrutura como Código (IaC)
- **Ferramentas**: Terraform para provisionamento de infraestrutura, Kubernetes para orquestração de containers, Docker para containerização.
- **Padrões**:
  - Todo provisionamento é feito via IaC (nenhuma configuração manual).
  - IaC versionada no repositório.
  - Environments separados: Development, Staging, Production.

### 15.2. CI/CD Pipeline
- **Ferramenta**: GitHub Actions (configurado em `.github/workflows/`).
- **Estágios**:
  1. **Build**: Compila o código, instala dependências.
  2. **Testes**: Executa testes unitários e de integração.
  3. **Scan de Segurança**: Scan de vulnerabilidades em dependências e código.
  4. **Build de Imagens**: Cria imagens Docker.
  5. **Push de Imagens**: Envia imagens para registry (ex: Docker Hub, AWS ECR).
  6. **Deploy**: Deploy para Kubernetes (Staging automatico, Production manual).

### 15.3. Containerização
- **Docker**: Todas as aplicações são containerizadas com Docker.
- **Multi-stage builds**: Imagens Docker otimizadas (menor tamanho, sem dependências de desenvolvimento).
- **Imagens Base**: Usar imagens base oficiais e minimizadas (ex: `mcr.microsoft.com/dotnet/aspnet:9.0-alpine`, `node:20-alpine`).

---

## 16. Revisão de Código e Qualidade

### 16.1. Revisão de Código (Code Review)
Todo código novo ou modificado deve passar por revisão de pelo menos um outro desenvolvedor. Pontos a verificar na revisão:
- Cumprimento de padrões arquiteturais (Clean Architecture, DDD, SOLID).
- Testes adequados.
- Segurança.
- Performance.
- Legibilidade e manutenibilidade.
- Documentação.

### 16.2. Qualidade de Código
- **Padrões de Código**:
  - .NET: Código em conformidade com o C# Coding Guidelines.
  - TypeScript: Código em conformidade com o TypeScript Style Guide.
- **Linting**:
  - .NET: Roslyn Analyzers.
  - TypeScript: ESLint.
- **Formatação**:
  - .NET: dotnet format.
  - TypeScript: Prettier.
- **Ferramentas de Qualidade**: SonarQube para análise contínua de qualidade.

---

## 17. Regras e Restrições

### 17.1. Regras Obrigatórias
1. **Nunca Commit Secrets**: Nenhuma chave de API, senha, token, ou dado sensível pode ser commitado no repositório. Usar secrets managers.
2. **Nunca Modificar Migrações Existentes**: Migrações EF Core já aplicadas não podem ser modificadas. Sempre criar novas migrações.
3. **Nunca Quebrar Contratos de API**: Versão da API (ex: `/api/v1/`, `/api/v2/`) para mudanças incompatíveis.
4. **Nunca Ignorar Erros**: Todo erro deve ser tratado adequadamente (logado, usuário notificado, sistema recuperado).
5. **Todo Código Deve Ter Testes**: Como mencionado na seção de testes.

### 17.2. Restrições Tecnológicas
- **Bibliotecas**: Sempre avaliar cuidadosamente antes de adicionar uma nova biblioteca (verificar licença, popularidade, manutenção, vulnerabilidades).
- **Novas Tecnologias**: Qualquer nova tecnologia não presente na stack inicial deve ser aprovada pela equipe de arquitetura.

---

## 18. Fluxo de Desenvolvimento

### 18.1. Git Workflow
- **Branching Strategy**: Git Flow ou Trunk-Based Development com feature flags.
  - `main`: Código de produção.
  - `develop`: Código de desenvolvimento.
  - `feature/*`: Branches de features (mergeadas em develop via PR).
  - `hotfix/*`: Branches de hotfix (mergeadas em main e develop).
- **Commits**: Commits semânticos (ex: `feat: adicionar tela de backtesting`, `fix: corrigir erro de conexão com Binance`).
- **Pull Requests (PRs)**:
  - Cada PR resolve um único problema ou implementa uma única feature.
  - PRs devem ter descrição clara, testes, e passar por todos os checks do CI.
  - PRs devem ser aprovados por pelo menos um outro desenvolvedor antes do merge.

### 18.2. Processo de Desenvolvimento
1. **Escolha uma Tarefa**: Selecione uma tarefa da lista em `tasks/`.
2. **Crie uma Branch**: Crie uma branch feature a partir de develop.
3. **Implemente**: Implemente a tarefa seguindo todos os padrões e regras deste documento.
4. **Teste**: Escreva testes adequados e certifique-se de que todos passam.
5. **Commit**: Commita as mudanças com mensagem semântica.
6. **Abra um PR**: Abra um pull request para develop.
7. **Revisão**: Aguardar revisão de código e aprovação.
8. **Merge**: Merge o PR em develop após aprovação.

---

## Conclusão

Este documento é o guia definitivo para o desenvolvimento da Crypto AI Platform. Todas as decisões, implementações e revisões devem estar em conformidade com o que está aqui definido. Se houver qualquer dúvida ou ambiguidade, consulte a equipe de arquitetura antes de prosseguir.

Lembre-se: esta é uma plataforma enterprise para trading de criptomoedas. A qualidade, segurança e confiabilidade são não-negotiáveis. Sempre escreva código como se seu próprio dinheiro estivesse em jogo.
