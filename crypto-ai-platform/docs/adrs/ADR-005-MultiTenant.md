# ADR-005: Estratégia de Multi-Tenant
**Status**: Aprovado  
**Data**: 2026-06-25  
**Decisores**: Lead Architect, CTO  
**Contexto**: Definir como isolar dados entre diferentes inquilinos

---

## 1. Decisão
- **Padrão**: Banco de Dados Compartilhado, Schema Compartilhado, Isolamento via TenantId
- **Isolamento**:
  - Todas as entidades têm `TenantId`
  - Row Level Security (RLS) no PostgreSQL/TimescaleDB para garantir que um inquilino não acesse dados de outro
  - Redis: Chaves prefixadas com `tenant:{TenantId}:`
  - Cold Storage: Diretórios separados por TenantId

## 2. Alternativas Consideradas
- Banco de Dados Separado por Inquilino: Muito caro e complexo para muitos inquilinos
- Schema Separado por Inquilino: Complexidade extra de migrações
- Opção escolhida: Balanceio entre custo, simplicidade e isolamento

## 3. Consequências
- **Positivas**: Baixo custo, fácil manutenção de migrações, bom isolamento com RLS
- **Negativas**: Menor isolamento que banco/schema separado
