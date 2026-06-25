# Task 24: Learning

## Objetivo
Implementar o módulo de Learning para a Crypto AI Platform! Permitir que os usuários acessem conteúdos educacionais (artigos, vídeos, tutoriais, cursos) e acompanhem o seu progresso!

## Status
✅ Implementado com sucesso!

## Critérios de Aceite
- [x] Criar entidades LearningContent, UserLearningProgress, ContentType na camada Domain
- [x] Adicionar DbSet<LearningContent> e DbSet<UserLearningProgress> no ApplicationDbContext
- [x] Criar queries na camada Application para obter conteúdos e progresso do usuário
- [x] Criar comando na camada Application para atualizar progresso do usuário
- [x] Criar endpoints na camada Presentation para o módulo de Learning

## Detalhes da Implementação
### Domain
- Criar enum `ContentType` (Article, Video, Tutorial, Course)
- Criar entidade `LearningContent` como Aggregate Root
- Criar entidade `UserLearningProgress` como Aggregate Root

### Infrastructure
- Atualizar `ApplicationDbContext` com `DbSet<LearningContent>` e `DbSet<UserLearningProgress>`

### Application
- Criar `GetLearningContentsQuery` e handler
- Criar `GetLearningContentQuery` e handler
- Criar `UpdateUserProgressCommand` e handler
- Criar `GetUserProgressQuery` e handler

### Presentation
- Criar `LearningController`

## Arquivos Criados/Atualizados
- `packages/domain/Learning/ContentType.cs`
- `packages/domain/Learning/LearningContent.cs`
- `packages/domain/Learning/UserLearningProgress.cs`
- `packages/infrastructure/Data/ApplicationDbContext.cs`
- `packages/application/Learning/GetLearningContentsQuery.cs`
- `packages/application/Learning/GetLearningContentsQueryHandler.cs`
- `packages/application/Learning/GetLearningContentQuery.cs`
- `packages/application/Learning/GetLearningContentQueryHandler.cs`
- `packages/application/Learning/UpdateUserProgressCommand.cs`
- `packages/application/Learning/UpdateUserProgressCommandHandler.cs`
- `packages/application/Learning/GetUserProgressQuery.cs`
- `packages/application/Learning/GetUserProgressQueryHandler.cs`
- `packages/presentation/Controllers/LearningController.cs`
- `tasks/024-Learning.md`

## Próximas Tarefas
- Task 25: Deployment