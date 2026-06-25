using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record GetUserByIdQuery(Guid UserId) : IRequest<GetUserByIdResponse>;

public record GetUserByIdResponse
{
    public Guid UserId { get; init; }
    public string Email { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public List<string> Roles { get; init; } = new();
}
