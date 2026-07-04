# ARCHITECTURE_CONFORMANCE

Documento de verificação rápida de conformidade arquitetural para Task 045A.

- Padrões esperados: Clean Architecture, DDD, CQRS, SOLID, Multi-Tenant, Event Driven.
- Estado atual: infra e domínio compilam; application possui inconsistências que quebram CQRS/DI (DbContext e handlers). Correções propostas focam em alinhar contratos e não alterar regras de negócio.

Aproach: identificar divergências entre contratos (interfaces/DbContext) e consumidores, aplicar correções de compatibilidade (usings, aliases, adaptadores) e restabelecer DI consistente.
