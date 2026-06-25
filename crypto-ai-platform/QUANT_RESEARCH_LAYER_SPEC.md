# 📊 Especificação Técnica – QUANT RESEARCH LAYER
**Plataforma**: Crypto AI Platform  
**Versão**: 1.0  
**Data**: 2026-06-25

---

## 🏗️ Visão Geral da Camada
A **QUANT RESEARCH LAYER** é uma expansão da Clean Architecture existente, focada na **validação científica de estratégias quantitativas**. Ela se integra aos módulos já existentes, adicionando capacidades avançadas de discovery, validação e otimização de alphas.

### Integração com a Arquitetura Atual
- **Domain Layer**: Novas entidades, value objects e agregados para os módulos de pesquisa.
- **Application Layer**: Commands/Queries para executar ações de pesquisa.
- **Infrastructure Layer**: Serviços para cálculos quantitativos, integração com bibliotecas (ex: MathNet.Numerics, Python.NET para integração com pandas/scikit-learn).
- **Presentation Layer**: Novos endpoints para as APIs de pesquisa.

---

## 📦 Módulos da QUANT RESEARCH LAYER

---

### 1. Feature Store
**Objetivo**: Armazenar, versionar e servir features (indicadores técnicos, fundamentais, on-chain) para pesquisa e backtesting de forma eficiente e reprodutível.

#### Arquitetura
- **Domain**: `Feature`, `FeatureVersion`, `FeatureSet`
- **Application**: `CreateFeatureCommand`, `GetFeatureQuery`, `ListFeaturesQuery`
- **Infrastructure**: `IFeatureStoreService`, integração com banco de dados time-series (ex: InfluxDB ou TimescaleDB)
- **Presentation**: Endpoints REST + WebSocket para streaming de features em tempo real

#### Dependências
- Base de dados time-series (TimescaleDB/InfluxDB)
- Integração com Market Data (módulo de dados históricos/realtime)

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `features` | Metadados das features (nome, descrição, tipo, fonte) |
| `feature_versions` | Versão de cada feature, com hash do código de cálculo |
| `feature_values` | Valores históricos das features, particionados por símbolo e data |
| `feature_sets` | Conjuntos de features pré-definidos para estratégias |

#### APIs
- `GET /api/v1/features`: Listar todas as features
- `GET /api/v1/features/{id}`: Obter detalhes de uma feature
- `POST /api/v1/features`: Criar nova feature
- `GET /api/v1/features/{id}/values`: Obter valores históricos de uma feature
- `POST /api/v1/feature-sets`: Criar conjunto de features

#### Métricas
- Latência de recuperação de features
- Taxa de sucesso de cálculo de features
- Uso de features por estratégias

#### Critérios de Aceite
1. Features são versionadas e não podem ser alteradas retroativamente
2. Recuperação de 1 ano de dados de 10 features para 100 símbolos em < 10 segundos
3. Features são calculadas automaticamente com base em market data
4. API retorna valores de features em formato CSV/Parquet/JSON

---

### 2. Signal Discovery
**Objetivo**: Automatizar a descoberta de sinais de trading (alphas) estatisticamente significativos a partir de dados históricos e features.

#### Arquitetura
- **Domain**: `Signal`, `SignalTest`, `DiscoveryJob`
- **Application**: `StartSignalDiscoveryCommand`, `GetSignalDiscoveryStatusQuery`
- **Infrastructure**: `ISignalDiscoveryEngine`, integração com algoritmos de ML/estatísticos (ex: regressão, random forests, redes neurais)
- **Presentation**: Endpoints para criar jobs de discovery e visualizar sinais encontrados

#### Dependências
- Feature Store
- Market Data Histórico
- Bibliotecas de ML (ex: ML.NET ou Python.NET com scikit-learn)

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `signal_discovery_jobs` | Jobs de discovery, com status e parâmetros |
| `discovered_signals` | Sinais encontrados, com métricas de significância |
| `signal_tests` | Resultados de backtests de sinais |

