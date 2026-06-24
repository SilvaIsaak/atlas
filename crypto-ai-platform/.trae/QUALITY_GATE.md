# Crypto AI Platform - Quality Gate
## Índice
1. [Pré-requisitos para Merge](#pré-requisitos-para-merge)
2. [Checklist](#checklist)

---

## Pré-requisitos para Merge
Um PR só pode ser mergeado se:
1. Build passa no CI
2. Todos os testes passam
3. Cobertura de código ≥ 80% para Domain e Application Layers
4. Sem vulnerabilidades críticas ou altas em dependências (Snyk/GitHub Dependabot)
5. Aprovado por todos os agentes necessários
6. Aprovado por pelo menos um humano (se aplicável)
7. Documentação atualizada

---

## Checklist
Antes de mergear, verifique:
- [ ] Build do CI passa
- [ ] Todos os testes passam
- [ ] Cobertura ≥ 80% (Domain/Application)
- [ ] Sem vulnerabilidades críticas/altas
- [ ] Arquitetura segue Clean Architecture/DDD
- [ ] Não há segredos no código
- [ ] Todos os guidelines seguidos (CODING_STANDARDS, SECURITY, DATABASE, etc.)
- [ ] Documentação atualizada
- [ ] Mensagens de commit seguem Conventional Commits
