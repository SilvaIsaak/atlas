# Task 004: Users

## Objetivo
Implementar endpoints de gerenciamento de usuários!

## Status
✅ Concluído

## Critérios de Aceite
- [x] Endpoint para obter usuário por ID (protegido)
- [x] Endpoint para obter todos os usuários (apenas Admin)
- [x] Endpoint para atualizar usuário
- [x] Endpoint para deletar usuário

## Detalhes da Implementação

### Backend
1. Criado `GetUserByIdQuery` e handler para buscar usuário por ID
2. Criado `GetAllUsersQuery` e handler para listar todos os usuários
3. Criado `UpdateUserCommand` e handler para atualizar usuário
4. Criado `DeleteUserCommand` e handler para deletar usuário
5. Adicionado endpoints no `AuthController`:
   - `GET /api/v1/auth/users` (Admin)
   - `GET /api/v1/auth/users/{userId}` (Autenticado)
   - `PUT /api/v1/auth/users/{userId}` (Autenticado)
   - `DELETE /api/v1/auth/users/{userId}` (Admin)

## Arquivos Criados/Atualizados
- `packages/application/IdentityAndAccess/GetUserByIdQuery.cs`
- `packages/application/IdentityAndAccess/GetUserByIdQueryHandler.cs`
- `packages/application/IdentityAndAccess/GetAllUsersQuery.cs`
- `packages/application/IdentityAndAccess/GetAllUsersQueryHandler.cs`
- `packages/application/IdentityAndAccess/UpdateUserCommand.cs`
- `packages/application/IdentityAndAccess/UpdateUserCommandHandler.cs`
- `packages/application/IdentityAndAccess/DeleteUserCommand.cs`
- `packages/application/IdentityAndAccess/DeleteUserCommandHandler.cs`
- `packages/presentation/Controllers/AuthController.cs`
- `tasks/004-Users.md`

## Próximas Tarefas
- Task 005: Roles
- Task 006: Permissions