#### APIs
- `POST /api/v1/signal-discovery/jobs`: Iniciar job de discovery
- `GET /api/v1/signal-discovery/jobs/{id}`: Obter status do job
- `GET /api/v1/discovered-signals`: Listar sinais encontrados
- `GET /api/v1/discovered-signals/{id}`: Obter detalhes de um sinal

#### Métricas
- Sharpe Ratio dos sinais descobertos
- Win Rate dos sinais
- Time-to-discovery (tempo médio para encontrar sinais significativos)
- Taxa de falsos positivos

#### Critérios de Aceite
1. Encontra pelo menos 1 sinal estatisticamente significativo (p-valor < 0.05) por job
2. Permite configurar parâmetros de discovery (lookback, universe de símbolos, features)
3. Integra diretamente com o Strategy Engine para converter sinais em estratégias
4. Relatório de discovery gerado com métricas e visualizações

---

### 3. Factor Analysis
**Objetivo**: Analisar a exposição de estratégias/sinais a fatores de risco conhecidos (ex: momentum, value, carry, volatilidade) no mercado de criptomoedas.

#### Arquitetura
- **Domain**: `Factor`, `FactorExposure`, `FactorAnalysisJob`
- **Application**: `StartFactorAnalysisCommand`, `GetFactorAnalysisReportQuery`
- **Infrastructure**: `IFactorAnalysisService`, cálculo de regressão de returns da estratégia vs. fatores
- **Presentation**: Endpoints para executar análise e visualizar relatórios

#### Dependências
- Feature Store (para fatores)
- Backtest Engine (para returns das estratégias)

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `factors` | Definição dos fatores de risco |
| `factor_analysis_jobs` | Jobs de análise |
| `factor_exposures` | Exposição de cada estratégia/sinal a cada fator |
| `factor_reports` | Relatórios de análise de fatores |

#### APIs
- `POST /api/v1/factor-analysis/jobs`: Iniciar análise de fatores
- `GET /api/v1/factor-analysis/jobs/{id}`: Obter status e relatório
- `GET /api/v1/factors`: Listar fatores disponíveis
- `POST /api/v1/factors`: Criar novo fator customizado

#### Métricas
- R² da regressão (explicação dos returns por fatores)
- T-stat dos betas de cada fator
- Volatilidade atribuída a cada fator

#### Critérios de Aceite
1. Análise é executada em menos de 5 minutos para uma estratégia de 5 anos de dados
2. Relatório mostra decomposição de risco por fator
3. Permite comparar exposição de múltiplas estratégias
4. Integra com o Risk Engine para limitar exposição a fatores específicos

---

### 4. Monte Carlo Simulation
**Objetivo**: Realizar simulações de Monte Carlo para avaliar a robustez de estratégias sob diferentes cenários de mercado.

#### Arquitetura
- **Domain**: `MonteCarloJob`, `MonteCarloPath`, `MonteCarloResult`
- **Application**: `StartMonteCarloSimulationCommand`, `GetMonteCarloReportQuery`
- **Infrastructure**: `IMonteCarloService`, geração de caminhos sintéticos de preços (geometric Brownian motion, bootstrap)
- **Presentation**: Endpoints para executar simulações e visualizar distribuições de returns

#### Dependências
- Backtest Engine (para modelo de preços)
- Histórico de preços (para calibrar simulações)

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `monte_carlo_jobs` | Jobs de simulação, com parâmetros |
| `monte_carlo_paths` | Caminhos de preços gerados (armazenamento otimizado) |
| `monte_carlo_results` | Resultados agregados das simulações |

#### APIs
- `POST /api/v1/monte-carlo/jobs`: Iniciar simulação
- `GET /api/v1/monte-carlo/jobs/{id}`: Obter status e resultados
- `GET /api/v1/monte-carlo/jobs/{id}/paths`: Obter caminhos gerados (amostra)

