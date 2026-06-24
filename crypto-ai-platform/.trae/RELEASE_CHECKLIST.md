# Crypto AI Platform - Release Checklist
## Índice
1. [Pré-Release](#pré-release)
2. [Release](#release)
3. [Pós-Release](#pós-release)

---

## Pré-Release
- [ ] Todos os PRs da release mergeados na branch `develop`
- [ ] Todos os testes passam na CI
- [ ] Cobertura de código ≥ 80%
- [ ] Sem vulnerabilidades críticas/altas
- [ ] Testes de penetração em ambiente staging
- [ ] Testes de performance em ambiente staging
- [ ] Testes E2E passam em ambiente staging
- [ ] Documentação atualizada
- [ ] CHANGELOG atualizado
- [ ] Versão do projeto atualizada (semver)

---

## Release
- [ ] Merge da branch `develop` na branch `main`
- [ ] Criar tag com a versão (ex: `v1.0.0`)
- [ ] Deploy para ambiente de produção
- [ ] Verificar monitoramento (Prometheus/Grafana)
- [ ] Verificar logs (Loki)
- [ ] Fazer smoke test em produção

---

## Pós-Release
- [ ] Aviso para usuários (se aplicável)
- [ ] Atualizar roadmap
- [ ] Revisar feedback dos usuários
- [ ] Planejar próxima release
