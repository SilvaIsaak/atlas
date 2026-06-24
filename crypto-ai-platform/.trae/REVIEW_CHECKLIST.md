# Crypto AI Platform — Checklist de Revisão de Código

## Objetivo
Este documento é um checklist obrigatório para todas as revisões de código (PRs) na Crypto AI Platform. Ao revisar um PR, o revisor DEVE verificar todos os itens relevantes para a mudança e marcar [x] quando estiverem ok, ou deixar comentários caso não estejam.

## Índice
1. [Arquitetura e Clean Architecture](#1-arquitetura-e-clean-architecture)
2. [Regras de Negócio e DDD](#2-regras-de-negócio-e-ddd)
3. [Qualidade do Código e SOLID](#3-qualidade-do-código-e-solid)
4. [Segurança](#4-segurança)
5. [Observabilidade](#5-observabilidade)
6. [Testes](#6-testes)
7. [Documentação](#7-documentação)
8. [Performance e Escalabilidade](#8-performance-e-escalabilidade)

---

## 1. Arquitetura e Clean Architecture
| Item | Verificação |
|------|-------------|
| [ ] | O código segue as regras definidas em ARCHITECTURE_RULES.md e PROJECT_CONTEXT.md? |
| [ ] | As dependências entre as camadas da Clean Architecture estão corretas (Domain não depende de ninguém, Application só depende de Domain, Infrastructure implementa interfaces, etc.)? |
| [ ] | Não há lógica de negócio em Infrastructure ou Presentation — toda lógica de negócio está em Domain ou Application? |
| [ ] | Todas as integrações com serviços externos (exchanges, bancos de dados, Kafka) estão encapsuladas em interfaces definidas nas camadas internas? |
| [ ] | Commands/Queries são usados corretamente com MediatR para CQRS? |

---

## 2. Regras de Negócio e DDD
| Item | Verificação |
|------|-------------|
| [ ] | Todos os Value Objects são imutáveis? |
| [ ] | Todos os Agregados Raiz garantem as invariantes de negócio? |
| [ ] | Não há acesso direto a entidades internas de um agregado — todo acesso é via Agregado Raiz? |
| [ ] | Todos os eventos significativos do domínio são representados como Domain Events e lançados pelo Agregado Raiz? |
| [ ] | A terminologia utilizada no código corresponde à Linguagem Ubíqua definida em PROJECT_CONTEXT.md (glossário)? |

---

## 3. Qualidade do Código e SOLID
| Item | Verificação |
|------|-------------|
| [ ] | O código segue as regras de coding standards definidas em CODING_STANDARDS.md? |
| [ ] | O código é formatado com as ferramentas padrão da linguagem (dotnet format, Prettier)? |
| [ ] | Não há arquivos maiores que 500 linhas ou métodos maiores que 100 linhas? |
| [ ] | O código segue os princípios SOLID (Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion)? |
| [ ] | Não há código duplicado (DRY)? |
| [ ] | As variáveis, funções e classes têm nomes claros e significativos (não há nomes genéricos como `x`, `data`, `temp` sem justificativa)? |

---

## 4. Segurança
| Item | Verificação |
|------|-------------|
| [ ] | Não há segredos (senhas, chaves API, certificados) em texto claro no código ou logs? |
| [ ] | Todas as entradas de usuário são validadas e sanitizadas (para evitar injeções como SQL Injection, XSS, etc.)? |
| [ ] | As senhas são armazenadas usando bcrypt/Argon2 (não em texto claro ou hash simples)? |
| [ ] | Todas as APIs exigem autenticação JWT quando necessário? |
| [ ] | As APIs têm rate limiting implementado? |
| [ ] | O código não usa bibliotecas com vulnerabilidades conhecidas (verificação de SCA)? |

---

## 5. Observabilidade
| Item | Verificação |
|------|-------------|
| [ ] | O código tem logs estruturados em JSON com os campos obrigatórios (timestamp, serviceName, traceId, level, message)? |
| [ ] | O código propaga trace ID em chamadas a serviços externos (W3C Trace Context)? |
| [ ] | O código emite métricas customizadas importantes para o Prometheus? |
| [ ] | Não há logs que exponham dados sensíveis do usuário ou da plataforma? |

---

## 6. Testes
| Item | Verificação |
|------|-------------|
| [ ] | O código novo ou modificado tem testes unitários associados? |
| [ ] | Testes de integração foram adicionados para integrações externas, se aplicável? |
| [ ] | Todos os testes automatizados passam no CI/CD? |
| [ ] | Os testes cobrem os casos de sucesso e os casos de erro/limite (edge cases)? |

---

## 7. Documentação
| Item | Verificação |
|------|-------------|
| [ ] | A documentação foi atualizada, se necessário (READMEs, arquivos na pasta docs)? |
| [ ] | O código tem comentários claros para partes complexas (embora preferível deixar o código auto-documentado)? |

---

## 8. Performance e Escalabilidade
| Item | Verificação |
|------|-------------|
| [ ] | Índices adequados foram adicionados nas tabelas do banco de dados para consultas frequentes? |
| [ ] | Cache com Redis foi usado para dados frequentemente acessados e pouco alterados, se aplicável? |
| [ ] | Não há N+1 queries no EF Core ou outras ORMs? |
| [ ] | O código usa async/await para operações I/O (acesso a banco, APIs externas)? |

---

## Histórico de Versões
| Versão | Data | Autor | Descrição |
|--------|------|-------|-----------|
| v1.0 | 2026-06-24 | Equipe de Arquitetura | Versão inicial do documento. |