#### Métricas
- VaR (Value at Risk) em diferentes percentis
- CVaR (Conditional VaR)
- Probabilidade de drawdown > X%
- Probabilidade de Sharpe Ratio < Y

#### Critérios de Aceite
1. Gera pelo menos 10.000 caminhos em menos de 10 minutos
2. Permite calibrar simulações com dados históricos
3. Relatório mostra distribuição de P&L, drawdown e Sharpe Ratio
4. Compara simulação com resultados do backtest out-of-sample

---

### 5. Parameter Optimization
**Objetivo**: Otimizar os parâmetros de estratégias de forma robusta (evitando overfitting), usando métodos como walk-forward, bayesian optimization ou grid search.

#### Arquitetura
- **Domain**: `OptimizationJob`, `ParameterSet`, `OptimizationResult`
- **Application**: `StartParameterOptimizationCommand`, `GetOptimizationReportQuery`
- **Infrastructure**: `IParameterOptimizationService`, integração com métodos de otimização (ex: Grid Search, Bayesian Optimization com Optuna via Python.NET)
- **Presentation**: Endpoints para configurar e executar otimizações

#### Dependências
- Backtest Engine
- Feature Store (se estratégia usar features parametrizadas)

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `optimization_jobs` | Jobs de otimização, com espaço de busca de parâmetros |
| `parameter_sets` | Conjuntos de parâmetros testados |
| `optimization_results` | Resultados da otimização, com ranking de parâmetros |

#### APIs
- `POST /api/v1/parameter-optimization/jobs`: Iniciar otimização
- `GET /api/v1/parameter-optimization/jobs/{id}`: Obter status e resultados
- `GET /api/v1/parameter-optimization/jobs/{id}/top-parameter-sets`: Obter melhores conjuntos de parâmetros

#### Métricas
- Sharpe Ratio out-of-sample dos parâmetros otimizados
- Robustez do pico (deterioração de performance no out-of-sample vs in-sample)
- Tempo de execução da otimização

#### Critérios de Aceite
1. Evita overfitting (usa walk-forward ou holdout set)
2. Permite definir espaço de busca para cada parâmetro (valores mínimos/máximos, passo)
3. Retorna os 10 melhores conjuntos de parâmetros, não só o melhor
4. Integra com o Strategy Engine para implantar parâmetros otimizados

---

### 6. Walk Forward Validation (Expansão)
**Objetivo**: Expandir o módulo básico de Walk Forward para suportar análise mais detalhada, incluindo rolling windows de diferentes tamanhos, anchor windows e métricas de robustez.

#### Arquitetura
- **Domain**: `WalkForwardJob`, `WalkForwardWindow` (expansão do existente), `WalkForwardMetrics`
- **Application**: `StartWalkForwardValidationCommand`, `GetWalkForwardReportQuery` (expansão)
- **Infrastructure**: `IWalkForwardEngine` (refatorar para suportar novos tipos de windows)
- **Presentation**: Endpoints expandidos para relatórios detalhados

#### Dependências
- Backtest Engine
- Parameter Optimization (opcional, para otimizar por janela)

#### Banco de Dados
- Expansão das tabelas existentes:
  - `walk_forward_windows`: Adicionar tipo de janela (rolling/anchored), tamanho de training/testing
  - `walk_forward_results`: Adicionar métricas por janela e de robustez

#### APIs
- Expansão dos endpoints existentes:
  - `POST /api/v1/walkforward` (adicionar parâmetros de tipo de janela)
  - `GET /api/v1/walkforward/{id}/robustness-report`: Obter relatório de robustez

#### Métricas
- Robustez T-Stat (razão entre Sharpe out-of-sample e in-sample)
- Drawdown por janela
- Número de janelas com retorno positivo
- Consistência de performance entre janelas

#### Critérios de Aceite
1. Suporta rolling e anchored walk-forward
2. Permite configurar diferentes tamanhos de training/testing windows
3. Relatório mostra performance por janela, não só agregada
4. Detecta overfitting (ex: performance in-sample muito melhor que out-of-sample)

