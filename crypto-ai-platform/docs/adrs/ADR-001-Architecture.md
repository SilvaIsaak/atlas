# ADR-001: Arquitetura Geral do Sistema
**Status**: Aprovado  
**Data**: 2026-06-25  
**Decisores**: Lead Architect, CTO  
**Contexto**: Definir a arquitetura base para a Crypto AI Platform

---

## 1. Decisão
A plataforma seguirá uma **Clean Architecture + DDD + CQRS + Event-Driven Architecture** com as seguintes camadas:
- **Domain Layer**: Entidades, Value Objects, Agregados, Eventos de Domínio, Interfaces de Repositório
- **Application Layer**: Commands, Queries, Handlers, Behaviors
- **Infrastructure Layer**: Implementações concretas, serviços externos, integrações
- **Presentation Layer**: Controllers REST com versionamento

## 2. Justificativa
Essa arquitetura garante:
- Separação de responsabilidades
- Testabilidade
- Manutenibilidade
- Escalabilidade
- Evolução gradual do sistema

## 3. Alternativas Consideradas
- Arquitetura Monolítica Simples: Risco de acoplamento excessivo a longo prazo
- Microsserviços: Complexidade excessiva para estágio inicial
- Hexagonal Architecture: Similar a Clean Architecture, mas Clean Architecture foi escolhida por melhor alinhamento com a equipe

## 4. Consequências
- **Positivas**: Código organizado, testes fáceis de escrever, fácil evolução
- **Negativas**: Curva de aprendizado inicial, mais arquivos para gerenciar
