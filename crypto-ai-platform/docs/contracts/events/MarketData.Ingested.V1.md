# Event Contract: MarketData.Ingested.V1
**Data**: 2026-06-25  
**Versão**: 1.0.0  
**Status**: Congelado

---

## 1. Schema
```json
{
  "type": "object",
  "required": [
    "eventId",
    "version",
    "correlationId",
    "causationId",
    "occurredAt",
    "tenantId",
    "idempotencyKey",
    "dataSourceId",
    "assetSymbol",
    "dataType",
    "ingestionJobId"
  ],
  "properties": {
    "eventId": {"type": "string", "format": "uuid"},
    "version": {"type": "object", "required": ["major", "minor", "patch"], "properties": {"major": {"type": "integer"}, "minor": {"type": "integer"}, "patch": {"type": "integer"}}},
    "correlationId": {"type": "string", "format": "uuid"},
    "causationId": {"type": "string", "format": "uuid", "nullable": true},
    "occurredAt": {"type": "string", "format": "date-time"},
    "tenantId": {"type": "string", "format": "uuid"},
    "idempotencyKey": {"type": "string", "format": "uuid"},
    "dataSourceId": {"type": "string", "format": "uuid"},
    "assetSymbol": {"type": "string"},
    "dataType": {"type": "string", "enum": ["OHLCV", "Trades", "OrderBook"]},
    "ingestionJobId": {"type": "string", "format": "uuid"}
  }
}
```

## 2. Exemplo de Payload
```json
{
  "eventId": "a1b2c3d4-e5f6-4a5b-9c8d-1f2e3a4b5c6d",
  "version": {"major": 1, "minor": 0, "patch": 0},
  "correlationId": "a1b2c3d4-e5f6-4a5b-9c8d-1f2e3a4b5c6d",
  "causationId": null,
  "occurredAt": "2026-06-25T12:00:00Z",
  "tenantId": "123e4567-e89b-12d3-a456-426614174000",
  "idempotencyKey": "a1b2c3d4-e5f6-4a5b-9c8d-1f2e3a4b5c6d",
  "dataSourceId": "123e4567-e89b-12d3-a456-426614174001",
  "assetSymbol": "BTCUSDT",
  "dataType": "OHLCV",
  "ingestionJobId": "123e4567-e89b-12d3-a456-426614174002"
}
```

## 3. Producers
- BinanceIngestionWorker
- Future exchange workers

## 4. Consumers
- FeatureCalculationWorker
- DataQualityCheckWorker