---

### 7. Regime Detection
**Objetivo**: Detectar regimes de mercado (ex: alta volatilidade, baixa volatilidade, tendência alta/baixa, range) para adaptar estratégias automaticamente.

#### Arquitetura
- **Domain**: `MarketRegime`, `RegimeDetectionJob`, `RegimeRule`
- **Application**: `DetectRegimesCommand`, `GetCurrentRegimeQuery`
- **Infrastructure**: `IRegimeDetectionService`, algoritmos como Hidden Markov Models (HMM), K-Means, ou regras baseadas em volatilidade/indicadores
- **Presentation**: Endpoints para detectar regimes e visualizar regime atual

#### Dependências
- Feature Store (para features de regime: VIX, volatilidade realizada, RSI, etc.)
- Market Data em tempo real

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `market_regimes` | Definição dos regimes |
| `regime_detection_jobs` | Jobs de detecção de regimes históricos |
| `regime_history` | Histórico de regimes para cada símbolo |
| `regime_rules` | Regras para adaptar estratégias por regime |

#### APIs
- `POST /api/v1/regime-detection/jobs`: Detectar regimes históricos
- `GET /api/v1/regimes/{symbol}/current`: Obter regime atual para um símbolo
- `GET /api/v1/regimes/{symbol}/history`: Obter histórico de regimes
- `POST /api/v1/regime-rules`: Criar regra para adaptar estratégia por regime

#### Métricas
- Acurácia de detecção de regime (se comparada a um benchmark)
- Performance de estratégias adaptadas por regime vs não adaptadas
- Tempo médio de permanência em cada regime

#### Critérios de Aceite
1. Detecta pelo menos 3 regimes diferentes em dados históricos
2. Permite adicionar regimes customizados
3. Integra com o Strategy Engine para ativar/desativar estratégias por regime
4. Atualiza o regime atual em tempo real (a cada minuto)

---

### 8. Portfolio Optimization
**Objetivo**: Otimizar a alocação de capital entre múltiplas estratégias/símbolos usando métodos modernos de portfolio theory (ex: Markowitz, risk parity, minimum variance, max Sharpe).

#### Arquitetura
- **Domain**: `Portfolio`, `PortfolioOptimizationJob`, `Allocation`
- **Application**: `OptimizePortfolioCommand`, `GetPortfolioQuery`
- **Infrastructure**: `IPortfolioOptimizationService`, integração com algoritmos de otimização
- **Presentation**: Endpoints para otimizar e visualizar alocação de portfólio

#### Dependências
- Backtest Engine (para returns de estratégias)
- Risk Engine (para limites de alocação)

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `portfolios` | Portfólios criados pelo usuário |
| `portfolio_optimization_jobs` | Jobs de otimização |
| `allocations` | Alocação de capital por estratégia/símbolo |

#### APIs
- `POST /api/v1/portfolio-optimization/jobs`: Iniciar otimização de portfólio
- `GET /api/v1/portfolio-optimization/jobs/{id}`: Obter resultados
- `GET /api/v1/portfolios/{id}`: Obter portfólio otimizado
- `POST /api/v1/portfolios`: Criar novo portfólio

#### Métricas
- Sharpe Ratio do portfólio
- Volatilidade do portfólio
- Diversificação (ex: concentração de capital)
- Max Drawdown do portfólio

#### Critérios de Aceite
1. Suporta pelo menos 3 métodos de otimização (risk parity, min variance, max Sharpe)
2. Permite definir restrições (ex: alocação máxima por estratégia, mínimo de 3 estratégias)
3. Relatório mostra alocação atual e proposta, com comparação de métricas
4. Integra com o Execution Engine para rebalancear o portfólio automaticamente (opcional)

---

### 9. Strategy Ranking
**Objetivo**: Rankear estratégias com base em métricas combinadas (Sharpe, Max Drawdown, Win Rate, robustez walk-forward) para ajudar os usuários a escolher as melhores.

