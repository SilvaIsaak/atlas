using MediatR;

namespace CryptoAIPlatform.Application.WalkForward;

public record GetAllWalkForwardsQuery : IRequest<List<GetWalkForwardResponse>>
{
    public Guid UserId { get; init; }
}