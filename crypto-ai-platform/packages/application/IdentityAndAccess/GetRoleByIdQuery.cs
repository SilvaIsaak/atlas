using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record GetRoleByIdQuery(Guid RoleId) : IRequest<GetRoleByIdResponse>;
