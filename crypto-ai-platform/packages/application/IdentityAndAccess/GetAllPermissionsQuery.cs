using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record GetAllPermissionsQuery : IRequest<List<PermissionDto>>;

public record PermissionDto(Permission Permission, string Name, string Description);
