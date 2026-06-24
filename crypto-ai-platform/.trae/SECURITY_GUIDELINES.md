# Crypto AI Platform - Security Guidelines

## Índice
1. [Princípios Básicos](#princípios-básicos)
2. [Autenticação e Autorização](#autenticação-e-autorização)
3. [Segurança de Senhas](#segurança-de-senhas)
4. [Armazenamento de Segredos](#armazenamento-de-segredos)
5. [Segurança de APIs](#segurança-de-apis)
6. [Segurança de Dados](#segurança-de-dados)
7. [Auditoria e Logging](#auditoria-e-logging)
8. [Segurança de Infraestrutura](#segurança-de-infraestrutura)
9. [Checklist de Segurança](#checklist-de-segurança)

---

## Princípios Básicos
- **Defense in Depth**: Segurança em múltiplas camadas
- **Least Privilege**: Usuários e serviços têm apenas as permissões necessárias
- **Never Trust Input**: Sempre validar e sanitizar entradas
- **Fail-Secure**: Sistema falha em estado seguro
- **Keep It Simple**: Simplicidade reduz surface de ataque

---

## Autenticação e Autorização
- Usar **ASP.NET Core Identity** para autenticação e gerenciamento de usuários
- Usar **JWT com chaves assimétricas (RS256)** (não chaves simétricas)
- Rotacionar chaves JWT regularmente
- Tokens JWT com vida útil curta (acesso: 15 min, refresh: 7 dias)
- Blacklist de tokens revogados via Redis
- 2FA/OTP obrigatório para todos os usuários (usar Google Authenticator ou similar)
- Autorização via **Roles + Policies** (não apenas roles hardcoded)
- Sempre verificar `User.Identity.IsAuthenticated` e `User.IsInRole()`/`AuthorizeAttribute`

---

## Segurança de Senhas
- Sempre usar `PasswordHasher<TUser>` do ASP.NET Core Identity (não implementar hash manualmente!)
- Algoritmo padrão: PBKDF2 com HMAC-SHA256, 100.000 iterações
- Forçar senhas fortes:
  - Mínimo 8 caracteres
  - Combinação de letras maiúsculas/minúsculas, números e símbolos
- Proibir senhas fracas (ex: 123456, password)
- Logar tentativas de login falhas (bloquear IP após X tentativas)

---

## Armazenamento de Segredos
- **Nunca commit segredos em arquivos no Git** (ex: appsettings.json, Dockerfile)
- Usar `User Secrets` no desenvolvimento (.NET)
- Usar **HashiCorp Vault** ou **Azure Key Vault** em produção
- Segredos de integração com exchanges (API keys) devem ser criptografados em repouso
- Rotacionar chaves de API das exchanges regularmente

---

## Segurança de APIs
- HSTS (HTTP Strict Transport Security) ativado em produção
- CORS configurado de forma restrita (permitir apenas o frontend oficial)
- Rate limiting para prevenir brute force e ataques DoS
- Validar e sanitizar TODAS as entradas (usar FluentValidation)
- Sanitizar outputs para prevenir XSS (especialmente no frontend)
- Usar anti-CSRF tokens para endpoints que alteram estado (apenas para frontend tradicional; não necessário para APIs JWT)

---

## Segurança de Dados
- Criptografia em trânsito: TLS 1.3 (sem versões antigas)
- Criptografia em repouso: PostgreSQL com Transparent Data Encryption (TDE) em produção
- Dados sensíveis (chaves de API de exchanges, cartões de crédito, etc.) criptografados com AES-256 em repouso
- Retenção de dados: Definir políticas de retenção e exclusão segura
- Backups criptografados

---

## Auditoria e Logging
- Logar todos os eventos de autenticação (login/logout/senha alterada)
- Logar todas as ações que alteram dados sensíveis
- Logs estruturados com Serilog (incluir timestamp, usuário, IP, trace ID)
- Logs sem dados sensíveis (não logar senhas, chaves de API, cartões, etc.)
- Usar OpenTelemetry para tracing distribuído

---

## Segurança de Infraestrutura
- Docker: Imagens base oficiais (não usar imagens não verificadas)
- Kubernetes: Network Policies, RBAC, segredos gerenciados
- Atualizar dependências regularmente (verificar vulnerabilidades com GitHub Dependabot, Snyk)
- Isolamento de serviços em redes separadas (em produção)
- Backups testados regularmente

---

## Checklist de Segurança
- [ ] HSTS ativado em produção
- [ ] CORS restrito
- [ ] Rate limiting configurado
- [ ] Todas as entradas validadas com FluentValidation
- [ ] Senhas com hash via ASP.NET Core Identity
- [ ] 2FA obrigatório para todos os usuários
- [ ] Segredos em Vault/Key Vault (não em código)
- [ ] Logs sem dados sensíveis
- [ ] Dependências atualizadas
- [ ] TLS 1.3 configurado
- [ ] Backups criptografados
