# ADR-004: Estratégia de Segurança
**Status**: Aprovado  
**Data**: 2026-06-25  
**Decisores**: Lead Architect, CTO, Security Lead  
**Contexto**: Definir regras de segurança para a plataforma

---

## 1. Decisão
- **Secrets Management**:
  - Desenvolvimento: User Secrets/.NET + Docker Secrets
  - Staging/Produção: Azure Key Vault/AWS Secrets Manager (OBRIGATÓRIO)
  - Rotatividade: Chaves API - 90 dias, Senhas BD - 60 dias, JWT Secret - 30 dias
- **Criptografia**:
  - Exchange API Keys: AES-256-GCM (autenticado)
  - Dados em trânsito: TLS 1.3
  - Dados em repouso: Criptografia servidor-side (PostgreSQL, S3/Azure Blob)
- **Autenticação/Autorização**:
  - JWT para APIs
  - RBAC com políticas customizadas
  - Row Level Security (RLS) no PostgreSQL para multi-tenant
  - Rate Limiting por Tenant e Usuário
- **Auditoria**:
  - Todos os endpoints de escrita geram audit logs
  - Audit logs são imutáveis, retidos eternamente

## 2. Alternativas Consideradas
- OAuth2/OIDC para autenticação: Planejado para fases futuras
- HashiCorp Vault para secrets: Azure Key Vault/AWS Secrets Manager foram escolhidos por integração com provedores cloud preferidos

## 3. Consequências
- **Positivas**: Segurança em camadas, conformidade com boas práticas, auditoria completa
- **Negativas**: Complexidade extra para gerenciar secrets e criptografia
