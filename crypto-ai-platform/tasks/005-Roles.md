# Task 005: Roles

## Objetivo
Implementar endpoints de gerenciamento de roles!

## Status
✅ Concluído

## Critérios de Aceite
- [x] Endpoint para criar role (apenas Admin)
- [x] Endpoint para obter todas as roles (apenas Admin)
- [x] Endpoint para obter role por ID (apenas Admin)
- [x] Endpoint para atualizar role (apenas Admin)
- [x] Endpoint para deletar role (apenas Admin)

## Detalhes da Implementação

### Backend
1. Criado `GetAllRolesQuery` e handler para listar todas as roles
2. Criado `GetRoleByIdQuery` e handler para buscar role por ID
3. Criado `UpdateRoleCommand` e handler para atualizar role
4. Criado `DeleteRoleCommand` e handler para deletar role
5. Adicionado endpoints no `AuthController`:
   - `POST /api/v1/auth/roles` (Admin)
   - `GET /api/v1/auth/roles` (Admin)
   - `GET /api/v1/auth/roles/{roleId}` (Admin)
   - `PUT /api/v1/auth/roles/{roleId}` (Admin)
   - `DELETE /api/v1/auth/roles/{roleId}` (Admin)

## Arquivos Criados/Atualizados
- `packages/application/IdentityAndAccess/GetAllRolesQuery.cs`
- `packages/application/IdentityAndAccess/GetAllRolesQueryHandler.cs`
- `packages/application/IdentityAndAccess/GetRoleByIdQuery.cs`
- `packages/application/IdentityAndAccess/GetRoleByIdQueryHandler.cs`
- `packages/application/IdentityAndAccess/UpdateRoleCommand.cs`
- `packages/application/IdentityAndAccess/UpdateRoleCommandHandler.cs`
- `packages/application/IdentityAndAccess/DeleteRoleCommand.cs`
- `packages/application/IdentityAndAccess/DeleteRoleCommandHandler.cs`
- `packages/presentation/Controllers/AuthController.cs`
- `tasks/005-Roles.md`

## Próximas Tarefas
- Task 006: Permissions
