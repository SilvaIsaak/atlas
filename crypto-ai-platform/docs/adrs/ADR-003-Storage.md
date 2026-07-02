# ADR-003: Estratégia de Armazenamento
**Status**: Aprovado  
**Data**: 2026-06-25  
**Decisores**: Lead Architect, CTO  
**Contexto**: Definir onde armazenar cada tipo de dado

---

## 1. Decisão
| Tipo de Dado | Tecnologia | Justificativa |
|---------------|-------------|----------------|
| Metadados, Identidade, Audit Logs, Entidades Não-Série Temporal | PostgreSQL | Relacional robusto, ACID, integração com EF Core |
| Dados de Série Temporal (OHLCV, Trades, Features, Spreads) | TimescaleDB (extensão do PostgreSQL) | Otimizado para time-series, compressão, retenção |
| Cache Distribuído, Dados em Tempo Real, Rate Limiting | Redis | Baixa latência, Pub/Sub, estruturas de dados ricas |
| Cold Storage (Dados Antigos, Artefatos, Pacotes de Reprodução) | S3/Azure Blob Storage | Escalável, barato, seguro |

## 2. Regras Adicionais
- **Redis**: Chaves prefixadas com `tenant:{TenantId}:{Module}:{Key}`
- **Cold Storage**: Dados particionados por `source/data_type/year/month/day/asset`
- **Migrations**: EF Core, idempotentes, backup pré-apply

## 3. Alternativas Consideradas
- InfluxDB para time-series: Menor integração com PostgreSQL, TimescaleDB foi escolhido para manter simplicidade
- MongoDB para metadados: Nenhum benefício claro sobre PostgreSQL para nossos casos de uso

## 4. Consequências
- **Positivas**: Desempenho otimizado para cada tipo de dado, escalabilidade, segurança
- **Negativas**: Múltiplas tecnologias para gerenciar
