# Crypto AI Platform - Event Guidelines
## Índice
1. [Arquitetura de Eventos](#arquitetura-de-eventos)
2. [Padrões de Nomenclatura](#padrões-de-nomenclatura)
3. [Schema](#schema)
4. [Outbox Pattern](#outbox-pattern)

---

## Arquitetura de Eventos
- **Apache Kafka**: Para event streaming entre serviços
- **Confluent Schema Registry**: Para schemas Avro
- **Outbox Pattern**: Para garantir entrega at-least-once

---

## Padrões de Nomenclatura
- **Tópicos Kafka**: `{domínio}.{agregado}.{evento}.{versão}` (ex: `trading.strategy.created.v1`, `market.data.candle.updated.v1`)
- **Nome do Evento**: Classe PascalCase no C# (ex: `StrategyCreatedDomainEvent`, `MarketCandleUpdatedDomainEvent`)
- **Avro Schemas**: Mesmo nome do evento com sufixo `.avsc` (ex: `StrategyCreatedDomainEvent.avsc`)

---

## Schema
- Usar **Avro** para schemas (compatibilidade backwards/forwards)
- Schema Registry centralizado
- Cada evento tem um schema versionado

---

## Outbox Pattern
- Armazenar eventos em tabela `outbox_messages` no banco de dados antes de enviar para Kafka
- Processador de Outbox (worker) lê periodicamente a tabela, envia para Kafka e marca como processado
- Garante entrega at-least-once e idempotência
