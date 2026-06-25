using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record DeleteRoleCommand(Guid RoleId) : IRequest<bool>;