#### Arquitetura
- **Domain**: `StrategyRanking`, `RankingCriteria`
- **Application**: `RankStrategiesCommand`, `GetStrategyRankingsQuery`
- **Infrastructure**: `IStrategyRankingService`, cálculo de scores combinados
- **Presentation**: Endpoints para rankear estratégias e visualizar rankings

#### Dependências
- Backtest Engine
- Walk Forward Validation
- Factor Analysis

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `ranking_criteria` | Critérios de ranking definidos pelo usuário/admin |
| `strategy_rankings` | Rankings gerados, com scores de cada estratégia |

#### APIs
- `POST /api/v1/strategy-rankings`: Gerar ranking de estratégias
- `GET /api/v1/strategy-rankings`: Listar rankings gerados
- `GET /api/v1/strategy-rankings/{id}`: Obter detalhes de um ranking
- `POST /api/v1/ranking-criteria`: Criar critérios de ranking customizados

#### Métricas
- Score combinado (média ponderada ou score Z)
- Consistência do ranking em diferentes períodos
- Performance out-of-sample das estratégias top-ranked

#### Critérios de Aceite
1. Permite definir pesos para cada métrica no ranking
2. Rankea estratégias em menos de 1 minuto para 100 estratégias
3. Mostra comparação lado a lado das estratégias top-ranked
4. Permite salvar rankings para acompanhamento posterior

---

### 10. Research Notebook
**Objetivo**: Fornecer um ambiente de notebook (similar ao Jupyter) integrado à plataforma para pesquisa quantitativa customizada.

#### Arquitetura
- **Domain**: `Notebook`, `NotebookCell`, `NotebookOutput`
- **Application**: `CreateNotebookCommand`, `ExecuteNotebookCellCommand`, `GetNotebookQuery`
- **Infrastructure**: `INotebookService`, integração com Jupyter Kernel (via Jupyter HTTP API) ou similar para C#/Python
- **Presentation**: Endpoints REST para gerenciar notebooks e UI de notebook integrada ao frontend

#### Dependências
- Feature Store
- Backtest Engine
- Todos os outros módulos da QUANT RESEARCH LAYER

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `notebooks` | Notebooks criados pelos usuários |
| `notebook_cells` | Células dos notebooks (código, texto) |
| `notebook_outputs` | Saídas das células (textos, gráficos, tabelas) |

#### APIs
- `POST /api/v1/notebooks`: Criar novo notebook
- `GET /api/v1/notebooks`: Listar notebooks do usuário
- `GET /api/v1/notebooks/{id}`: Obter notebook completo
- `POST /api/v1/notebooks/{id}/cells`: Adicionar célula
- `POST /api/v1/notebooks/{id}/cells/{cellId}/execute`: Executar célula
- `DELETE /api/v1/notebooks/{id}`: Excluir notebook

#### Métricas
- Tempo médio de execução de células
- Taxa de sucesso de execução
- Número de notebooks criados/executados por usuário

#### Critérios de Aceite
1. Suporta células de código C# e Python
2. Integra com todos os módulos da QUANT RESEARCH LAYER (acessa Feature Store, executa backtests, etc.)
3. Permite salvar e compartilhar notebooks
4. Renderiza gráficos e visualizações diretamente no notebook

---

### 11. Strategy Comparison
**Objetivo**: Permitir comparação lado a lado de múltiplas estratégias usando métricas de performance, exposição a fatores, drawdowns, etc.

#### Arquitetura
- **Domain**: `StrategyComparison`, `ComparisonReport`
- **Application**: `CreateStrategyComparisonCommand`, `GetStrategyComparisonReportQuery`
- **Infrastructure**: `IStrategyComparisonService`, geração de relatórios comparativos
- **Presentation**: Endpoints para criar comparações e visualizar relatórios

