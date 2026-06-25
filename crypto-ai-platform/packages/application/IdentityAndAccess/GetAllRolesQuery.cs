using MediatR;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record GetAllRolesQuery : IRequest<List<GetRoleByIdResponse>>;

public record GetRoleByIdResponse(Guid RoleId, string Name, string Description, DateTime CreatedAt, List<Permission> Permissions);
