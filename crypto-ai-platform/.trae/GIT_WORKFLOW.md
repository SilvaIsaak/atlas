# Crypto AI Platform — Git Workflow

## Objetivo
Este documento define o fluxo de trabalho (workflow) Git obrigatório para todo o desenvolvimento da Crypto AI Platform. Seguir este workflow garante que o repositório permaneça organizado, que todas as mudanças passem por revisão de código, que a história do Git seja clara, e que deployments sejam seguros e auditáveis.

## Índice
1. [Estratégia de Branching](#1-estratégia-de-branching)
2. [Convenções de Commits Semânticos](#2-convenções-de-commits-semânticos)
3. [Abertura de Pull Requests (PRs)](#3-abertura-de-pull-requests-prs)
4. [Revisão de Código](#4-revisão-de-código)
5. [Merge de PRs](#5-merge-de-prs)
6. [Exemplo de Fluxo Completo](#6-exemplo-de-fluxo-completo)

---

## 1. Estratégia de Branching
Usamos uma estratégia de branching inspirada no Git Flow e adaptada para CI/CD:

### 1.1 Branches Principais
| Branch | Função | Vida Útil | Regras |
|--------|--------|-----------|--------|
| `main` | Código de produção. Tudo em `main` DEVE ser deployável para produção. | Perene | - Nenhum commit direto — só merge via PR aprovado.<br>- Todo merge em `main` dispara deployment para staging/produção (semi-automatico). |
| `develop` | Código de desenvolvimento — integração de features. Tudo em `develop` DEVE ser compilável e passar testes automatizados. | Perene | - Nenhum commit direto — só merge via PR aprovado.<br>- Features são mergeadas em `develop`. |

### 1.2 Branches de Feature
Para desenvolver novas funcionalidades ou tarefas:
- **Padrão de Nome**: `feature/<nome-da-feature>` ou `task/<id-da-tarefa>-breve-descricao`
- Origem: `develop`
- Destino: `develop`
- Exemplos:
  - `feature/criar-tela-de-backtesting`
  - `task/T001-configurar-autenticacao-jwt`
  - `bugfix/T042-corrigir-crash-quando-executa-ordem`

### 1.3 Branches de Hotfix
Para correções de bugs críticos em produção:
- **Padrão de Nome**: `hotfix/<id-do-bug>-breve-descricao`
- Origem: `main`
- Destino: `main` e `develop` (para que a correção também entre no fluxo de desenvolvimento)
- Exemplos:
  - `hotfix/B001-vazamento-de-chaves-api`
  - `hotfix/corrigir-drawdown-maximo-incorreto`

### 1.4 Branches de Release (Opcional para Versões)
Para preparar versões para produção (quando houver releases grandes):
- **Padrão de Nome**: `release/v0.1.0`
- Origem: `develop`
- Destino: `main` e `develop`

---

## 2. Convenções de Commits Semânticos
TODOS os commits DEVEM seguir a especificação Conventional Commits para manter a história do Git organizada e permitir gerar CHANGELOGs automaticamente.

### 2.1 Estrutura de um Commit Semântico
```
<type>[optional scope]: <description>

[optional body]

[optional footer(s)]
```

### 2.2 Tipos de Commit Permitidos
| Tipo | Descrição |
|------|-----------|
| `feat` | Nova funcionalidade para o usuário. |
| `fix` | Correção de bug para o usuário. |
| `docs` | Apenas mudanças em documentação. |
| `style` | Apenas mudanças que não afetam o significado do código (espaços em branco, formatação, ponto e vírgula, etc.). |
| `refactor` | Mudança de código que não corrige bug e não adiciona funcionalidade. |
| `perf` | Mudança de código que melhora performance. |
| `test` | Adiciona ou corrige testes. |
| `chore` | Mudança no processo de build, CI, ou ferramentas auxiliares. |

### 2.3 Escopos (Scopes) Opcionais
Escopos são palavras que indicam o módulo ou componente que o commit afeta, por exemplo:
- `feat(strategies): adicionar suporte a estratégias em Python`
- `fix(market-data): corrigir latência na atualização de preços`

### 2.4 Exemplos de Commits Válidos
- `feat: adicionar tela de criação de estratégias`
- `fix(strategies): corrigir cálculo do Sharpe Ratio`
- `docs: atualizar README com instruções de setup`
- `test(backend): adicionar testes unitários para StrategyBacktestService`
- `chore: atualizar dependências do .NET`

---

## 3. Abertura de Pull Requests (PRs)
TODAS as mudanças, independentemente de tamanho, DEVEM ser enviadas via Pull Request (PR).

### 3.1 Passos para Abertura de um PR
1. Crie uma branch de feature/hotfix a partir de `develop` (ou `main` para hotfix).
2. Implemente sua funcionalidade ou correção.
3. Certifique-se que TODOS os testes automatizados estão passando (CI/CD deve bloquear merge se os testes falharem).
4. Certifique-se que o código segue as regras de arquitetura, coding standards e qualidade.
5. Abra um PR no GitHub com as seguintes informações obrigatórias:

#### 3.2 Estrutura do Descrição do PR
```markdown
## Objetivo
Descrição clara e concisa do objetivo do PR (qual problema resolve ou qual funcionalidade adiciona).

## Tipo de Mudança
- [ ] Nova funcionalidade
- [ ] Correção de bug
- [ ] Refatoração
- [ ] Mudança em documentação
- [ ] Outro (especificar)

## Tarefas Relacionadas
Closes #T001 (número da tarefa no GitHub Projects/issue tracker, se houver).

## Checklist
- [ ] Eu segui as regras de arquitetura definidas em ARCHITECTURE_RULES.md
- [ ] Eu segui as regras de código definidas em CODING_STANDARDS.md
- [ ] Eu escrevi testes unitários para o código novo/modificado
- [ ] Todos os testes passam localmente
- [ ] Eu atualizei a documentação, se necessário
- [ ] Eu verifiquei que não há vazamento de segredos ou dados sensíveis no código

## Capturas de Tela (apenas para mudanças no frontend)
Adicione prints do antes e depois, se aplicável.
```

---

## 4. Revisão de Código
### 4.1 Quem Revisa?
- Todo PR DEVE ser revisado por pelo menos um outro membro da equipe com experiência na área da mudança.
- PRs que afetam a arquitetura ou segurança DEVEM ser revisados pelo Arquiteto Chefe ou pelo responsável da área.

### 4.2 O que Verificar na Revisão?
Use o documento REVIEW_CHECKLIST.md como guia — ele tem a lista completa de pontos a verificar!

---

## 5. Merge de PRs
### 5.1 Regras para Merge
- PR só pode ser mergeado se TODOS os checks do CI/CD estiverem passando.
- PR só pode ser mergeado se tiver pelo menos uma aprovação.
- NÃO USAR "Merge commit" — use "Squash and merge" ou "Rebase and merge".

#### 5.1.1 Squash and Merge (Padrão)
Squash and merge combina todos os commits da branch do PR em um único commit semântico, mantendo a história do `main` e `develop` limpa e organizada. Este é o método padrão para merge de PRs de feature e bugfix.

---

## 6. Exemplo de Fluxo Completo
Vamos ver um exemplo de como implementar uma feature "Adicionar Indicador RSI":
1. Você cria uma branch a partir de `develop`: `git checkout -b feature/adicionar-indicador-rsi`
2. Você implementa o código e os testes.
3. Você faz commits semânticos:
   - `feat(indicators): adicionar cálculo do RSI`
   - `test(indicators): adicionar testes unitários para RSI`
4. Você abre um PR de `feature/adicionar-indicador-rsi` para `develop`, preenchendo a descrição.
5. O CI/CD roda todos os testes, SonarQube, etc. — todos passam.
6. Um colega revisa o PR, faz um comentário sobre uma melhoria, você ajusta.
7. PR é aprovado!
8. Você mergeia o PR usando "Squash and merge" em `develop`.
9. Quando `develop` estiver pronto para release, mergeia em `main` e faz deploy.

---

## Histórico de Versões
| Versão | Data | Autor | Descrição |
|--------|------|-------|-----------|
| v1.0 | 2026-06-24 | Equipe de Arquitetura | Versão inicial do documento. |
