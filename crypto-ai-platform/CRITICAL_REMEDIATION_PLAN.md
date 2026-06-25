# 📋 Plano de Correção Prioritário - Crypto AI Platform
**Data**: 2026-06-25  
**Baseado em**: FINAL_AUDIT_REPORT.md

---

## 🚀 Fase 1 – Segurança Crítica
**Objetivo**: Remover vulnerabilidades de segurança críticas que impedem o uso em ambientes não-desenvolvimento.
**Riscos**:
- Exposição de credenciais durante o desenvolvimento
- Ataques de DoS / Brute Force
- Uso não autorizado de tokens
**Dependências**: Nenhuma
**Esforço estimado**: 40h (2 semanas / 1 dev)

### Critérios de Aceite
1. Secrets gerenciados por vault (ex: Azure Key Vault / Docker Secrets)
2. Rate Limiting implementado em todos os endpoints
3. Refresh tokens armazenados no banco e invalidados após uso
4. CORS restrito para produção (apenas domínios autorizados)
5. AllowedHosts configurado para domínios específicos
6. Middleware CSRF implementado em endpoints state-changing
7. Hardening padrão (headers de segurança: X-Content-Type-Options, X-Frame-Options, etc.)

### Definição de Pronto
- ✅ CI/CD passa sem falhas de segurança
- ✅ Pen Test básico não encontra vulnerabilidades críticas
- ✅ Documentação de segurança atualizada

---

## 🧪 Fase 2 – Qualidade
**Objetivo**: Garantir que o código tenha testes básicos e qualidade mínima para evolução segura.
**Riscos**:
- Regressões não detectadas durante refatorações
- Cobertura insuficiente para validar funcionalidade
**Dependências**: Fase 1 concluída
**Esforço estimado**: 60h (3 semanas / 1 dev)

### Critérios de Aceite
1. Testes unitários para 100% das Domain Entities
2. Testes unitários para 70% dos Command/Query Handlers
3. Testes de integração para APIs principais (Auth, Strategies, Backtesting)
4. Cobertura mínima de 40% no SonarQube / Coverlet
5. Quality Gates implementados no CI/CD

### Definição de Pronto
- ✅ Todos os testes passam no CI/CD
- ✅ Cobertura mínima atingida e verificada
- ✅ Nenhum code smell crítico no SonarQube

---

## 🗃️ Fase 3 – Banco de Dados
**Objetivo**: Preparar o banco de dados para uso em staging e produção.
**Riscos**:
- Consultas lentas em ambiente com dados
- Perda de dados por falta de migrations
**Dependências**: Fase 2 concluída
**Esforço estimado**: 30h (1,5 semanas / 1 dev)

### Critérios de Aceite
1. Migrations criadas para todos os DbSets
2. Índices criados para campos de filtro frequentes (UserId, StrategyId, Status)
3. Otimizações de consultas (ex: SplitQuery, NoTracking)
4. Plano de backup automático configurado
5. Docker-compose com PostgreSQL e pgAdmin para desenvolvimento

### Definição de Pronto
- ✅ Migrations aplicadas sem erros no banco de desenvolvimento
- ✅ Explain Analyze mostra índices sendo usados em consultas frequentes
- ✅ Backup automático testado e restaurado com sucesso

---

## 📊 Fase 4 – Observabilidade
**Objetivo**: Garantir que a plataforma seja monitorável e que problemas sejam detectados rapidamente.
**Riscos**:
- Problemas em produção sem visibilidade
- Tempo alto de MTTR (Mean Time to Recovery)
**Dependências**: Fase 3 concluída
**Esforço estimado**: 25h (1 semana / 1 dev)

### Critérios de Aceite
1. Health Checks implementados para API, PostgreSQL e Redis
2. Alertas básicos configurados no Prometheus/Grafana (API down, alto uso de CPU)
3. Dashboards de monitoramento básicos (taxa de erros, latência, uso do banco)
4. Logs estruturados e sem dados sensíveis
5. Logs de auditoria para ações importantes (login, criação de ordens, mudança de status)

### Definição de Pronto
- ✅ Todos os health checks passam no Docker Compose
- ✅ Alertas de teste são acionados corretamente
- ✅ Logs de auditoria são gerados e armazenados

---

## 📈 Fase 5 – Trading Foundation
**Objetivo**: Implementar as bases para o trading quantitativo (integração de dados e engine de estratégias).
**Riscos**:
- Dados de mercado imprecisos
- Estratégias não executam como esperado
**Dependências**: Fase 4 concluída
**Esforço estimado**: 80h (4 semanas / 2 devs)

### Critérios de Aceite
1. Integração funcional com pelo menos uma exchange (ex: Binance Spot Testnet)
2. Download de dados históricos (OHLCV) para backtesting
3. Market Data em tempo real (WebSocket)
4. Strategy Engine funcional (execução de regras básicas)
5. Documentação de integração com exchanges

