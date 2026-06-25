using MediatR;
using CryptoAIPlatform.Domain.IdentityAndAccess;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record CreateRoleCommand : IRequest<CreateRoleResponse>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<Permission>? Permissions { get; init; }
}

public record CreateRoleResponse
{
    public Guid RoleId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<Permission>? Permissions { get; init; }
}