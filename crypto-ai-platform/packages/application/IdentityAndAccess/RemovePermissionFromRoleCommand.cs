using MediatR;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record RemovePermissionFromRoleCommand(Guid RoleId, Permission Permission) : IRequest<bool>;
