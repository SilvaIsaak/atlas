using MediatR;

namespace CryptoAIPlatform.Application.WalkForward;

public record ExecuteWalkForwardCommand : IRequest<ExecuteWalkForwardResponse>
{
    public Guid WalkForwardId { get; init; }
    public Guid UserId { get; init; }
}

public record ExecuteWalkForwardResponse
{
    public Guid WalkForwardId { get; init; }
    public string Status { get; init; } = string.Empty;
}