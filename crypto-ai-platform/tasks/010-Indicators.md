# Task 010: Indicators

## Objetivo
Implementar biblioteca de indicadores técnicos para o Crypto AI Platform! Incluir indicadores básicos (SMA, EMA, RSI, MACD, Bollinger Bands) e APIs para calculá-los a partir de klines!

## Status
🚧 Em andamento

## Critérios de Aceite
- [x] Criar Domain layer para indicadores (interfaces, value objects)
- [x] Implementar indicadores core: SMA, EMA, RSI, MACD, Bollinger Bands
- [x] Criar Application layer queries para cálculo de indicadores
- [x] Criar Presentation layer endpoints para indicadores
- [x] Integração com Market Data (klines) para cálculo
- [ ] Testes unitários para indicadores

## Detalhes da Implementação
### Domain
- Criada interface `ITechnicalIndicator` para definição de um indicador técnico
- Criados value objects para resultados dos indicadores
- Implementados indicadores core na camada Domain como Domain Services

### Application
- Criados queries e handlers para cálculo de cada indicador:
  - CalculateSmaQuery
  - CalculateEmaQuery
  - CalculateRsiQuery
  - CalculateMacdQuery
  - CalculateBollingerBandsQuery
- Os handlers usam o Market Data existente (klines) para cálculo

### Presentation
- Criado `IndicatorsController` com endpoints para cálculo de cada indicador

## Arquivos Criados/Atualizados
- packages/domain/Indicators/ITechnicalIndicator.cs
- packages/domain/Indicators/SmaIndicator.cs
- packages/domain/Indicators/EmaIndicator.cs
- packages/domain/Indicators/RsiIndicator.cs
- packages/domain/Indicators/MacdIndicator.cs
- packages/domain/Indicators/BollingerBandsIndicator.cs
- packages/application/Indicators/CalculateSmaQuery.cs
- packages/application/Indicators/CalculateEmaQuery.cs
- packages/application/Indicators/CalculateRsiQuery.cs
- packages/application/Indicators/CalculateMacdQuery.cs
- packages/application/Indicators/CalculateBollingerBandsQuery.cs
- packages/presentation/Controllers/IndicatorsController.cs
- tasks/010-Indicators.md

## Próximas Tarefas
- Task 011: News Analysis
