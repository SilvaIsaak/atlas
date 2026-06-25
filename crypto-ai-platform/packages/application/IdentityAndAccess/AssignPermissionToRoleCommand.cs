using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record AssignPermissionToRoleCommand(Guid RoleId, Permission Permission) : IRequest<bool>;
