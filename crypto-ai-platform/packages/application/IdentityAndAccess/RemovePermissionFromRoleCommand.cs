using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record RemovePermissionFromRoleCommand(Guid RoleId, Permission Permission) : IRequest<bool>;
