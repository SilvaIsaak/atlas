# Task 21: Notifications

## Objetivo
Implementar o módulo de Notifications para a Crypto AI Platform! Permitir que os usuários recebam alertas sobre trades, riscos, atualizações de sistema e muito mais!

## Status
✅ Implementado com sucesso!

## Critérios de Aceite
- [x] Criar entidades Notification, NotificationType na camada Domain
- [x] Adicionar DbSet<Notification> no ApplicationDbContext
- [x] Criar queries/commands na camada Application para gerenciar notificações
- [x] Criar endpoints na camada Presentation para o módulo de Notifications
- [x] Implementar marcação de notificações como lidas

## Detalhes da Implementação
### Domain
- Criar enum `NotificationType`
- Criar entidade `Notification` como Aggregate Root

### Infrastructure
- Atualizar `ApplicationDbContext` com `DbSet<Notification>`

### Application
- Criar `CreateNotificationCommand` e handler
- Criar `GetNotificationQuery` e handler
- Criar `GetAllNotificationsQuery` e handler
- Criar `MarkNotificationAsReadCommand` e handler

### Presentation
- Criar `NotificationsController`

## Arquivos Criados/Atualizados
- `packages/domain/Notifications/NotificationType.cs`
- `packages/domain/Notifications/Notification.cs`
- `packages/infrastructure/Data/ApplicationDbContext.cs`
- `packages/application/Notifications/CreateNotificationCommand.cs`
- `packages/application/Notifications/CreateNotificationCommandHandler.cs`
- `packages/application/Notifications/MarkNotificationAsReadCommand.cs`
- `packages/application/Notifications/MarkNotificationAsReadCommandHandler.cs`
- `packages/application/Notifications/GetAllNotificationsQuery.cs`
- `packages/application/Notifications/GetAllNotificationsQueryHandler.cs`
- `packages/application/Notifications/GetNotificationQuery.cs`
- `packages/application/Notifications/GetNotificationQueryHandler.cs`
- `packages/presentation/Controllers/NotificationsController.cs`
- `tasks/021-Notifications.md`

## Próximas Tarefas
- Task 22: Dashboard