### Definição de Pronto
- ✅ Dados históricos são baixados e armazenados com sucesso
- ✅ Strategy Engine executa uma estratégia simples (ex: moving average crossover)
- ✅ Integração com exchange testada e funcional

---

## 📉 Fase 6 – Paper Trading
**Objetivo**: Implementar o ambiente de simulação de trading para validação de estratégias.
**Riscos**:
- Simulações imprecisas que não refletem o mercado real
- Métricas de performance erradas
**Dependências**: Fase 5 concluída
**Esforço estimado**: 60h (3 semanas / 1 dev)

### Critérios de Aceite
1. Simulação de ordens (Market, Limit)
2. Simulação de portfólio (capital inicial, P&L)
3. Cálculo de métricas (Sharpe, Max Drawdown, Win Rate)
4. Interface básica para criar e monitorar paper trades
5. Relatórios de performance em PDF/CSV

### Definição de Pronto
- ✅ Paper Trade é executado do início ao fim sem erros
- ✅ Métricas calculadas coincidem com resultados esperados
- ✅ Relatórios são gerados e baixados com sucesso

---

## 💰 Fase 7 – Live Trading Readiness
**Objetivo**: Preparar a plataforma para operar com dinheiro real de forma segura.
**Riscos**:
- Perda financeira por bugs no Execution Engine
- Violação de limites de risco
- Falta de logs para auditoria
**Dependências**: Fase 6 concluída
**Esforço estimado**: 100h (5 semanas / 2 devs)

### Critérios de Aceite
1. Risk Engine funcional (stop loss, take profit, limites de exposição)
2. Execution Engine funcional (integração com exchange real/testnet)
3. Circuit Breakers implementados para proteger contra falhas em cascata
4. Logs de auditoria completos para todas as ações de trading
5. Testes E2E para fluxo de Live Trading
6. Documentação de operação e runbooks para incidentes

### Definição de Pronto
- ✅ Live Trade é executado do início ao fim no testnet sem erros
- ✅ Circuit Breakers são acionados corretamente em caso de falhas
- ✅ Todos os logs de auditoria são gerados e armazenados por pelo menos 1 ano

---

## 📌 Nova Ordem de Execução das Tasks (Com Prioridade de Correção)

Abaixo está a ordem revisada das tasks do projeto, priorizando as correções identificadas na auditoria:

| Ordem | Task Original | Objetivo na Nova Ordem | Fase de Correção Associada |
|-------|----------------|--------------------------|----------------------------|
| **00** | **Security Hardening** | *Nova task* | Fase 1 |
| **01** | **Test Suite Foundation** | *Nova task* | Fase 2 |
| **02** | **Database Migrations & Indices** | *Nova task* | Fase 3 |
| **03** | **Observability & Health Checks** | *Nova task* | Fase 4 |
| **04** | **Exchange Integration Foundation** | Atualizar Task 07 (Exchange Integration) | Fase 5 |
| **05** | **Research Engine v2** | Atualizar Task 12 | Fase 5 |
| **06** | **Strategies v2** | Atualizar Task 13 | Fase 5 |
| **07** | **Backtesting Engine** | Atualizar Task 14 | Fase 5 / Fase 6 |
| **08** | **Walk Forward Analysis** | Atualizar Task 15 | Fase 6 |
| **09** | **Paper Trading Engine** | Atualizar Task 16 | Fase 6 |
| **10** | **Risk Engine** | Atualizar Task 17 | Fase 7 |
| **11** | **AI Decision** | Atualizar Task 18 | Fase 7 |
| **12** | **Execution Engine** | Atualizar Task 19 | Fase 7 |
| **13** | **Live Trading** | Atualizar Task 20 | Fase 7 |
| **14** | **Notifications** | Atualizar Task 21 | Fase 4 / Fase 7 |
| **15** | **Dashboard** | Atualizar Task 22 | Fase 4 |
| **16** | **Monitoring** | Atualizar Task 23 | Fase 4 |
| **17** | **Learning** | Atualizar Task 24 | Fase 2 (opcional) |
| **18** | **Deployment** | Atualizar Task 25 | Fase 4 / Fase 7 |
| **19** | **Mobile** | Atualizar Task 26 | Fase 6 (opcional) |
| **20** | **Reports** | Atualizar Task 27 | Fase 6 |
| **21** | **Admin** | Atualizar Task 28 | Fase 2 |
| **22** | **MultiTenant** | Atualizar Task 29 | Fase 7 (opcional) |
| **23** | **Production Readiness** | Atualizar Task 30 | Fase 7 |

---

## 📌 Resumo do Timeline Total
- **Fase 1-4**: 10 semanas (2,5 meses) – Prontidão para STAGING
- **Fase 5-7**: 12 semanas (3 meses) – Prontidão para PAPER TRADING
- **Total estimado**: 22 semanas (~5,5 meses) com 2 devs dedicados
