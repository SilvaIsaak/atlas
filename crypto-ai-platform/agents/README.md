# Crypto AI Platform — Framework de Desenvolvimento Orientado por IA
Este diretório contém os agentes especializados que compõem o **Framework de Desenvolvimento Orientado por IA** da Crypto AI Platform. Cada agente tem uma função específica e segue regras estritas para garantir que o projeto seja desenvolvido com qualidade, segurança e seguindo a arquitetura definida.

## Visão Geral do Framework
O framework é composto por agentes especializados que colaboram para implementar a plataforma. Os agentes são:

| Agente | Arquivo | Função |
|--------|---------|--------|
| CTO | `CTO.md` | Supervisor geral, alinhamento com objetivos de negócio, aprovação de decisões importantes. |
| ARCHITECT | `ARCHITECT.md` | Validação de arquitetura, garantia de Clean Architecture/DDD/CQRS/SOLID. |
| BACKEND_AGENT | `BACKEND_AGENT.md` | Desenvolvimento backend usando .NET 9, ASP.NET Core, EF Core, MediatR, etc. |
| FRONTEND_AGENT | `FRONTEND_AGENT.md` | Desenvolvimento frontend usando Next.js 14, React, TypeScript, Tailwind CSS, shadcn/ui. |
| DATABASE_AGENT | `DATABASE_AGENT.md` | Design e gerenciamento de banco de dados (PostgreSQL + TimescaleDB, Redis). |
| SECURITY_AGENT | `SECURITY_AGENT.md` | Validação de segurança, prevenção de vulnerabilidades. |
| DEVOPS_AGENT | `DEVOPS_AGENT.md` | Infraestrutura, CI/CD, Docker, Kubernetes, Terraform. |
| QA_AGENT | `QA_AGENT.md` | Qualidade, testes, cobertura de código, validação de requisitos. |
| DESIGN_AGENT | `DESIGN_AGENT.md` | Design system, UX/UI. |
| QUANT_AGENT | `QUANT_AGENT.md` | Lógica quantitativa, indicadores, backtesting. |
| STRATEGY_AGENT | `STRATEGY_AGENT.md` | Implementação de estratégias de negociação. |
| EXECUTION_AGENT | `EXECUTION_AGENT.md` | Execução de ordens em exchanges. |
| RISK_AGENT | `RISK_AGENT.md` | Gerenciamento de risco, validação de regras de risco. |
| MARKET_AGENT | `MARKET_AGENT.md` | Integração com dados de mercado de exchanges. |
| MONITORING_AGENT | `MONITORING_AGENT.md` | Observabilidade, métricas, logs, tracing. |
| AUDIT_AGENT | `AUDIT_AGENT.md` | Auditoria de código, conformidade. |
| LEARNING_AGENT | `LEARNING_AGENT.md` | Otimização de estratégias com machine learning. |
| SUPERVISOR_AGENT | `SUPERVISOR_AGENT.md` | Supervisor dos agentes, coordenação de tarefas. |

## Estrutura de Cada Agente
Cada arquivo de agente contém as seguintes seções (todas obrigatórias):
1. **Contexto**: Qual o papel do agente e qual o escopo do projeto?
2. **Memória**: Pontos importantes que o agente deve sempre lembrar.
3. **Objetivos**: Quais são os objetivos do agente?
4. **Restrições**: Quais são as regras que o agente NÃO PODE quebrar?
5. **Ferramentas**: Quais ferramentas o agente tem acesso?
6. **Fluxo de Decisão**: Qual o passo a passo que o agente deve seguir para realizar uma tarefa?
7. **Critérios de Revisão**: Quais pontos o agente deve verificar ao revisar algo?
8. **Prompt Role**: O prompt que define o papel do agente (para usar diretamente em LLMs).
9. **Exemplos**: Exemplos de tarefas executadas corretamente e incorretamente.
10. **Anti-padrões**: Quais práticas o agente deve evitar?
11. **Checklist**: Lista de verificação antes de finalizar uma tarefa.
12. **Histórico de Versões**: Registro de alterações no agente.

## Como Usar o Framework
Para usar o framework para implementar uma tarefa:
1. **Escolha o Agente Correto**: Use o agente especializado na tarefa (ex: BACKEND_AGENT para backend, FRONTEND_AGENT para frontend).
2. **Leia a Documentação do Agente**: Leia todo o arquivo do agente para entender suas regras e fluxo.
3. **Use o Prompt Role**: Copie o conteúdo da seção "Prompt Role" do agente e use como prompt para seu LLM.
4. **Siga o Fluxo de Decisão**: Siga o passo a passo definido na seção "Fluxo de Decisão".
5. **Use o Checklist**: Verifique todos os itens do checklist antes de finalizar.
6. **Consulte Outros Agentes**: Quando necessário, chame outros agentes especializados (ex: consulte o ARCHITECT Agent para validação de arquitetura).

## Principais Regras do Framework
1. **Nenhuma Arquitetura Inventada**: Todas as implementações devem seguir a arquitetura definida (Clean Architecture, DDD, CQRS, SOLID).
2. **Validação Cruzada**: Sempre que possível, valide com outros agentes (ex: BACKEND_AGENT valida com ARCHITECT Agent).
3. **Checklists Obrigatórias**: Sempre use os checklists dos agentes antes de finalizar uma tarefa.
4. **Documentação Sempre Atualizada**: Sempre atualize a documentação quando fizer mudanças.
5. **Segurança Primeiro**: Sempre priorize a segurança (nenhum segredo em código, validação de entradas, etc.).

## Histórico de Versões
| Versão | Data | Autor | Descrição |
|--------|------|-------|-----------|
| v1.0 | 2026-06-24 | Equipe de Arquitetura | Versão inicial do framework com os principais agentes completos. |
