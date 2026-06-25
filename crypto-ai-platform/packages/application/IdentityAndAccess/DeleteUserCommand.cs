using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record DeleteUserCommand(Guid UserId) : IRequest<bool>;
