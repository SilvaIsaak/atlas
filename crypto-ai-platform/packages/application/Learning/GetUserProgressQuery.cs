using MediatR;

namespace CryptoAIPlatform.Application.Learning;

public record GetUserProgressQuery : IRequest<List<GetUserProgressResponse>>
{
    public Guid UserId { get; init; }
}

public record GetUserProgressResponse(
    Guid ProgressId,
    Guid ContentId,
    int ProgressPercentage,
    bool IsCompleted,
    DateTime? CompletedAt,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
