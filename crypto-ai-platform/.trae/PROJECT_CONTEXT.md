# Crypto AI Platform — Contexto do Projeto

## Objetivo

Este documento define o contexto completo do projeto **Crypto AI Platform**: a motivação para seu desenvolvimento, os objetivos de negócio, os stakeholders, a situação atual ("as-is"), a visão futura ("to-be"), os requisitos funcionais e não-funcionais, as restrições, as premissas, as dependências externas e as definições de terminologia.
Este documento é a fonte de verdade sobre o "por quê" do projeto e é a referência para todos os membros da equipe (desenvolvedores, arquitetos, product owners, design, QA, DevOps, etc.) e stakeholders.
**Todas as decisões arquiteturais, de design e de implementação devem ser alinhadas com este documento!**

## Índice

1. [Situação Atual (As-Is)](#1-situação-atual-as-is)
2. [Problema a Ser Resolvido](#2-problema-a-ser-resolvido)
3. [Objetivos de Negócio](#3-objetivos-de-negócio)
4. [Stakeholders](#4-stakeholders)
5. [Glossário de Termos do Negócio](#5-glossário-de-termos-do-negócio)
6. [Requisitos Funcionais (RF)](#6-requisitos-funcionais-rf)
7. [Requisitos Não-Funcionais (RNF)](#7-requisitos-não-funcionais-rnf)
8. [Restrições](#8-restrições)
9. [Premissas](#9-premissas)
10. [Dependências Externas](#10-dependências-externas)
11. [Riscos Iniciais e Mitigação](#11-riscos-iniciais-e-mitigação)

---

## 1. Situação Atual (As-Is)

Atualmente, a equipe responsável por operações quantitativas de criptomoedas enfrenta os seguintes desafios:
- **Falta de integração unificada**: As estratégias de trading são desenvolvidas em diferentes linguagens e frameworks (Python, JavaScript, C++), cada uma com seu próprio conjunto de ferramentas de backtesting e integração com exchanges, dificultando a manutenção e a governança.
- **Ausência de ambiente de validação**: A única forma de testar uma estratégia é com o dinheiro real na exchange, ou então com ferramentas de backtesting que não simulam condições de mercado reais (slippage, latência, comissões, etc.).
- **Gerenciamento de risco manual**: O gerenciamento de risco é feito de forma manual ou via scripts específicos, sem monitoramento em tempo real e sem capacidade de parada de emergência automatizada.
- **Falta de observabilidade**: Não há uma forma unificada de monitorar a performance das estratégias, os logs das execuções e os eventos do sistema.
- **Ausência de plataforma multi-tenant**: Não há forma de dar acesso a diferentes traders e equipes sem compartilhar credenciais de API das exchanges, o que representa um risco enorme de segurança.
- **Falta de ferramentas de pesquisa**: A pesquisa de novas estratégias é um processo manual, com coleta de dados de múltiplas fontes e análises que demoram dias para ser executadas.

---

## 2. Problema a Ser Resolvido

A Crypto AI Platform resolverá os seguintes problemas críticos:
| ID | Problema | Impacto |
|----|----------|---------|
| P001 | Não há uma plataforma unificada para desenvolver, testar, validar e executar estratégias de trading quantitativas de criptomoedas com AI/ML. | Alto |
| P002 | Não há mecanismo robusto de gerenciamento de risco automatizado para operações de live trading. | Alto |
| P003 | Não há forma segura de gerenciar chaves API de exchanges em uma equipe multi-tenant. | Alto |
| P004 | A observabilidade das estratégias e da plataforma é inexistente ou fragmentada em múltiplas ferramentas. | Médio |
| P005 | Ferramentas de pesquisa quantitativa existentes são lentas, caras ou não se integram com as ferramentas internas. | Médio |

---

## 3. Objetivos de Negócio

Os objetivos de negócio da Crypto AI Platform são, em ordem de prioridade:

### 3.1. Objetivo Principal
**OBJ-001**: Criar uma plataforma enterprise unificada para o desenvolvimento, validação e execução de estratégias de trading quantitativas de criptomoedas com suporte a AI/ML, que garanta segurança, escalabilidade, resiliência e observabilidade.

### 3.2. Objetivos Secundários
- **OBJ-002**: Reduzir em 90% o tempo de validação de estratégias através de backtesting e paper trading de alta performance.
- **OBJ-003**: Eliminar completamente a necessidade de compartilhar chaves API de exchanges entre membros da equipe através de uma camada segura de gerenciamento de chaves.
- **OBJ-004**: Implementar um sistema de gerenciamento de risco automatizado que reduza em 95% as perdas acidentais.
- **OBJ-005**: Criar uma ferramenta de pesquisa quantitativa integrada com dados de múltiplas fontes e suporte a AI/ML para otimização de estratégias.

### 3.3. Indicadores Chave de Desempenho (KPIs)
Os KPIs que serão usados para medir o sucesso do projeto são:
| ID | KPI | Meta |
|----|-----|------|
| KPI-001 | Tempo médio de backtesting de uma estratégia com 1 ano de dados de 1 minuto. | ≤ 5 minutos |
| KPI-002 | Tempo médio de incidentes de segurança relacionados à plataforma. | 0 incidentes / ano |
| KPI-003 | Tempo médio de detecção de violação de regra de risco. | ≤ 100 milissegundos |
| KPI-004 | Disponibilidade da plataforma em produção. | ≥ 99.9% (SLA de 3 nines) |
| KPI-005 | Número de estratégias ativas em live trading na plataforma. | ≥ 100 estratégias no primeiro ano |

---

## 4. Stakeholders

A tabela abaixo lista os stakeholders da Crypto AI Platform, suas funções e responsabilidades:
| Nome/Função | Papel | Responsabilidades |
|-------------|-------|--------------------|
| Chief Technology Officer (CTO) | Patrocinador Executivo | Aprovação da arquitetura, alocação de recursos, aprovação de gastos. |
| Arquiteto Chefe de Software | Líder Técnico | Definição e validação da arquitetura, validação de decisões técnicas, garantia da qualidade do código. |
| Product Owner (PO) | Líder de Produto | Definição e priorização do backlog do produto, alinhamento com stakeholders, validação de requisitos. |
| Equipe de Desenvolvimento Backend | Implementação | Implementação da API, workers, integração com exchanges, banco de dados, mensageria. |
| Equipe de Desenvolvimento Frontend | Implementação | Implementação da interface web, dashboard, integração com APIs. |
| Equipe de QA/Testes | Qualidade | Criação e execução de testes automatizados e manuais, validação dos requisitos, criação de planos de testes. |
| Equipe de DevOps/SRE | Infraestrutura e Operações | Criação de pipeline de CI/CD, provisionamento de infraestrutura, monitoramento e observabilidade, gerenciamento de incidentes. |
| Equipe de Segurança Cibernética | Segurança | Validação de segurança da plataforma, revisão de código para vulnerabilidades, compliance com regulamentações. |
| Equipe Quantitativa | Usuários Principais | Definição dos requisitos de negócio para a plataforma, validação das funcionalidades de backtesting, live trading e gerenciamento de risco. |
| Traders Autônomos | Usuários Finais | Uso da plataforma para trading, validação da usabilidade. |

---

## 5. Glossário de Termos do Negócio

É FUNDAMENTAL que todos os membros da equipe usem este glossário como Linguagem Ubíqua (Ubiquitous Language) do DDD!
| Termo | Definição |
|-------|-----------|
| Ativo (Asset) | Uma criptomoeda (ex: BTC, ETH, SOL) ou par de trading (ex: BTC/USDT, ETH/BRL). |
| Order Book (Livro de Ordens) | Uma lista de ordens de compra e venda de um ativo em uma exchange, organizados por preço e volume. |
| Tick | Uma atualização individual de dados de mercado (preço, volume, book de ordens). |
| Candle / OHLCV | Dados históricos agregados em um período de tempo determinado (1 minuto, 5 minutos, 1 hora, 1 dia) contendo: Open (preço de abertura), High (preço máximo), Low (preço mínimo), Close (preço de fechamento), Volume (volume negociado no período). |
| Estratégia (Strategy) | Um conjunto de regras de negócio e/ou modelos de AI/ML que definem quando comprar, vender ou manter um ativo. |
| Backtesting | O processo de simular a execução de uma estratégia usando dados históricos de mercado para verificar como teria se comportado no passado. |
| Paper Trading (Simulação) | O processo de simular a execução de uma estratégia usando dados de mercado em tempo real, mas com dinheiro virtual, para testar a estratégia em condições de mercado reais sem risco financeiro. |
| Live Trading | O processo de executar uma estratégia usando dinheiro real em uma exchange real. |
| Order (Ordem) | Uma instrução enviada a uma exchange para comprar ou vender um ativo: Market (preço de mercado), Limit (preço limite), Stop Loss, Take Profit, etc. |
| Position (Posição) | A quantidade de um ativo que uma estratégia ou usuário detém (long - comprado, short - vendido). |
| Drawdown | A redução percentual do valor de um portfólio desde o pico máximo. |
| Sharpe Ratio | Mede o retorno ajustado ao risco de uma estratégia, calculado como retorno excessivo dividido pelo desvio padrão. |
| Sortino Ratio | Semelhante ao Sharpe Ratio, mas usa apenas o desvio padrão das perdas (downside deviation). |
| Profit Factor | A razão entre o total de lucros e o total de perdas de uma estratégia. |
| Slippage (Desvio de Preço) | A diferença entre o preço esperado de uma ordem e o preço real de execução. |
| Latência | O tempo que uma mensagem ou ordem demora para ir de um ponto a outro. |
| Exchange | Uma plataforma para negociação de criptomoedas (Binance, Coinbase Pro, Kraken, etc.). |

---

## 6. Requisitos Funcionais (RF)

Os requisitos funcionais definem **o que o sistema deve fazer**. Eles são classificados por módulo da plataforma.

---

### 6.1. Módulo de Identidade e Acesso (Identity & Access)
| ID | Requisito | Critérios de Aceite |
|----|-----------|----------------------|
| RF-IA-001 | O sistema deve permitir a criação de usuários com e-mail e senha. | - Um usuário pode criar uma conta com e-mail e senha.<br>- A senha deve ser armazenada como hash seguro (bcrypt work factor ≥ 12).<br>- Um e-mail de verificação é enviado após a criação da conta. |
| RF-IA-002 | O sistema deve permitir autenticação via provedores OAuth2 (Google, GitHub, Azure AD). | - Um usuário pode se autenticar usando o Google.<br>- Um usuário pode se autenticar usando o GitHub.<br>- Um usuário pode se autenticar usando o Azure AD. |
| RF-IA-003 | O sistema deve exigir autenticação de dois fatores (2FA) obrigatória para acesso a funcionalidades de live trading e gerenciamento de chaves API. | - O usuário não pode acessar a página de chaves API sem ativar 2FA.<br>- O usuário não pode iniciar uma estratégia em live trading sem ativar 2FA. |
| RF-IA-004 | O sistema deve ter controle de acesso baseado em roles (RBAC - Role-Based Access Control). | - Roles existentes: Admin, Trader, Viewer.<br>- Admin pode gerenciar todos os usuários e configurações da plataforma.<br>- Trader pode criar estratégias, fazer backtesting, paper trading e live trading.<br>- Viewer só pode visualizar dashboards e relatórios. |

---

### 6.2. Módulo de Estratégias (Strategies)
| ID | Requisito | Critérios de Aceite |
|----|-----------|----------------------|
| RF-STR-001 | O sistema deve permitir a criação de estratégias usando linguagens como C#, Python e JavaScript/TypeScript. | - O usuário pode escrever código de estratégia em C#.<br>- O usuário pode escrever código de estratégia em Python.<br>- O usuário pode salvar versões de uma estratégia. |
| RF-STR-002 | O sistema deve fornecer uma biblioteca padrão de indicadores técnicos (RSI, MACD, Bollinger Bands, médias móveis, etc.). | - A biblioteca inclui RSI.<br>- A biblioteca inclui MACD.<br>- A biblioteca inclui Bollinger Bands.<br>- A biblioteca inclui SMA, EMA, WMA. |

---

### 6.3. Módulo de Backtesting
| ID | Requisito | Critérios de Aceite |
|----|-----------|----------------------|
| RF-BT-001 | O sistema deve permitir o backtesting de estratégias em dados históricos de múltiplas fontes. | - Backtesting em dados de Binance.<br>- Backtesting em dados de Coinbase Pro.<br>- Backtesting em dados de Kraken. |
| RF-BT-002 | O sistema deve simular condições reais durante o backtesting (slippage, comissões da exchange, latência). | - O usuário pode definir um valor percentual de slippage para simular.<br>- O usuário pode definir o valor das comissões da exchange.<br>- O usuário pode definir latência média de rede para simular. |
| RF-BT-003 | O sistema deve gerar um relatório detalhado de performance após o backtesting. | - O relatório inclui Total Return.<br>- O relatório inclui Sharpe Ratio.<br>- O relatório inclui Sortino Ratio.<br>- O relatório inclui Maximum Drawdown.<br>- O relatório inclui Profit Factor.<br>- O relatório inclui Win Rate. |

---

### 6.4. Módulo de Paper Trading
| ID | Requisito | Critérios de Aceite |
|----|-----------|----------------------|
| RF-PT-001 | O sistema deve permitir a execução de estratégias em papel trading com dados de mercado em tempo real e dinheiro virtual. | - O usuário pode configurar o saldo inicial do dinheiro virtual.<br>- As ordens são executadas usando o book de ordens real da exchange.<br>- O usuário pode ver o desempenho da estratégia em tempo real. |

---

### 6.5. Módulo de Live Trading
| ID | Requisito | Critérios de Aceite |
|----|-----------|----------------------|
| RF-LT-001 | O sistema deve permitir a execução de estratégias em live trading em múltiplas exchanges. | - Integração com Binance Spot.<br>- Integração com Coinbase Pro.<br>- Integração com Kraken Spot. |
| RF-LT-002 | O sistema deve permitir a pausa e o encerramento manual de estratégias em live trading. | - O usuário pode clicar em um botão "Pausar" para pausar a estratégia.<br>- O usuário pode clicar em um botão "Encerrar" para encerrar a estratégia e fechar todas as posições abertas. |

---

### 6.6. Módulo de Gerenciamento de Risco
| ID | Requisito | Critérios de Aceite |
|----|-----------|----------------------|
| RF-RISK-001 | O sistema deve permitir a definição de regras de risco por estratégia. | - O usuário pode definir um Maximum Drawdown percentual.<br>- O usuário pode definir um Max Position Size percentual.<br>- O usuário pode definir um Max Total Exposure percentual. |
| RF-RISK-002 | O sistema deve monitorar as regras de risco em tempo real durante o live trading e tomar ações automatizadas se as regras forem violadas. | - Se o drawdown máximo for atingido, a estratégia é pausada e todas as posições são fechadas.<br>- A ação é registrada em log e o usuário é notificado. |

---

### 6.7. Módulo de Gerenciamento de Chaves API
| ID | Requisito | Critérios de Aceite |
|----|-----------|----------------------|
| RF-KEY-001 | O sistema deve armazenar chaves API de exchanges de forma segura. | - As chaves API são criptografadas em repouso usando AES-256.<br>- As chaves API NÃO são armazenadas em texto claro em nenhum lugar.<br>- As chaves API NÃO são exibidas em texto claro na interface web (apenas os últimos 4 caracteres). |

---

### 6.8. Módulo de Observabilidade e Alertas
| ID | Requisito | Critérios de Aceite |
|----|-----------|----------------------|
| RF-OBS-001 | O sistema deve ter um dashboard de monitoramento em tempo real para todas as estratégias. | - O dashboard mostra o desempenho atual de todas as estratégias em live trading.<br>- O dashboard mostra o desempenho histórico de todas as estratégias. |
| RF-OBS-002 | O sistema deve enviar alertas para o usuário em caso de eventos importantes. | - Alerta por e-mail quando uma estratégia é pausada ou encerrada.<br>- Alerta por SMS quando uma regra de risco é violada.<br>- Alerta por Push Notification em dispositivos móveis (aplicativo iOS/Android). |

---

## 7. Requisitos Não-Funcionais (RNF)

Os requisitos não-funcionais definem **como o sistema deve se comportar** (performance, escalabilidade, segurança, disponibilidade, etc.).

---

### 7.1. Performance
| ID | Requisito | Validação |
|----|-----------|-----------|
| RNF-PERF-001 | A API deve responder a 95% das requisições em menos de 500ms. | Testes de performance usando k6 com 1000 usuários simultâneos. |
| RNF-PERF-002 | Um backtesting com 1 ano de dados de 1 minuto (≈525.600 candles) deve ser executado em menos de 5 minutos. | Teste de performance de backtesting usando dados históricos de BTC/USDT na Binance. |
| RNF-PERF-003 | O tempo de detecção de violação de regra de risco deve ser ≤ 100ms. | Teste de latência do módulo de risco usando dados em tempo real. |

---

### 7.2. Escalabilidade
| ID | Requisito |
|----|-----------|
| RNF-SCAL-001 | A plataforma deve suportar a adição de novas instâncias de API e workers horizontalmente, sem downtime. |
| RNF-SCAL-002 | O banco de dados PostgreSQL deve suportar read replicas para consultas de leitura (Queries CQRS). |
| RNF-SCAL-003 | O Kafka deve suportar particionamento de tópicos para paralelizar o consumo de eventos. |

---

### 7.3. Segurança
| ID | Requisito |
|----|-----------|
| RNF-SEC-001 | A plataforma deve ser OWASP Top 10 Compliant. |
| RNF-SEC-002 | Todo o tráfego de rede (interno e externo) deve ser criptografado usando TLS 1.3 (ou TLS 1.2 como fallback). |
| RNF-SEC-003 | As comunicações entre serviços na infraestrutura devem usar mTLS (mutual TLS). |
| RNF-SEC-004 | Senhas de usuários e chaves API de exchanges NÃO PODEM ser armazenadas em texto claro. |
| RNF-SEC-005 | O sistema deve ter rate limiting em todas as APIs públicas para prevenir abusos e ataques DDoS. |
| RNF-SEC-006 | Todo código deve passar por análise de segurança estática (SAST) e análise de vulnerabilidades em dependências (SCA). |

---

### 7.4. Disponibilidade
| ID | Requisito |
|----|-----------|
| RNF-AVAIL-001 | A plataforma deve ter um SLA (Service Level Agreement) de 99.9% de disponibilidade anual (downtime máximo de 8h46m/ano). |
| RNF-AVAIL-002 | O sistema deve ter Disaster Recovery (DR) com RTO (Recovery Time Objective) de ≤ 4 horas e RPO (Recovery Point Objective) de ≤ 15 minutos. |

---

### 7.5. Observabilidade
| ID | Requisito |
|----|-----------|
| RNF-OBS-001 | Todo componente da plataforma deve ter logs estruturados em JSON, com trace ID, span ID, user ID, service name, timestamp e nível de log. |
| RNF-OBS-002 | Todo componente deve emitir métricas para Prometheus. |
| RNF-OBS-003 | Todo componente deve ser instrumentado com OpenTelemetry para tracing distribuído. |

---

## 8. Restrições

As restrições são limitações que o projeto deve cumprir, e que não podem ser alteradas:
- **REST-001**: Linguagens de programação permitidas são C# 12+ para backend, TypeScript 5+ para frontend e Python 3.11+ para scripts de pesquisa. Nenhuma outra linguagem é permitida sem aprovação explícita do CTO.
- **REST-002**: O backend deve ser escrito usando .NET 9 LTS (Long Term Support).
- **REST-003**: O frontend deve ser escrito usando Next.js 14+ com App Router e React 18+.
- **REST-004**: O banco de dados relacional principal é PostgreSQL 16+.
- **REST-005**: O banco de dados de cache e filas é Redis 7+.
- **REST-006**: O broker de mensagens é Apache Kafka 3.7+ com KRaft (sem ZooKeeper).
- **REST-007**: O provisionamento de infraestrutura é feito via Terraform 1.7+.
- **REST-008**: A orquestração de containers é feita via Kubernetes 1.29+.
- **REST-009**: Nenhum serviço SaaS pago é permitido sem aprovação financeira prévia do CTO.

---

## 9. Premissas

As premissas são fatos que a equipe assume como verdadeiros para o planejamento e execução do projeto:
- **PREM-001**: As exchanges de criptomoedas (Binance, Coinbase Pro, Kraken) manterão suas APIs públicas disponíveis e sem mudanças breaking changes sem aviso prévio razoável.
- **PREM-002**: A infraestrutura cloud usada (AWS / Azure / GCP) terá disponibilidade de 99.99% para os serviços usados (EC2, EKS, RDS, etc.).
- **PREM-003**: A equipe de desenvolvimento terá todos os recursos necessários (hardware, software, acesso a ferramentas) disponíveis desde o primeiro dia.
- **PREM-004**: A equipe quantitativa fornecerá feedback contínuo durante o desenvolvimento para validação dos requisitos e das funcionalidades.

---

## 10. Dependências Externas

| ID | Dependência | Tipo | Notas |
|----|-------------|------|-------|
| DEP-001 | Binance API REST e WebSocket | Integração de terceiros | Usada para dados de mercado e ordens de trading. |
| DEP-002 | Coinbase Pro API REST e WebSocket | Integração de terceiros | Usada para dados de mercado e ordens de trading. |
| DEP-003 | Kraken API REST e WebSocket | Integração de terceiros | Usada para dados de mercado e ordens de trading. |
| DEP-004 | HashiCorp Vault (ou AWS Secrets Manager / Azure Key Vault) | Ferramenta de infraestrutura | Usada para gerenciamento seguro de segredos (senhas, chaves API, certificados). |
| DEP-005 | SendGrid (ou outro provedor de e-mail transacional) | Integração de terceiros | Usada para envio de e-mails de verificação, notificações, etc. |
| DEP-006 | Twilio (ou outro provedor de SMS) | Integração de terceiros | Usada para envio de alertas por SMS. |

---

## 11. Riscos Iniciais e Mitigação

| ID | Risco | Probabilidade | Impacto | Estratégia de Mitigação |
|----|-------|---------------|---------|---------------------------|
| RISK-001 | Uma exchange modifica sua API de forma breaking change, quebrando a integração. | Alta | Alto | - Usar uma camada de abstração (interface) para integração com exchanges, facilitando a adaptação a mudanças.<br>- Monitorar os repositórios de documentação das APIs das exchanges.<br>- Ter um conjunto de testes de integração automatizados que são executados periodicamente para detectar mudanças. |
| RISK-002 | Uma falha de segurança na plataforma leva ao vazamento de chaves API de exchanges, com consequente perda financeira para os usuários. | Baixa | Crítico | - Implementar criptografia de chaves API em repouso e em trânsito.<br>- Implementar RBAC e 2FA obrigatório.<br>- Realizar auditorias de segurança regulares (internas e externas).<br>- Ter um plano de resposta a incidentes bem definido. |
| RISK-003 | A performance do backtesting não atinge a meta de 5 minutos para 1 ano de dados de 1 minuto. | Médio | Médio | - Usar processamento paralelo/distribuído para backtesting.<br>- Otimizar consultas ao banco de dados com índices apropriados.<br>- Usar TimescaleDB (extensão do PostgreSQL) para dados de séries temporais. |
| RISK-004 | A plataforma sofre um ataque DDoS ou abuso de API. | Alta | Médio | - Implementar rate limiting em todas as APIs.<br>- Usar um WAF (Web Application Firewall).<br>- Usar um CDN (Cloudflare, AWS CloudFront) para proteger o frontend e a API. |
| RISK-005 | A equipe de desenvolvimento não tem experiência suficiente com as tecnologias escolhidas (.NET 9, Kafka, Kubernetes). | Médio | Médio | - Alocar orçamento para treinamentos e certificações da equipe.<br>- Contratar consultores especializados nas tecnologias para auxiliar nas fases iniciais. |

---

## Histórico de Versões

| Versão | Data | Autor | Descrição |
|--------|------|-------|-----------|
| v1.0 | 2026-06-24 | Equipe de Arquitetura | Versão inicial do documento. |
