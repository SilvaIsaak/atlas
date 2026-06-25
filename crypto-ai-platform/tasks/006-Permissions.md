# Task 006: Permissions

## Objetivo
Implementar sistema de permissions e policy-based authorization!

## Status
✅ Implementado com sucesso!

## Critérios de Aceite
- [x] Criar enum de Permissions
- [x] Adicionar policy-based authorization no backend
- [x] Criar endpoints de gerenciamento de permissions
- [x] Associar permissions a roles

## Detalhes da Implementação

### Backend
1. Criado enum `Permission` no `Domain.IdentityAndAccess` com todas as permissões
2. Criada entidade `RolePermission` para mapear muitos para muitos entre Role e Permission
3. Adicionada propriedade `RolePermissions` na entidade `Role`
4. Atualizado `ApplicationDbContext` para incluir o mapeamento de RolePermission
5. Adicionado policy-based authorization no `DependencyInjection.cs` com políticas para cada permissão
6. Atualizado `JwtTokenService` para incluir permissões no token JWT
7. Criados comandos/handlers para gerenciar permissões (Assinar/Remover de Role)
8. Adicionados endpoints no `AuthController`:
   - GET /permissions (obter todas as permissões)
   - POST /roles/{roleId}/permissions (atribuir permissão a role)
   - DELETE /roles/{roleId}/permissions/{permission} (remover permissão de role)

## Arquivos Criados/Atualizados
- `packages/domain/IdentityAndAccess/Permission.cs`
- `packages/domain/IdentityAndAccess/RolePermission.cs`
- `packages/domain/IdentityAndAccess/Role.cs`
- `packages/infrastructure/Data/ApplicationDbContext.cs`
- `packages/infrastructure/DependencyInjection.cs`
- `packages/infrastructure/Services/JwtTokenService.cs`
- `packages/application/IdentityAndAccess/AssignPermissionToRoleCommand.cs`
- `packages/application/IdentityAndAccess/AssignPermissionToRoleCommandHandler.cs`
- `packages/application/IdentityAndAccess/RemovePermissionFromRoleCommand.cs`
- `packages/application/IdentityAndAccess/RemovePermissionFromRoleCommandHandler.cs`
- `packages/application/IdentityAndAccess/GetAllPermissionsQuery.cs`
- `packages/application/IdentityAndAccess/GetAllPermissionsQueryHandler.cs`
- `packages/application/IdentityAndAccess/GetAllRolesQuery.cs`
- `packages/application/IdentityAndAccess/GetAllRolesQueryHandler.cs`
- `packages/application/IdentityAndAccess/GetRoleByIdQueryHandler.cs`
- `packages/application/IdentityAndAccess/CreateRoleCommand.cs`
- `packages/application/IdentityAndAccess/CreateRoleCommandHandler.cs`
- `packages/application/IdentityAndAccess/UpdateRoleCommand.cs`
- `packages/application/IdentityAndAccess/UpdateRoleCommandHandler.cs`
- `packages/presentation/Controllers/AuthController.cs`
- `tasks/006-Permissions.md`

## Próximas Tarefas
- Melhorias e integração com frontend
