# Task 011: News Analysis

## Objetivo
Implementar módulo de análise de notícias para o Crypto AI Platform! Incluir integração com fontes de notícias, análise de sentimento e exibição de notícias relacionadas a ativos!

## Status
🚧 Em andamento

## Critérios de Aceite
- [ ] Criar entidades News e NewsAnalysis na Domain layer
- [ ] Criar interface INewsProvider para integração com fontes de notícias
- [ ] Implementar análise de sentimento básica (positivo, neutro, negativo)
- [ ] Criar Application layer queries/commands para buscar e analisar notícias
- [ ] Criar Presentation layer endpoints para notícias e análise de sentimento
- [ ] Integração com Market Data para associar notícias a ativos

## Detalhes da Implementação
### Domain
- Criar entidade News para representar uma notícia
- Criar entidade/value object NewsAnalysis para armazenar resultado de análise de sentimento
- Criar interface INewsProvider para definir como buscar notícias de fontes externas

### Application
- Criar queries para buscar notícias por ativo/exchange
- Criar commands para analisar sentimento de notícias

### Presentation
- Criar NewsController com endpoints para buscar notícias e análise de sentimento

## Arquivos Criados/Atualizados
- packages/domain/News/News.cs
- packages/domain/News/NewsAnalysis.cs
- packages/domain/News/INewsProvider.cs
- packages/application/News/GetNewsQuery.cs
- packages/application/News/AnalyzeNewsSentimentCommand.cs
- packages/presentation/Controllers/NewsController.cs
- tasks/011-News-Analysis.md

## Próximas Tarefas
- Task 012: Research Engine
