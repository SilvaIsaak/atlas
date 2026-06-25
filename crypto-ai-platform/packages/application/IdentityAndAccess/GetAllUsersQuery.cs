using MediatR;

namespace CryptoAIPlatform.Application.IdentityAndAccess;

public record GetAllUsersQuery : IRequest<List<GetUserByIdResponse>>;
