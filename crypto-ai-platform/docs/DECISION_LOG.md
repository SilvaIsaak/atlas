# Log de Decisões Arquiteturais
**Plataforma**: Crypto AI Platform  
**Última Atualização**: 2026-06-25

---

## 1. Decisões

| ID | Data | Decisão | Justificativa | Autor | Aprovadores |
|----|------|---------|----------------|-------|--------------|
| D001 | 2026-06-25 | Usar Clean Architecture + DDD + CQRS + EDA | Separação de responsabilidades, testabilidade, manutenibilidade | Lead Architect | CTO |
| D002 | 2026-06-25 | Usar RabbitMQ como Event Bus na Phase 0 | Simplicidade, baixa latência, boa integração com .NET | Lead Architect | CTO |
| D003 | 2026-06-25 | Usar PostgreSQL + TimescaleDB para dados | Relacional robusto, otimizado para time-series | Lead Architect, Database Lead | CTO |
| D004 | 2026-06-25 | Usar Redis para cache e real-time | Baixa latência, Pub/Sub, estruturas de dados ricas | Lead Architect | CTO |
| D005 | 2026-06-25 | Usar S3/Azure Blob para cold storage | Escalável, barato, seguro | Lead Architect, DevOps Lead | CTO |
| D006 | 2026-06-25 | Política de Secrets Management: User Secrets (dev) + Vault (staging/prod) | Segurança, ambiente separado | Lead Architect, Security Lead | CTO |
| D007 | 2026-06-25 | Multi-tenant: Banco/Compartilhado, Schema/Compartilhado, RLS | Balanceio entre custo, simplicidade e isolamento | Lead Architect | CTO |
| D008 | 2026-06-25 | Observabilidade: OpenTelemetry + Grafana Stack | Padrão da indústria, low cost, completo | Lead Architect, DevOps Lead | CTO |
| D009 | 2026-06-25 | Phase 0: Implementar Quant Foundation antes de Trading | Base sólida para funcionalidades avançadas | Lead Architect, Quant Lead | CTO |