#### Dependências
- Backtest Engine
- Walk Forward Validation
- Factor Analysis

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `strategy_comparisons` | Comparações criadas pelos usuários |
| `comparison_reports` | Relatórios de comparação gerados |

#### APIs
- `POST /api/v1/strategy-comparisons`: Criar comparação de estratégias
- `GET /api/v1/strategy-comparisons`: Listar comparações
- `GET /api/v1/strategy-comparisons/{id}`: Obter relatório de comparação

#### Métricas
- Mesmas métricas de performance, plus métricas de comparação (ex: diferença de Sharpe, correlação entre estratégias)

#### Critérios de Aceite
1. Compara pelo menos 5 estratégias em simultâneo
2. Inclui gráficos comparativos (equity curve, drawdown, returns mensais)
3. Mostra correlação entre as estratégias
4. Permite exportar comparação em PDF/CSV

---

### 12. Alpha Validation
**Objetivo**: Validar alphas/sinais usando métodos rigorosos (ex: out-of-sample testing, testes de hipótese, decaimento de alpha) para garantir que não são resultado de overfitting.

#### Arquitetura
- **Domain**: `AlphaValidationJob`, `AlphaValidationResult`
- **Application**: `ValidateAlphaCommand`, `GetAlphaValidationReportQuery`
- **Infrastructure**: `IAlphaValidationService`, execução de testes de hipótese, decaimento de alpha
- **Presentation**: Endpoints para validar alphas e visualizar relatórios

#### Dependências
- Feature Store
- Backtest Engine
- Signal Discovery

#### Banco de Dados
| Tabela/Collection | Descrição |
|--------------------|-----------|
| `alpha_validation_jobs` | Jobs de validação |
| `alpha_validation_results` | Resultados da validação |

#### APIs
- `POST /api/v1/alpha-validation/jobs`: Iniciar validação de alpha
- `GET /api/v1/alpha-validation/jobs/{id}`: Obter status e relatório
- `GET /api/v1/alpha-validation/jobs/{id}/decay-chart`: Obter gráfico de decaimento de alpha

#### Métricas
- Decaimento de alpha (performance ao longo do tempo)
- P-valor do teste de hipótese
- T-stat do retorno do alpha
- Taxa de sucesso em diferentes regimes de mercado

#### Critérios de Aceite
1. Executa validação completa em menos de 10 minutos
2. Relatório mostra decaimento de alpha ao longo do tempo
3. Inclui testes de hipótese para confirmar significância estatística
4. Recomenda se o alpha é "válido" ou "provavelmente overfitting"

---

## 📅 Roadmap Geral Atualizado

Integramos a **QUANT RESEARCH LAYER** ao plano de correção prioritário anterior:

| Fase Original | Módulos de Pesquisa Adicionados | Timeline Estimada |
|----------------|-----------------------------------|--------------------|
| Fase 4 (Observabilidade) | - | Mantida: 1 semana |
| Fase 5 (Trading Foundation) | Feature Store, Signal Discovery (básico) | Adiciona +4 semanas (total: 8 semanas) |
| Fase 6 (Paper Trading) | Factor Analysis, Monte Carlo Simulation, Parameter Optimization, Walk Forward Validation (expandido), Regime Detection, Strategy Comparison | Adiciona +6 semanas (total: 9 semanas) |
| Fase 7 (Live Trading Readiness) | Portfolio Optimization, Strategy Ranking, Alpha Validation | Adiciona +5 semanas (total: 10 semanas) |
| **Nova Fase 8** | Research Notebook | 4 semanas (após Fase 7) |

---

## 📌 Resumo Total Atualizado
- **Prontidão para Staging**: 10 semanas (~2,5 meses)
- **Prontidão para Paper Trading com Pesquisa Básica**: 24 semanas (~6 meses)
- **Prontidão para Paper Trading com Pesquisa Avançada**: 30 semanas (~7,5 meses)
- **Prontidão para Live Trading com Pesquisa**: 34 semanas (~8,5 meses)
