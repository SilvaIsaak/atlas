# Crypto AI Platform - Development Workflow
## Índice
1. [Fluxo Geral](#fluxo-geral)
2. [Git Workflow](#git-workflow)
3. [Pull Request Process](#pull-request-process)

---

## Fluxo Geral
1. Pegar uma Task da lista em `tasks/`
2. Criar uma branch a partir de `develop` com o nome `feature/{descricao}` ou `fix/{descricao}`
3. Implementar a Task seguindo os guidelines e agentes
4. Rodar os testes e garantir que todos passem
5. Fazer commit seguindo Conventional Commits
6. Criar PR para a branch `develop`
7. Passar por revisão de código (agentes + humanos)
8. Mergear PR quando aprovado

---

## Git Workflow
Seguimos o **Git Flow**:
- `main`: Branch de produção (apenas código aprovado e testado)
- `develop`: Branch de desenvolvimento (integração de features)
- `feature/*`: Novas features
- `fix/*`: Correções de bugs
- `hotfix/*`: Correções emergenciais de produção

---

## Conventional Commits
Mensagens de commit seguem:
`{tipo}({escopo}): {descrição}`

Tipos:
- `feat`: Nova feature
- `fix`: Correção de bug
- `docs`: Apenas mudanças na documentação
- `style`: Ajustes de estilo (formatação, etc.)
- `refactor`: Refatoração de código (não adiciona feature/corrige bug)
- `test`: Ajustes em testes
- `chore`: Ajustes em build, config, etc.

Exemplos:
- `feat(auth): add 2FA with TOTP`
- `fix(backtesting): fix max drawdown calculation`
- `docs(readme): update getting started guide`

---

## Pull Request Process
1. PR deve ter uma descrição clara do que foi feito
2. PR deve ter tests unitários/integration para o código novo
3. PR deve passar pelo CI (build, tests)
4. PR deve ser revisado pelo:
   - ARCHITECT Agent
   - SECURITY Agent
   - DATABASE Agent (se envolver banco)
   - QA Agent (se envolver testes)
5. PR mergeado quando aprovado por todos
