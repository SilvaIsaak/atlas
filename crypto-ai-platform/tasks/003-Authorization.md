# Task 003: Authorization

## Objective
Implement role-based and policy-based authorization for the Crypto AI Platform!

## Status
✅ Completed (core role-based auth done)

## Criteria for Acceptance
- [x] JWT tokens include user roles
- [x] Role-based authorization with [Authorize(Roles = "...")]
- [x] Role management endpoints (create role, assign role)
- [x] Frontend auth store includes roles
- [ ] Policy-based authorization
- [ ] Full role CRUD endpoints
- [ ] Frontend role management UI

## Implementation Details

### Backend
1. Updated `JwtTokenService` to generate tokens with role claims
2. Added `CreateRoleCommand` and `AssignRoleCommand` in Application layer
3. Updated `AuthController` with role endpoints (protected by Admin role)
4. Added test endpoints to verify authorization

### Frontend
1. Updated `useAuthStore` to include user roles
2. Updated `LoginUserResponse` and auth flow to pass roles

## Files Created/Updated
### Backend
- `packages/infrastructure/Services/JwtTokenService.cs`
- `packages/application/IdentityAndAccess/CreateRoleCommand.cs`
- `packages/application/IdentityAndAccess/CreateRoleCommandHandler.cs`
- `packages/application/IdentityAndAccess/AssignRoleCommand.cs`
- `packages/application/IdentityAndAccess/AssignRoleCommandHandler.cs`
- `packages/application/IdentityAndAccess/LoginUserCommand.cs`
- `packages/application/IdentityAndAccess/LoginUserCommandHandler.cs`
- `packages/presentation/Controllers/AuthController.cs`

### Frontend
- `apps/web/lib/stores/useAuthStore.ts`

## Next Tasks
- Task 004: Users
- Task 005: Roles
- Task 006: Permissions
