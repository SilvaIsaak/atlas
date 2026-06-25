using MediatR;

namespace CryptoAIPlatform.Application.Learning;

public record UpdateUserProgressCommand : IRequest<UpdateUserProgressResponse>
{
    public Guid UserId { get; init; }
    public Guid ContentId { get; init; }
    public int ProgressPercentage { get; init; }
    public bool IsCompleted { get; init; }
}

public record UpdateUserProgressResponse(
    Guid ProgressId,
    int ProgressPercentage,
    bool IsCompleted,
    DateTime? CompletedAt
);
