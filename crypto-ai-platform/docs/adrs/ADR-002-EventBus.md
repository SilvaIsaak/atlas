# ADR-002: Event Bus e Mensageria
**Status**: Aprovado  
**Data**: 2026-06-25  
**Decisores**: Lead Architect, CTO  
**Contexto**: Definir o sistema de mensageria para comunicação entre módulos

---

## 1. Decisão
- **Event Bus Principal**: RabbitMQ para Phase 0 (simplicidade, baixa latência para ambiente inicial)
- **Patterns**: Outbox Pattern (garantir entrega at-least-once), Inbox Pattern (garantir processamento exactly-once)
- **Idempotência**: Todas as mensagens têm IdempotencyKey único
- **Versionamento**: SemVer para eventos, backward compatibility obrigatória

## 2. Justificativa
- RabbitMQ é fácil de configurar e usar em desenvolvimento
- Outbox/Inbox Patterns solucionam problemas de distribuição de eventos sem transações
- Versionamento evita que alterações quebrem consumers

## 3. Alternativas Consideradas
- Kafka: Escalabilidade maior, mas complexidade excessiva para Phase 0 (mantido como opção para futuras fases)
- Azure Service Bus/AWS SQS: Lock-in de fornecedor, evitado para Phase 0

## 4. Consequências
- **Positivas**: Comunicação assíncrona segura, idempotente, versionada
- **Negativas**: Manutenção do RabbitMQ, overhead do Outbox/Inbox Patterns
