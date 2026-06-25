using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record AssignRoleCommand : IRequest<AssignRoleResponse>
{
    public Guid UserId { get; init; }
    public string RoleName { get; init; } = string.Empty;
}

public record AssignRoleResponse
{
    public Guid UserId { get; init; }
    public string RoleName { get; init; } = string.Empty;
    public bool Success { get; init; }
}