using MediatR;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record AssignPermissionToRoleCommand(Guid RoleId, Permission Permission) : IRequest<bool>;
